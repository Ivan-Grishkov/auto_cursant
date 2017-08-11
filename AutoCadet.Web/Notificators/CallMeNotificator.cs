using System;
using System.Linq;
using AutoCadet.Domain;
using AutoCadet.Domain.Entities;
using log4net;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator : BaseEmailSender, ICallMeNotificator
    {
        private readonly ILog _log;
        private readonly AutoCadetDbContext _autoCadetDbContext;

        public CallMeNotificator(ILog log, AutoCadetDbContext autoCadetDbContext)
        {
            _log = log;
            _autoCadetDbContext = autoCadetDbContext;
        }

        /// <param name="phone"></param>
        /// <param name="requesterName"></param>
        /// <param name="instructor">admin if no instructor</param>
        /// <returns>is success</returns>
        public bool Notify(string phone, string requesterName, Instructor instructor)
        {
            try
            {
                if (instructor == null)
                {
                    instructor = _autoCadetDbContext.Instructors.Where(x => x.IsPrimary).OrderBy(x => Guid.NewGuid()).Take(3).FirstOrDefault();
                }

                var messageText = $"{instructor?.LastName} {instructor?.FirstName} перезвоните, пожалуйста, на номер {phone}. {requesterName}";
                if (instructor != null)
                {
                    messageText += $" тел. инструктора: {instructor.Phone}";
                    if (!string.IsNullOrWhiteSpace(instructor.Phone2))
                    {
                        messageText += $" и {instructor.Phone2}";
                    }
                }

                SentEmail(messageText, "Перезвоните - uroki-vozhdeniya.by", instructor?.Email);
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