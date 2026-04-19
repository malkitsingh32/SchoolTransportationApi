using Application.ExternalAPI;
using Application.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;


namespace Infrastructure.Implementation.Services.Email
{
    public class SendGridEmail : ISendGridEmail
    {
        private readonly Mailsetting _mailSetting;

        #region Ctor
        public SendGridEmail(IOptions<Mailsetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        #endregion
        #region Send Mail

        public async Task<bool> SendMail(string to, string subject, string body, string fromName = "", string replyToEmail = "")
        {
            try
            {
                using var client = new SmtpClient(_mailSetting.SmtpHost, _mailSetting.SmtpPort)
                {
                    Credentials = new NetworkCredential(_mailSetting.UserName, _mailSetting.Password),
                    EnableSsl = _mailSetting.EnableSsl
                };

                using var msg = new MailMessage
                {
                    From = new MailAddress(_mailSetting.FromEmail, string.IsNullOrEmpty(fromName) ? _mailSetting.DisplayName : fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                msg.To.Add(new MailAddress(to));

                if (!string.IsNullOrEmpty(replyToEmail))
                {
                    msg.ReplyToList.Add(new MailAddress(replyToEmail));
                }

                await client.SendMailAsync(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendMailWithAttachment(string to, string subject, string body, byte[] attachment = null, string attachmentName = "", string fromName = "", string replyToEmail = "")
        {
            try
            {
                using var client = new SmtpClient(_mailSetting.SmtpHost, _mailSetting.SmtpPort)
                {
                    Credentials = new NetworkCredential(_mailSetting.UserName, _mailSetting.Password),
                    EnableSsl = _mailSetting.EnableSsl
                };

                using var msg = new MailMessage
                {
                    From = new MailAddress(_mailSetting.FromEmail, string.IsNullOrEmpty(fromName) ? (_mailSetting.DisplayName ?? "Default Name") : fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                msg.To.Add(new MailAddress(to));

                if (!string.IsNullOrEmpty(replyToEmail))
                {
                    msg.ReplyToList.Add(new MailAddress(replyToEmail));
                }

                if (attachment != null && attachment.Length > 0 && !string.IsNullOrEmpty(attachmentName))
                {
                    using var ms = new MemoryStream(attachment);
                    var attach = new Attachment(ms, attachmentName);
                    msg.Attachments.Add(attach);

                    await client.SendMailAsync(msg);
                }
                else
                {
                    await client.SendMailAsync(msg);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email with attachment: {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}
