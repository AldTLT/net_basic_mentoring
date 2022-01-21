using Gates.BL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.BL.Interfaces.WebApi
{
    /// <summary>
    /// Интерфейс для WebApi.
    /// </summary>
    public interface IProjectManager
    {
        /// <summary>
        /// Метод возвращает список задач выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Коллекция задач выбранного проекта для WebApi.</returns>
        ICollection<TaskViewModel> GetTasksOfProject(int projectId);

        /// <summary>
        /// Метод возвращает все проекты пользователя.
        /// </summary>
        /// <param name="login">Идентификатор пользователя</param>
        /// <returns>Перечисление проектов.</returns>
        ICollection<ProjectViewModel> GetMyProjects(string login);

        /// <summary>
        /// Метод возвращает все проекты.
        /// </summary>
        /// <returns>Перечисление проектов.</returns>
        ICollection<ProjectViewModel> GetAllProjects();

        /// <summary>
        /// Метод добавляет проект в БД.
        /// </summary>
        /// <param name="project">Модель проекта из WebApi.</param>
        /// <returns>True если проект успешно добавлен, иначе - false.</returns>
        bool AddProject(ProjectViewModel project);

        /// <summary>
        /// Метод удаляет удаляет проект из БД.
        /// </summary>
        /// <param name="projectId">Идентификатор проекта.</param>
        /// <returns>True если проект успешно удален, иначе - false.</returns>
        bool DeleteProject(int projectId);

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

        /// <summary>
        /// Метод возвращает выбранный проект
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Выбранный проект для WebApi.</returns>
        ProjectViewModel GetProject(int projectId);

    }
}
