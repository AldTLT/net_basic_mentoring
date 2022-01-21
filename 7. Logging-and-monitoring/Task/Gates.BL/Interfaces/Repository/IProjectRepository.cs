using Gates.BL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.BL.Interfaces.Repository
{
    public interface IProjectRepository : IRepository<ProjectViewModel>
    {
        /// <summary>
        /// Метод возвращает список задач выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Модель проекта для WebApi.</returns>
        ICollection<TaskViewModel> GetTasksOfProject(int projectId);

        /// <summary>
        /// Метод добавляет задачу к проекту.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет добавлена в проект.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно добавлена, иначе - false.</returns>
        bool AddTaskToProject(int taskId, int projectId);

        /// <summary>
        /// Метод удаляет задачу из проекта.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет удалена из проекта.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно удалена, иначе - false.</returns>
        bool DeleteTaskFromProject(int taskId, int projectId);
        

        ICollection<ProjectViewModel> GetMyList(string login);
    }
}
