namespace Application.ExternalAPI
{
    public interface ISendGridEmail
    {
        Task<bool> SendMail(string to, string subject, string body, string fromName = "", string replyToEmail = "");
    }
}
