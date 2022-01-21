using System;
using System.Collections.Generic;
using System.Linq;
using Gates.BL.Interfaces.Repository;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using NLog;

namespace Gates.BL.Managers
{
    /// <summary>
    /// Менеджер комментариев
    /// </summary>
    public class CommentManager : ICommentManager
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger _logger;

        public CommentManager(ICommentRepository commentRepository, ILogger logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        /// <summary>
        /// Метод добавляет комментарий в БД
        /// </summary>
        /// <param name="comment">Модель комментария из WebApi</param>
        /// <returns>True если комментарий успешно добавлен, иначе - false</returns>
        public bool AddComment(CommentViewModel comment)
        {
            _logger.Info($"Add comment.");

            return _commentRepository.Create(comment);
        }

        /// <summary>
        /// Метод изменяет комментарий
        /// </summary>
        /// <param name="comment">Модель комментария из WebApi</param>
        /// <returns>True если комментарий успешно изменен, иначе - false</returns>
        public bool ChangeComment(CommentViewModel comment)
        {
            _logger.Info($"Change comment.");

            return _commentRepository.Update(comment);
        }

        /// <summary>
        /// Метод удаляет комментарий
        /// </summary>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <returns>True если комментарий успешно удален, иначе - false</returns>
        public bool DeleteComment(int commentId)
        {
            _logger.Info($"Delete comment. Comment id: [{commentId}]");

            return _commentRepository.Delete(commentId);
        }

        /// <summary>
        /// Метод возвращает комментарий
        /// </summary>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <returns>Модель комментария для WebApi</returns>
        public CommentViewModel GetComment(int commentId)
        {
            _logger.Info($"Get comment. Comment id: [{commentId}]");

            return _commentRepository.Get(commentId);
        }

        /// <summary>
        /// Метод возвращает все комментарии определенной задачи
        /// </summary>
        /// <param name="taskId">Идентификатор комментария</param>
        /// <returns>Коллекция комментариев задачи</returns>
        public ICollection<CommentViewModel> GetTaskComments(int taskId)
        {
            _logger.Info($"Get task comments. Task id: [{taskId}]");

            var comments = _commentRepository.GetTaskComments(taskId);

            _logger.Info($"Get task comments. Comments number: [{comments?.Count}]");

            return comments;
        }
    }
}
