using System.Web.Http;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using NLog;

namespace Gates.WebApp.Controllers
{
    /// <summary>
    /// Контроллер комментариев
    /// </summary>
    [Authorize]
    public class CommentController : ApiController
    {
        private readonly ICommentManager _iCommentManager;
        private readonly ILogger _logger;

        public CommentController(ICommentManager iCommentManager, ILogger logger)
        {
            _iCommentManager = iCommentManager;
            _logger = logger;
        }

        /// <summary>
        /// Метод возвращает комментарий
        /// </summary>
        /// <returns>Comment</returns>
        [HttpGet]
        public IHttpActionResult GetComment([FromUri] int commentId)
        {
            _logger.Info($"GetComment. CommentId: [{commentId}]. User: [{User.Identity.Name}]");

            var comment = _iCommentManager.GetComment(commentId);

            if (comment == null)
            {
                return BadRequest();
            }

            return Ok(comment);
        }

        /// <summary>
        /// Метод возвращает список комментариев задачи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCommentsTask([FromUri] int taskId)
        {
            _logger.Info($"GetCommentsTask. TaskId: [{taskId}]. User: [{User.Identity.Name}]");

            var commentsCollection = _iCommentManager.GetTaskComments(taskId);

            _logger.Info($"GetCommentsTask. Comments number: [{commentsCollection?.Count}]. User: [{User.Identity.Name}]");

            if (commentsCollection != null)
            {
                return Ok(commentsCollection);
            }

            return BadRequest();
        }

        /// <summary>
        /// Метод добавляет комментарий в БД
        /// </summary>
        /// <returns>Ok если комментарий добавлен, иначе - BadRequest</returns>
        [HttpPost]
        public IHttpActionResult AddComment([FromBody] CommentViewModel comment)
        {
            _logger.Info($"AddComment. User: [{User.Identity.Name}]");

            var result = _iCommentManager.AddComment(comment);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Метод изменяет комментарий
        /// </summary>
        /// <returns>Ok если комментарий изменен, иначе - BadRequest</returns>
        [HttpPut]
        public IHttpActionResult ChangeComment([FromBody] CommentViewModel comment)
        {
            _logger.Info($"ChangeComment. CommentId: [{comment?.CommentId}] User: [{User.Identity.Name}]");

            var result = _iCommentManager.ChangeComment(comment);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Метод удаляет комментарий
        /// </summary>
        /// <returns>Ok если комментарий удален, иначе - BadRequest</returns>
        [HttpDelete]
        public IHttpActionResult DeleteComment([FromUri] int commentId)
        {
            _logger.Info($"DeleteComment. CommentId: [{commentId}] User: [{User.Identity.Name}]");

            var result = _iCommentManager.DeleteComment(commentId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}