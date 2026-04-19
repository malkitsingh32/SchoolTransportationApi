using Application.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static Twilio.Rest.Api.V2010.Account.MessageResource;
namespace Infrastructure.Implementation.Services
{
    public  class TwilioService
    {
        private readonly TwilioSettings _twilioSettings;
        public TwilioService(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
        }
        public  async Task<MessageResource> SendSmsAsync(string toNumber, string body, DateTime? scheduleAtUtc = null)
        {
            
                body = body ?? "";
                string toNumberNormalized = ConvertToTwillioPhone(toNumber);
                TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
                return await MessageResource.CreateAsync(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilioSettings.FromNumber),
                    to: new Twilio.Types.PhoneNumber(toNumberNormalized),
                    sendAt: scheduleAtUtc,
                    scheduleType: scheduleAtUtc.HasValue
                        ? ScheduleTypeEnum.Fixed
                        : null
                );         
        }

        public static string ConvertToTwillioPhone(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = new string(value.Where(char.IsDigit).ToArray());
                value = value.Length == 10 ? "+1" + value : "+" + value;
            }
            return value;
        }
    }
}
