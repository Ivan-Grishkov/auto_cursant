using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using AutoCadet.Domain.Entities;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator : ICallMeNotificator
    {
        private const char EmailSplitter = ';';
        private readonly string _adminEmail = ConfigurationManager.AppSettings["NotifyMeDefaultEmails"] ?? "grishkov.ivan@gmail.com";

        /// <param name="phone"></param>
        /// <param name="requesterName"></param>
        /// <param name="instructor">admin if no instructor</param>
        /// <returns>is success</returns>
        public bool Notify(string phone, string requesterName, Instructor instructor)
        {
            try
            {
                MailMessage message = new MailMessage();
                if (!string.IsNullOrWhiteSpace(instructor?.Email))
                {
                    message.To.Add(instructor.Email);
                }

                var defaultEmails = _adminEmail.Split(EmailSplitter);
                foreach (var defaultEmail in defaultEmails.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    message.CC.Add(new MailAddress(defaultEmail));
                }
                message.Subject = "Перезвоните uroki-vozhdeniya.by";
                var messageText = $"Перезвоните, пожалуйста, на номер {phone}. {requesterName}";
                if (instructor != null)
                {
                    messageText += $" Инструктор {instructor.LastName} {instructor.FirstName}. тел. инструктора: {instructor.Phone}";
                }
                message.Body = messageText;

                SmtpClient client = new SmtpClient
                {
                    Timeout = 2000
                };

                client.Send(message);
            }
            catch (Exception e)
            {
                //TODO[2016/10/26 IG]: handle exception properly.
                return false;
            }
            return true;
        }
    }
}