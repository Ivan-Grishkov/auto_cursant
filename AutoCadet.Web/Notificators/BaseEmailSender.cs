using MimeKit;
using System.Configuration;
using System.Linq;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AutoCadet.Notificators
{
    public class BaseEmailSender
    {
        private const char EmailSplitter = ';';
        private readonly string _adminEmail = ConfigurationManager.AppSettings["NotifyMeDefaultEmails"] ?? "grishkov.ivan@gmail.com";

        protected void SentEmail(string body, string subject, params string[] toEmails)
        {
            var message = new MimeMessage();
            if (toEmails != null && toEmails.Any())
            {
                foreach (var email in toEmails)
                {
                    message.To.Add(new MailboxAddress (email, email));
                }
            }
            message.From.Add(new MailboxAddress("Уроки Вождения", "auto.dev2016@gmail.com"));
            var defaultEmails = _adminEmail.Split(EmailSplitter);
            foreach (var defaultEmail in defaultEmails.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                message.To.Add(new MailboxAddress (defaultEmail, defaultEmail));
            }

            message.Subject = subject;
            message.Body = new TextPart("plain") {
                Text = body
            };

            var mailSmtp = ConfigurationManager.AppSettings["MailSmtp"];
            var mailLogin = ConfigurationManager.AppSettings["MailLogin"];
            var mailAppPassword = ConfigurationManager.AppSettings["MailAppPassword"];

            using (var client = new SmtpClient())
            {
                client.Connect(mailSmtp, 465, true);

                client.Authenticate(mailLogin, mailAppPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}