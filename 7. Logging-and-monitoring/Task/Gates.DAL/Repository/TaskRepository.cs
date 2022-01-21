using Gates.BL.Interfaces.Repository;
using Gates.BL.ViewModels;
using Gates.DAL.DataAccess;
using Gates.DAL.Entities;
using Gates.DAL.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NLog;

namespace Gates.DAL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly UserContext _dbContext;
        private readonly BaseEntityRepository<TaskEntity> _taskBaseRepository;
        private readonly BaseEntityRepository<StatusEntity> _statusBaseRepository;
        private readonly BaseEntityRepository<PriorityEntity> _priorityBaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskRepository(UserContext userContext, IMapper mapper, ILogger logger)
        {
            _dbContext = userContext;
            _logger = logger;
            _taskBaseRepository = new BaseEntityRepository<TaskEntity>(_dbContext, _logger);
            _statusBaseRepository = new BaseEntityRepository<StatusEntity>(_dbContext, _logger);
            _priorityBaseRepository = new BaseEntityRepository<PriorityEntity>(_dbContext, _logger);
            _mapper = mapper;
        }


        public bool AddSubTask()
        {
            _logger.Error("Add subtask not impleented.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод добавляет задачу в БД.
        /// </summary>
        /// <param name="item">Задача TaskViewModel.</param>
        /// <returns>true если задача успешно добавлена, иначе - false.</returns>
        public bool Create(TaskViewModel item)
        {
            try
            {
                var authorId = _dbContext.Users.FirstOrDefault(u => u.UserName == item.AuthorLogin)?.Id;
                var executorId = _dbContext.Users.FirstOrDefault(u => u.UserName == item.ExecutorLogin)?.Id;
                var taskEntity = item.Map();

                if (authorId == null || executorId == null)
                {
                    return false;
                }

                taskEntity.AuthorId = authorId;
                taskEntity.ExecutorId = executorId;
                taskEntity.StatusId = GetStatusId(item.Status);
                taskEntity.PriorityId = GetPriorityId(item.Priority);

                taskEntity.CreateDate = DateTime.Now;
                taskEntity.ProjectId = item.ProjectId;

                _logger.Debug($"Create task. " +
                    $"AuthorId: [{taskEntity.AuthorId}]. " +
                    $"ExecutorId: [{taskEntity.ExecutorId}]. " +
                    $"Status: [{item.Status}]. " +
                    $"Priority: [{item.Priority}]. " +
                    $"Creation date: [{taskEntity.CreateDate:dd/MM/yyyy}]. " +
                    $"Project id: [{taskEntity.ProjectId}]");

                return _taskBaseRepository.Create(taskEntity);
            }
            catch (Exception ex)
            {
                _logger.Error($"Create task error. {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Метод удаляет задачу по Id.
        /// </summary>
        /// <param name="id">Id задачи.</param>
        /// <returns>true если задача успешно удалена, иначе - false.</returns>
        public bool Delete(int id)
        {
            _logger.Debug($"Delete task. Task id: [{id}]");
            return _taskBaseRepository.Delete(id);
        }

        /// <summary>
        /// Метод возвращает TaskViewModel по Id.
        /// </summary>
        /// <param name="id">Id задачи.</param>
        /// <returns>TaskViewModel</returns>
        public TaskViewModel Get(int id)
        {
            var taskEntity = _taskBaseRepository.Get(id);
            var status = _statusBaseRepository.Get(taskEntity.StatusId);
            var priority = _priorityBaseRepository.Get(taskEntity.PriorityId);
            taskEntity.Status = status;
            taskEntity.Priority = priority;

            _logger.Debug($"Get task. Task id: [{id}]");

            return taskEntity.Map();
        }

        public List<TaskViewModel> GetList()
        {
            var result = _dbContext.Tasks.ToList().Select(t => _mapper.Map<TaskViewModel>(t)).ToList();

            _logger.Debug($"Get tasks. Tasks number: [{result?.Count}]");

            return result;
        }

        public ICollection<BoardTaskViewModel> GetTasksByUser(string loginId)
        {
            try
            {
                _logger.Debug($"Get tasks of a user. Username: [{loginId}]");

                var taskEntityList = _dbContext.Tasks.Where(t => t.Author.UserName == loginId).ToList();
                return taskEntityList.Select(b => b.MapToBoardTask()).ToList();
            }
            catch(Exception ex)
            {
                _logger.Error($"Get tasks of a user error. [{ex.Message}]");
                return null;
            }
        }

        public bool Update(TaskViewModel item)
        {
            try
            {
                var executorId = _dbContext.Users.FirstOrDefault(u => u.UserName == item.ExecutorLogin)?.Id;
                var taskEntityToModify = _taskBaseRepository.Get(item.TaskId);

                if (taskEntityToModify == null)
                {
                    return false;
                }

                taskEntityToModify.Title = item.Title;
                taskEntityToModify.Description = item.Description;
                taskEntityToModify.ExecutorId = executorId;
                taskEntityToModify.DeadlineDate = item.DeadlineDate;
                taskEntityToModify.ProjectId = item.ProjectId;
                taskEntityToModify.StatusId = GetStatusId(item.Status);
                taskEntityToModify.PriorityId = GetPriorityId(item.Priority);

                _logger.Debug($"Create task. " +
                    $"AuthorId: [{taskEntityToModify.AuthorId}]. " +
                    $"ExecutorId: [{taskEntityToModify.ExecutorId}]. " +
                    $"Status: [{item.Status}]. " +
                    $"Priority: [{item.Priority}]. " +
                    $"Creation date: [{taskEntityToModify.CreateDate:dd/MM/yyyy}]. " +
                    $"Project id: [{taskEntityToModify.ProjectId}]");

                return _taskBaseRepository.Update(taskEntityToModify);
            }
            catch(Exception ex)
            {
                _logger.Error($"Update task error. {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Метод возвращает коллекцию BoardTaskViewModel.
        /// </summary>
        /// <returns>Коллекция BoardTaskViewModel.</returns>
        public ICollection<BoardTaskViewModel> GetAllTasks()
        {
            try
            {
                _logger.Debug($"Get all tasks.");

                var tasks = _dbContext.Tasks
                .OrderByDescending(p => p.DeadlineDate)
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.Author)
                .ToList();

                return tasks
                    .Select(c => c.MapToBoardTask())
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Debug($"Get all tasks error. {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Метод возвращает id приоритета задачи по названию.
        /// </summary>
        /// <param name="priorityTitle"></param>
        /// <returns></returns>
        private int GetPriorityId(string priorityTitle)
        {
            var priorityId = _dbContext.Priority
                .FirstOrDefault(p => p.Title == priorityTitle)
                .Id;

            _logger.Debug($"Get priority id. Priority: [{priorityTitle}]. Id: [{priorityId}]");

            // Если приоритет не найден, выставляется значение по умолчанию.
            return priorityId == 0 ? 1 : priorityId;
        }

        /// <summary>
        /// Метод возвращает id статуса задачи по названию.
        /// </summary>
        /// <param name="statusTitle"></param>
        /// <returns></returns>
        private int GetStatusId(string statusTitle)
        {
            var statusId = _dbContext.Status
                .FirstOrDefault(p => p.Title == statusTitle)
                .Id;

            _logger.Debug($"Get status id. Status: [{statusTitle}]. Id: [{statusId}]");

            // Если статус не найден, выставляется значение по умолчанию.
            return statusId == 0 ? 1 : statusId;
        }

        /// <summary>
        /// Метод возвращает список задач пользователя, в которых он является исполнителем
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        public List<BoardTaskViewModel> GetAssignedTasks(string userName)
        {            
                var tasks = _dbContext.Tasks
                .Where(t => t.Executor.UserName == userName)
                .OrderBy(p => p.DeadlineDate)
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.Author)
                .Include(p => p.Project)
                .ToList();

            _logger.Debug($"Get assigned task to user. Username: [{userName}]. Task number: [{tasks?.Count}]");

            return tasks
                    .Select(c => c.MapToBoardTask())
                    .ToList();

        }
    }
}
