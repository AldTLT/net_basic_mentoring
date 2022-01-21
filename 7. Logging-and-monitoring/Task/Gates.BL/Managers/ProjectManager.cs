using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using Gates.BL.Interfaces.Repository;
using System.Collections;
using NLog;

namespace Gates.BL.Managers
{
    /// <summary>
    /// Менеджер проектов
    /// </summary>
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger _logger;

        public ProjectManager(IProjectRepository projectRepository, ILogger logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        /// <summary>
        /// Метод добавляет проект в БД.
        /// </summary>
        /// <param name="project">Модель проекта из WebApi.</param>
        /// <returns>True если проект успешно добавлен, иначе - false.</returns>
        public bool AddProject(ProjectViewModel project)
        {
            _logger.Info($"Add project.");

            return _projectRepository.Create(project);
        }

        /// <summary>
        /// Метод добавляет задачу к проекту.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет добавлена в проект.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно добавлена, иначе - false.</returns>
        public bool AddTaskToProject(int taskId, int projectId)
        {
            _logger.Info($"Add task to project. Task id: [{taskId}. Project id: [{projectId}]]");

            return _projectRepository.AddTaskToProject(taskId, projectId);
        }

        /// <summary>
        /// Метод удаляет удаляет проект из БД.
        /// </summary>
        /// <param name="projectId">Идентификатор проекта.</param>
        /// <returns>True если проект успешно удален, иначе - false.</returns>
        public bool DeleteProject(int projectId)
        {
            _logger.Info($"Delete project. Project id: [{projectId}]]");

            return _projectRepository.Delete(projectId);
        }

        /// <summary>
        /// Метод удаляет задачу из проекта.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет удалена из проекта.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно удалена, иначе - false.</returns>
        public bool DeleteTaskFromProject(int taskId, int projectId)
        {
            _logger.Info($"Delete task from project. Task id: [{taskId}. Project id: [{projectId}]]");

            return _projectRepository.DeleteTaskFromProject(taskId, projectId);
        }

        /// <summary>
        /// Метод возвращает все проекты пользователя.
        /// </summary>
        /// <param name="login">Идентификатор пользователя</param>
        /// <returns>Перечисление проектов.</returns>
        public ICollection<ProjectViewModel> GetMyProjects(string login)
        {
            _logger.Info($"Get project of a user. Username: [{login}.");

            return _projectRepository.GetMyList(login);
        }

        /// <summary>
        /// Метод возвращает все проекты.
        /// </summary>
        /// <returns>Перечисление проектов.</returns>
        public ICollection<ProjectViewModel> GetAllProjects()
        {
            _logger.Info($"Get all projects.");

            return _projectRepository.GetList();
        }

        /// <summary>
        /// Метод возвращает список задач выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Модель проекта для WebApi.</returns>
        public ICollection<TaskViewModel> GetTasksOfProject(int projectId)
        {
            _logger.Info($"Get Tasks of project. Project id: [{projectId}]");

            return _projectRepository.GetTasksOfProject(projectId);
        }

        /// <summary>
        /// Метод возвращает выбранный проект
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>Выбранный проект для WebApi.</returns>
        public ProjectViewModel GetProject(int projectId)
        {
            _logger.Info($"Get project. Project id: [{projectId}]");

            return _projectRepository.Get(projectId);
        }
    }
}
