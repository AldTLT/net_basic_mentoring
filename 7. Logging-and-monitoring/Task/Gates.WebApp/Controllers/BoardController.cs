using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gates.BL.Interfaces.WebApi;
using NLog;

namespace Gates.WebApp.Controllers
{
    /// <summary>
    /// Контроллер доски задач.
    /// </summary>
    [Authorize]
    public class BoardController : ApiController
    {
        private readonly ILogger _logger;
        private readonly ITaskManager _iTaskManager;

        public BoardController(ITaskManager iTaskManager, ILogger logger)
        {
            _logger = logger;
            _iTaskManager = iTaskManager;
        }

        /// <summary>
        /// Метод возвращает список задач.
        /// </summary>
        /// <returns>Список задач List<Task></returns>
        [HttpGet]
        public IHttpActionResult GetBoardTasks()
        {
            _logger.Info("GetBoardTasks");

            var boardTasks = _iTaskManager.GetAllTasks();

            _logger.Info($"GetBoardTasks. Number of Tasks: [{boardTasks?.Count}]. User: [{User.Identity.Name}]");

            if (boardTasks != null)
            {
                return Ok(boardTasks);
            }

            return BadRequest();
        }
    }
}