using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using AutoCadet.Domain.Entities;
using log4net;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator : ICallMeNotificator
    {
        private readonly ILog _log;
        private const char EmailSplitter = ';';
        private readonly string _adminEmail = ConfigurationManager.AppSettings["NotifyMeDefaultEmails"] ?? "grishkov.ivan@gmail.com";

        public CallMeNotificator(ILog log)
        {
            _log = log;
        }

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
                var messageText = $"{instructor?.LastName} {instructor?.FirstName} перезвоните, пожалуйста, на номер {phone}. {requesterName}";
                if (instructor != null)
                {
                    messageText += $"тел. инструктора: {instructor.Phone}";
                    if (!string.IsNullOrWhiteSpace(instructor.Phone2))
                    {
                        messageText += $" и {instructor.Phone2}";
                    }
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
                _log.Error(e);
                return false;
            }
            return true;
        }
    }
}