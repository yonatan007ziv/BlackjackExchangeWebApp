using System.Net;
using System.Net.Mail;

namespace FirstWeb
{
    internal static class SMTPHandler
    {
        private static readonly string SmtpEmail = "email here";
        private static readonly string SmtpPassword = "password here";
        
        public static bool SendEmail(string email, string title, string content)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(SmtpEmail);
                message.To.Add(new MailAddress(email));

                message.Subject = title;
                message.Body = content;

                SmtpClient client = new SmtpClient();

                client.Host = "smtp.gmail.com";
                client.Port = 587;

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SmtpEmail, SmtpPassword);

                client.EnableSsl = true;

                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
