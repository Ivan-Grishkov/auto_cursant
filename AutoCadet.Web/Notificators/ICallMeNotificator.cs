using System.Threading.Tasks;

namespace AutoCadet.Notificators
{
    public interface ICallMeNotificator
    {
        Task<bool> NotifyAsync(string phone, string instructorPhone);
    }
}