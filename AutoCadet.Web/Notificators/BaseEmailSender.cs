using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace AutoCadet.Notificators
{
    public class BaseEmailSender
    {
        private const char EmailSplitter = ';';
        private readonly string _adminEmail = ConfigurationManager.AppSettings["NotifyMeDefaultEmails"] ?? "grishkov.ivan@gmail.com";

        protected void SentEmail(string body, string subject, params string[] toEmails)
        {
            MailMessage message = new MailMessage();
            if (toEmails != null && toEmails.Any())
            {
                foreach (var email in toEmails)
                {
                    message.To.Add(email);
                }
            }

            var defaultEmails = _adminEmail.Split(EmailSplitter);
            foreach (var defaultEmail in defaultEmails.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                message.To.Add(new MailAddress(defaultEmail));
            }

            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient
            {
                Timeout = 2000
            };

            client.Send(message);
        }
    }
}