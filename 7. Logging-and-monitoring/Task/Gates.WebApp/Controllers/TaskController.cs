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
using System.Web.Http.Description;

namespace Gates.WebApp.Controllers
{
    /// <summary>
    /// Контроллер задач.
    /// </summary>
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly ITaskManager _iTaskManager;
        private readonly ILogger _logger;

        public TaskController(ITaskManager iTaskManager, ILogger logger)
        {
            _iTaskManager = iTaskManager;
            _logger = logger;
        }

        /// <summary>
        /// Метод возвращает список задач.
        /// </summary>
        /// <returns>Task</returns>
        [HttpGet]
        public IHttpActionResult GetTask([FromUri] int taskId)
        {
            _logger.Info($"GetTask. TaskId: [{taskId}]. User: [{User.Identity.Name}]");

            var task = _iTaskManager.GetTask(taskId);

            if (task == null)
            {
                return BadRequest();
            }

            return Ok(task);
        }

        /// <summary>
        /// Метод добавляет задачу в БД.
        /// </summary>
        /// <returns>Ok если задача добавлена, иначе - BadRequest.</returns>
        [HttpPost]
        public IHttpActionResult AddTask([FromBody] TaskViewModel task)
        {
            _logger.Info($"AddTask. User: [{User.Identity.Name}]");

            var result = _iTaskManager.AddTask(task);

            if (!result)
            {
                return BadRequest();
            }

            // Increase counter.
            Counter.Counters.Increment(Counters.TasksCreation);

            return Ok(result);
        }

        /// <summary>
        /// Метод изменяет задачу.
        /// </summary>
        /// <returns>Ok если задача изменена, иначе - BadRequest.</returns>
        [HttpPut]
        public IHttpActionResult ChangeTask([FromBody] TaskViewModel task)
        {
            _logger.Info($"ChangeTask. User: [{User.Identity.Name}]");

            var result = _iTaskManager.UpdateTask(task);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Метод удаляет задачу.
        /// </summary>
        /// <returns>Ok если задача удалена, иначе - BadRequest.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteTask([FromUri] int taskId)
        {
            _logger.Info($"DeleteTask. TaskId: [{taskId}]. User: [{User.Identity.Name}]");

            var result = _iTaskManager.DeleteTask(taskId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Метод добавляет подзадачу к задаче.
        /// </summary>
        /// <returns>Ok если подзадача добавлена, иначе - BadRequest.</returns>
        [HttpPut]
        public IHttpActionResult AddSubTask([FromBody] TaskViewModel task, int parentTaskId)
        {
            _logger.Info($"AddSubTask. Parent task id: [{parentTaskId}]. User: [{User.Identity.Name}]");

            var result = _iTaskManager.AddSubTask(task, parentTaskId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetTasksByUser([FromUri] string loginId)
        {
            _logger.Info($"GetTasksByUser. Username: [{loginId}]. User: [{User.Identity.Name}]");

            var result = _iTaskManager.GetTasksByUser(loginId);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <summary>
        /// Метод возвращает список задач пользователя, в которых он является исполнителем
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        [HttpGet]
        public IHttpActionResult GetAssignedTasks ([FromUri] string userName)
        {
            _logger.Info($"GetAssignedTasks. Username: [{userName}]. User: [{User.Identity.Name}]");

            var result = _iTaskManager.GetAssignedTasks(userName);

            return Ok(result);
        }
    }
}