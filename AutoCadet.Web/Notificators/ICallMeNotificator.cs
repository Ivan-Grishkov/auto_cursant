using AutoCadet.Domain.Entities;

namespace AutoCadet.Notificators
{
    public interface ICallMeNotificator
    {
        bool Notify(string phone, Instructor instructor);
    }
}