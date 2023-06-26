using System.Net;
using System.Net.Mail;

namespace FirstWeb
{
    internal static class SMTPHandler
    {
        public static bool SendEmail(string email, string title, string content)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress("SMTP EMAIL HERE");
                message.To.Add(new MailAddress(email));

                message.Subject = title;
                message.Body = content;

                SmtpClient client = new SmtpClient();

                client.Host = "smtp.gmail.com";
                client.Port = 587;

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("SMTP EMAIL HERE", "SMTP PASSWORD HERE");

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
