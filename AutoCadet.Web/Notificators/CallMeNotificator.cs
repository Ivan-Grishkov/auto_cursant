using System;
using System.Net.Mail;
using AutoCadet.Domain.Entities;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator : ICallMeNotificator
    {
        private const string AdminEmail = "grishkov.ivan@gmail.com";

        /// <param name="phone"></param>
        /// <param name="instructor">admin if no instructor</param>
        /// <returns>is success</returns>
        public bool Notify(string phone, Instructor instructor)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(AdminEmail));
                message.Subject = "Перезвоните Auto Instructor";
                var messageText = $"Перезвоните, пожалуйста, на номер {phone}.";
                if (instructor != null)
                {
                    messageText += $" Инструктор {instructor.LastName} {instructor.FirstName}. тел. инструктораL {instructor.Phone}";
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