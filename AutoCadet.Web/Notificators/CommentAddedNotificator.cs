using System;
using System.Linq;
using AutoCadet.Domain;
using AutoCadet.Domain.Entities;
using AutoCadet.Models;
using log4net;

namespace AutoCadet.Notificators
{
    public class CommentAddedNotificator: BaseEmailSender, ICommentAddedNotificator
    {
        private readonly ILog _log;
        private readonly AutoCadetDbContext _autoCadetDbContext;

        public CommentAddedNotificator(ILog log, AutoCadetDbContext autoCadetDbContext)
        {
            _log = log;
            _autoCadetDbContext = autoCadetDbContext;
        }

        public bool Notify(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return false;
                }

                _log.Debug($"CommentAddedNotificator. {comment.Id}, Name:{comment.Name}, Score:{comment.Score}.");

                var instructor = comment.Instructor;
                if (instructor == null)
                {
                    _log.Warn($"Instructor not found: {comment.Instructor?.Id} ");
                    instructor = new Instructor();
                }

                string body =
                    $"Поступил новый коментарий про инструктора {instructor.FirstName} {instructor.LastName} \n" +
                    $"Рейтинг: {comment.Score}; \n" +
                    $"Текст: {comment.Text}; \n\n" +
                    $"От {comment.Name}; \n" +
                    $"Телефон: {comment.Phone}, Email: {comment.Email}";

                SentEmail(body, "Новый коментарий - uroki-vozhdeniya.by", instructor.Email);

                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
        }
    }
}