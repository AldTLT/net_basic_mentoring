using System.Collections.Generic;
using Gates.BL.ViewModels;

namespace Gates.BL.Interfaces.WebApi
{
    public interface ICommentManager
    {
        /// <summary>
        /// Метод добавляет комментарий в БД
        /// </summary>
        /// <param name="comment">Модель комментария из WebApi</param>
        /// <returns>True если комментарий успешно добавлен, иначе - false</returns>
        bool AddComment(CommentViewModel comment);

        /// <summary>
        /// Метод изменяет комментарий
        /// </summary>
        /// <param name="comment">Модель комментария из WebApi</param>
        /// <returns>True если комментарий успешно изменен, иначе - false</returns>
        bool ChangeComment(CommentViewModel comment);

        /// <summary>
        /// Метод удаляет комментарий
        /// </summary>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <returns>True если комментарий успешно удален, иначе - false</returns>
        bool DeleteComment(int commentId);

        /// <summary>
        /// Метод возвращает комментарий
        /// </summary>
        /// <param name="commentId">Идентификатор комментария</param>
        /// <returns>Модель комментария для WebApi</returns>
        CommentViewModel GetComment(int commentId);

        /// <summary>
        /// Метод возвращает все комментарии определенной задачи
        /// </summary>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <returns>Коллекция комментариев задачи</returns>
        ICollection<CommentViewModel> GetTaskComments(int taskId);
    }
}
