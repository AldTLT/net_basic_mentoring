using Gates.BL.ViewModels;
using Gates.DAL.Entities;

namespace Gates.DAL.Map
{
    /// <summary>
    ///     Класс представляет маппер CommentEntity <-> CommentViewModel
    /// </summary>
    internal static class CommentMapper
    {
        /// <summary>
        ///     Метод конвертирует CommentViewModel в CommentEntity
        /// </summary>
        /// <param name="commentViewModel">CommentViewModel</param>
        /// <returns>CommentEntity</returns>
        public static CommentEntity Map(this CommentViewModel commentViewModel)
        {
            return new CommentEntity
            {
                AuthorLogin = commentViewModel.Login,
                Content = commentViewModel.Content,
                TaskId = commentViewModel.TaskId
            };
        }

        /// <summary>
        ///     Метод конвертирует CommentEntity в CommentViewModel
        /// </summary>
        /// <param name="commentEntity">CommentEntity</param>
        /// <returns>CommentViewModel</returns>
        public static CommentViewModel Map(this CommentEntity commentEntity)
        {
            return new CommentViewModel
            {
                CommentId = commentEntity.Id,
                Login = commentEntity.AuthorLogin,
                Content = commentEntity.Content,
                CreateDate = commentEntity.CreateDate,
                TaskId = commentEntity.TaskId
            };
        }
    }
}