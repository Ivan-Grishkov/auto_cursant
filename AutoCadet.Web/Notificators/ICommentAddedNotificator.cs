using AutoCadet.Domain.Entities;

namespace AutoCadet.Notificators
{
    public interface ICommentAddedNotificator
    {
        bool Notify(Comment comment);
    }
}