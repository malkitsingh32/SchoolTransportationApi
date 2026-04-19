using Application.Abstraction.Services;
using Application.Settings;
using Domain.Entities;
using DTO.Response.Students;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Infrastructure.Implementation.Services
{
    public class SmsService : ISmsService
    {
        private readonly TwilioSettings _twilioSettings;
        private readonly IAdminService _adminService;

        public SmsService(IOptions<TwilioSettings> twilioSettings, IAdminService adminService)
        {
            _twilioSettings = twilioSettings.Value;
            _adminService = adminService;
        }
        public async Task SendBulkSmsAsync(IList<StudentSmsDto> students)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            int maxParallelTasks = 30; // safe concurrency for Twilio

            using SemaphoreSlim semaphore = new SemaphoreSlim(maxParallelTasks);

            var tasks = students.Select(async student =>
            {
                await semaphore.WaitAsync();

                try
                {
                    string formattedNumber = FormatPhoneNumber(student.PhoneNumber);
                    string twilioPhoneNumber = formattedNumber.Length == 10 ? "+1" + formattedNumber : "+" + formattedNumber;

                    await MessageResource.CreateAsync(
                        body: student.Message,
                        from: new PhoneNumber(_twilioSettings.FromNumber),
                        to: new PhoneNumber(twilioPhoneNumber)
                    );

                    await _adminService.AddLogs(student.Message, formattedNumber, "Outbound");
                }
                catch (Exception ex)
                {
                    await _adminService.AddLogs($"Failed: {ex.Message}", student.PhoneNumber, "Error");
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
        public static string FormatPhoneNumber(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (phoneNumber.Length == 11 && phoneNumber.StartsWith("1"))
            {
                return $"({phoneNumber.Substring(1, 3)}) {phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7)}";
            }

            return phoneNumber;
        }
    }
}
