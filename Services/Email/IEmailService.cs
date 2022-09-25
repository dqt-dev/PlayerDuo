namespace HappyVacation.Services.Email
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string content, byte[] qrCodeAttachment);
    }
}
