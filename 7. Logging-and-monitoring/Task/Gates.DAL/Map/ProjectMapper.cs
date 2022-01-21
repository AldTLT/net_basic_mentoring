using Gates.BL.ViewModels;
using Gates.DAL.Entities;

namespace Gates.DAL.Map
{
    /// <summary>
    ///     Класс представляет маппер ProjectEntity <-> ProjectViewModel
    /// </summary>
    internal static class ProjectMapper
    {
        /// <summary>
        ///     Метод конвертирует ProjectViewModel в ProjectEntity
        /// </summary>
        /// <param name="projectViewModel">ProjectViewModel</param>
        /// <returns>ProjectEntity</returns>
        public static ProjectEntity Map(this ProjectViewModel projectViewModel)
        {
            return new ProjectEntity
            {
                AuthorId = projectViewModel.AuthorId,
                Title = projectViewModel.Title,
                CreateDate = projectViewModel.CreateDate
            };
        }

        /// <summary>
        ///     Метод конвертирует ProjectEntity в ProjectViewModel
        /// </summary>
        /// <param name="projectEntity">ProjectEntity</param>
        /// <returns>ProjectViewModel</returns>
        public static ProjectViewModel Map(this ProjectEntity projectEntity)
        {
            return new ProjectViewModel
            {
                AuthorId = projectEntity.Author?.UserName,
                Title = projectEntity.Title,
                CreateDate = projectEntity.CreateDate,
                ProjectId = projectEntity.Id
            };
        }
    }
}