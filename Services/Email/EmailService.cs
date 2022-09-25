using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace HappyVacation.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(string email, string subject, string content, byte[] qrCodeAttachment)
        {

            string MyEmailAddress = _config["MyEmail:EmailAddress"];
            string Password = _config["MyEmail:Password"];

            MailAddress from = new MailAddress(MyEmailAddress, "Happy Vacation");
            MailAddress to = new MailAddress(email);
            var message = new MailMessage(from, to);
            message.Subject = subject;

            message.Body = content;
            message.IsBodyHtml = true;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = content;

            // email attachment
            if(qrCodeAttachment != null)
            {
                Attachment att = new Attachment(new MemoryStream(qrCodeAttachment), "Your QR code.png", MediaTypeNames.Image.Jpeg);
                message.Attachments.Add(att);
            }

            // send email
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(MyEmailAddress, Password);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}
