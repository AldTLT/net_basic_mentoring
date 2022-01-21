using System;
using System.Collections.Generic;
using System.Linq;
using Gates.BL.Interfaces.Repository;
using Gates.BL.ViewModels;
using Gates.DAL.DataAccess;
using Gates.DAL.Entities;
using Gates.DAL.Map;
using AutoMapper;
using NLog;

namespace Gates.DAL.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly UserContext _dbContext;
        private readonly BaseEntityRepository<ProjectEntity> _projectBaseRepository;
        private readonly BaseEntityRepository<TaskEntity> _taskBaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProjectRepository(UserContext context, ILogger logger)
        {
            _logger = logger;
            _dbContext = context;
            _projectBaseRepository = new BaseEntityRepository<ProjectEntity>(_dbContext, _logger);
            _taskBaseRepository = new BaseEntityRepository<TaskEntity>(_dbContext, _logger);
        }

        /// <summary>
        ///     Метод добавляет задачу к проекту.
        /// </summary>
        public bool AddTaskToProject(int taskId, int projectId)
        {
            try
            {
                _logger.Debug($"AddTaskToProject.");
                var project = _projectBaseRepository.Get(projectId);
                var task = _taskBaseRepository.Get(taskId);
                project.Tasks.Add(task);
                if (task.ProjectId != null) return false;
                task.ProjectId = projectId;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"AddTaskToProject. Error Message: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        ///     Метод добавляет проект в БД
        /// </summary>
        public bool Create(ProjectViewModel item)
        {
            _logger.Debug($"CreateProject.");

            var authorId = _dbContext.Users.FirstOrDefault(u => u.UserName == item.AuthorId)?.Id;

            if (authorId == null)
            {
                return false;
            }

            var projectEntity = item.Map();
            projectEntity.CreateDate = DateTime.Now;
            projectEntity.AuthorId = authorId;

            return _projectBaseRepository.Create(projectEntity);
        }

        /// <summary>
        ///     Метод удаляет проект из БД.
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                _logger.Debug($"DeleteProject.");

                var task = _dbContext.Tasks
                    .Where(t => t.ProjectId == id)
                    .Select(t => t).ToList();

                foreach (var t in task) t.ProjectId = null;
                _dbContext.SaveChanges();
                return _projectBaseRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteProject. Error Message: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        ///     Метод удаляет задачу из проекта.
        /// </summary>
        public bool DeleteTaskFromProject(int taskId, int projectId)
        {
            try
            {
                _logger.Debug($"DeleteTaskFromProject.");

                var project = _projectBaseRepository.Get(projectId);
                _dbContext.Entry(project).Collection("Tasks").Load();
                var task = _taskBaseRepository.Get(taskId);
                project.Tasks.Remove(task);
                task.ProjectId = null;

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteTaskFromProject. Error Message: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Метод возвращает выбранный проект
        /// </summary>
        /// <param name="Id">Идентификатор проекта</param>
        /// <returns>Выбранный проект для WebApi.</returns>
        public ProjectViewModel Get(int id)
        {
            try
            {
                _logger.Debug($"GetProject.");

                var project = _projectBaseRepository.Get(id).Map();
                return project;
            }

            catch (Exception ex)
            {
                _logger.Error($"GetProject. Error Message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Метод возвращает список проектов
        /// </summary>
        public List<ProjectViewModel> GetList()
        {
            try
            {
                _logger.Debug($"GetListProjects.");

                var projects = _dbContext.Projects
                    .ToList().Select(project => project.Map()).ToList();

                return projects;
            }

            catch (Exception ex)
            {
                _logger.Error($"GetListProjects. Error Message: {ex.Message}");

                return null;
            }
        }

        /// <summary>
        ///     Метод возвращает список проектов пользователя
        /// </summary>
        /// <param name="login">Идентификатор пользователя</param>
        public ICollection<ProjectViewModel> GetMyList(string login)
        {
            try
            {
                _logger.Debug($"GetUsersProjects.");

                var projects = _dbContext.Projects
                    .Where(p => p.Author.UserName == login)
                    .ToList().Select(project => project.Map()).ToList();

                return projects;
            }

            catch (Exception ex)
            {
                _logger.Error($"GetUsersProjects. Error Message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Метод возвращает список задач выбранного проекта
        /// </summary>
        public ICollection<TaskViewModel> GetTasksOfProject(int projectId)
        {
            try
            {
                _logger.Debug($"GetTasksOfProject.");

                var projectTasks = _dbContext.Tasks
                    .Where(t => t.ProjectId == projectId)
                    .ToList().Select(task => task.Map()).ToList();

                return projectTasks;
            }

            catch (Exception ex)
            {
                _logger.Error($"GetTasksOfProject. Error Message: {ex.Message}");
                return null;
            }
        }

        public bool Update(ProjectViewModel item)
        {
            _logger.Error($"UpdateProject. NotImplemented.");
            throw new NotImplementedException();
        }
    }
}