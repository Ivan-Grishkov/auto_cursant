using System.Threading.Tasks;

namespace AutoCadet.Notificators
{
    public class CallMeNotificator
    {
        /// <param name="phone"></param>
        /// <param name="instructorId">admin if no instructor</param>
        /// <returns>is success</returns>
        public async Task<bool> NotifyAsync(string phone, int? instructorId)
        {

            return false;
        }
    }
}