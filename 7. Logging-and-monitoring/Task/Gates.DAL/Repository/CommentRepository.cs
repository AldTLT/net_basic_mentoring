using System;
using System.Collections.Generic;
using System.Linq;
using Gates.BL.Interfaces.Repository;
using Gates.BL.ViewModels;
using Gates.DAL.DataAccess;
using Gates.DAL.Entities;
using Gates.DAL.Map;
using NLog;

namespace Gates.DAL.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BaseEntityRepository<ApplicationUserEntity> _accountBaseRepository;
        private readonly BaseEntityRepository<CommentEntity> _commentBaseRepository;
        private readonly UserContext _dbContext;
        private readonly BaseEntityRepository<TaskEntity> _taskBaseRepository;
        private readonly ILogger _logger;

        public CommentRepository(UserContext context, ILogger logger)
        {
            _dbContext = context;
            _logger = logger;
            _commentBaseRepository = new BaseEntityRepository<CommentEntity>(_dbContext, _logger);
            _accountBaseRepository = new BaseEntityRepository<ApplicationUserEntity>(_dbContext, _logger);
            _taskBaseRepository = new BaseEntityRepository<TaskEntity>(_dbContext, _logger);
        }

        /// <summary>
        /// Метод возвращает список комментариев задачи
        /// </summary>
        public ICollection<CommentViewModel> GetTaskComments(int taskId)
        {
            try
            {
                _logger.Debug($"GetTaskComments.");

                var commentsEntity = _dbContext.Comments
                    .Where(c => c.TaskId == taskId).OrderByDescending(c => c.CreateDate).ToList();

                return commentsEntity?.Select(c => c.Map()).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"GetTaskComments. Error Message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Метод изменяет комментарий
        /// </summary>
        public bool Update(CommentViewModel item)
        {
            _logger.Debug($"UpdateComment.");

            var comment = _commentBaseRepository.Get(item.CommentId);
            if (comment == null)
            {
                return false;
            }
            comment.Content = item.Content;

            return _commentBaseRepository.Update(comment);
        }

        /// <summary>
        /// Метод возвращает комментарий
        /// </summary>
        public CommentViewModel Get(int id)
        {
            _logger.Debug($"GetComment.");

            return _commentBaseRepository.Get(id).Map();
        }

        /// <summary>
        /// Метод добавляет комментарий в БД
        /// </summary>
        public bool Create(CommentViewModel item)
        {
            _logger.Debug($"CreateComment.");

            var author = _dbContext.Users.FirstOrDefault(u => u.UserName == item.Login);
            if (author == null) 
            {
                return false;
            }

            var commentEntity = item.Map();
            commentEntity.Author = author;
            commentEntity.CreateDate = DateTime.Now;

            return _commentBaseRepository.Create(commentEntity);
        }

        /// <summary>
        /// Метод удаляет комментарий
        /// </summary>
        public bool Delete(int id)
        {
            _logger.Debug($"DeleteComment.");

            return _commentBaseRepository.Delete(id);
        }

        public List<CommentViewModel> GetList()
        {
            try
            {
                _logger.Debug($"GetListComments.");

                var taskComments = _dbContext.Comments
                    .Select(c => c.Map()).ToList();

                return taskComments;
            }

            catch (Exception ex)
            {
                _logger.Error($"GetListComments. Error Message: {ex.Message}");
                return null;
            }
        }
    }
}