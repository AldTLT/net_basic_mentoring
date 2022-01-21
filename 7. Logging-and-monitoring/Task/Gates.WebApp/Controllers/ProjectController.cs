using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using Gates.WebApp.Infrastructure;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gates.WebApp.Controllers
{
    /// <summary>
    /// Контроллер проектов
    /// </summary>
    [Authorize]
    public class ProjectController : ApiController
    {
        private readonly IProjectManager _iProjectManager;
        private readonly ILogger _logger;

        public ProjectController(IProjectManager iProjectManager, ILogger logger)
        {
            _iProjectManager = iProjectManager;
            _logger = logger;
        }

        /// <summary>
        /// Метод возвращает список всех проектов
        /// </summary>
        /// <returns>Project</returns>
        [HttpGet]
        public IHttpActionResult GetAllProjects()
        {
            _logger.Info($"GetAllProjects. User: [{User.Identity.Name}]");

            var projects = _iProjectManager.GetAllProjects();

            _logger.Info($"GetAllProjects. Projects number: [{projects?.Count}]. User: [{User.Identity.Name}]");

            if (projects == null)
            {
                return BadRequest();
            }

            return Ok(projects);
        }

        /// <summary>
        /// Метод возвращает список проектов пользователя
        /// </summary>
        /// <param name="login">Идентификатор пользователя</param>
        /// <returns>Project</returns>
        [HttpGet]
        public IHttpActionResult GetMyProjects([FromUri] string login)
        {
            _logger.Info($"GetMyProjects. Username: [{login}]. User: [{User.Identity.Name}]");

            var projects = _iProjectManager.GetMyProjects(login);

            _logger.Info($"GetAllProjects. Projects number: [{projects?.Count}]. User: [{User.Identity.Name}]");

            if (projects == null)
            {
                return BadRequest();
            }

            return Ok(projects);
        }

        /// <summary>
        /// Метод возвращает список задач выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        [HttpGet]
        public IHttpActionResult GetTasksOfProject([FromUri] int projectId)
        {
            _logger.Info($"GetTasksOfProject. ProjectId: [{projectId}]. User: [{User.Identity.Name}]");

            var tasks = _iProjectManager.GetTasksOfProject(projectId);

            _logger.Info($"GetTasksOfProject. Tasks number: [{tasks.Count}]. User: [{User.Identity.Name}]");

            if (tasks == null)
            {
                return BadRequest();
            }

            return Ok(tasks);
        }

        /// <summary>
        /// Метод добавляет проект в БД.
        /// </summary>
        /// <returns>Ok если проект добавлен, иначе - BadRequest.</returns>
        [HttpPost]
        public IHttpActionResult AddProject([FromBody] ProjectViewModel project)
        {
            _logger.Info($"AddProject. User: [{User.Identity.Name}]");

            var result = _iProjectManager.AddProject(project);

            if (!result)
            {
                return BadRequest();
            }
            // Increase counter.
            Counter.Counters.Increment(Counters.ProjectCreation);

            return Ok(result);
        }

        /// <summary>
        /// Метод добавляет задачу к проекту.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет добавлена в проект.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно добавлена, иначе - false.</returns>
        [HttpPut]
        public IHttpActionResult AddTaskToProject( int taskId,  int projectId)
        {
            _logger.Info($"AddTaskToProject. TaskId: [{taskId}]. ProjectId: [{projectId}]. User: [{User.Identity.Name}]");

            var result = _iProjectManager.AddTaskToProject(taskId, projectId);
            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Метод удаляет задачу из проекта.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи, которая будет удалена из проекта.</param>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>True если задача успешно удалена, иначе - false.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteTaskFromProject( int taskId, int projectId)
        {
            _logger.Info($"DeleteTaskFromProject. TaskId: [{taskId}]. ProjectId: [{projectId}]. User: [{User.Identity.Name}]");

            var result = _iProjectManager.DeleteTaskFromProject(taskId, projectId);
            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Метод удаляет проект из БД.
        /// </summary>
        /// <returns>Ok если проект удален, иначе - BadRequest.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteProject([FromUri] int projectId)
        {
            _logger.Info($"DeleteProject. ProjectId: [{projectId}]. User: [{User.Identity.Name}]");

            var result = _iProjectManager.DeleteProject(projectId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Метод возвращает выбранный проект
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        [HttpGet]
        public IHttpActionResult GetProject([FromUri] int projectId)
        {
            _logger.Info($"GetProject. ProjectId: [{projectId}]. User: [{User.Identity.Name}]");

            var project = _iProjectManager.GetProject(projectId);

            if (project == null)
            {
                return BadRequest();
            }

            return Ok(project);
        }





    }
}
