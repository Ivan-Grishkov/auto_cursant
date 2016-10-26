using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator : ICallMeNotificator
    {
        private const string AdminEmail = "grishkov.ivan@gmail.com";
        private const string AdminEmail2 = "auto.dev2016@gmail.com";
        /// <param name="phone"></param>
        /// <param name="instructorPhone">admin if no instructor</param>
        /// <returns>is success</returns>
        public async Task<bool> NotifyAsync(string phone, string instructorPhone)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(AdminEmail2);
                message.To.Add(new MailAddress(AdminEmail));
                message.Subject = "This is my subject";
                message.Body = "This is the content";

                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;

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