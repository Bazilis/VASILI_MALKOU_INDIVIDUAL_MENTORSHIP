namespace BLL.Interfaces
{
    public interface IEmailSender
    {
        (bool, string) SendEmail(string userEmail, string emailSubject, string emailBody, bool isUseRabbitmq);
    }
}
