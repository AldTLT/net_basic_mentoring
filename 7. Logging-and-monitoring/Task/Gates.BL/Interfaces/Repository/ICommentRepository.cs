using System.Collections.Generic;
using Gates.BL.ViewModels;

namespace Gates.BL.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс содержащий методы для работы с Comment
    /// </summary>
    public interface ICommentRepository : IRepository<CommentViewModel>
    {
        /// <summary>
        /// Получить все комментарии определенной задачи
        /// </summary>
        /// <param name="taskId">Id задачи</param>
        /// <returns>Коллекция комментариев</returns>
        ICollection<CommentViewModel> GetTaskComments(int taskId);
    }
}
