using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gates.DAL.Entities
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser.
    // Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.

    /// <summary>
    ///     Профиль пользователя
    /// </summary>
    public class ApplicationUserEntity : IdentityUser
    {
        /// <summary>
        ///     Аккаунт может создавать несколько проектов
        /// </summary>
        public ICollection<ProjectEntity> Projects { get; set; }

        /// <summary>
        ///     Таски в которых аккаунт является автором
        /// </summary>
        public ICollection<TaskEntity> TasksAuthor { get; set; }

        ///// <summary>
        ///// Таски в которых аккаунт является исполнителем
        ///// </summary>
        public ICollection<TaskEntity> TasksExecutor { get; set; }
    }
}