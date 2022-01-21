using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.Interfaces.Repository;
using Gates.BL.ViewModels;
using AutoMapper;
using NLog;

namespace Gates.BL.Managers
{
    /// <summary>
    /// Менеджер задач.
    /// </summary>
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _TaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskManager(ITaskRepository TaskRepository, IMapper mapper, ILogger logger)
        {
            _TaskRepository = TaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Метод добавляет подзадачу к задаче.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi, которая будет добавлена в виде подзадачи.</param>
        /// <param name="parenTaskId">Идентификатор задачи - родителя</param>
        /// <returns>True если подзадача успешно добавлена, иначе - false.</returns>
        public bool AddSubTask(TaskViewModel task, int parenTaskId)
        {
            _logger.Error($"Add sub task. Not implemented.");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод добавляет задачу в БД.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi.</param>
        /// <returns>True если задача успешно добавлена, иначе - false.</returns>
        public bool AddTask(TaskViewModel task)
        {
            _logger.Info($"Add task.");

            return _TaskRepository.Create(task);
        }

        /// <summary>
        /// Метод меняет данные задачи.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi.</param>
        /// <returns>True если задача успешно изменена, иначе - false.</returns>
        public bool UpdateTask(TaskViewModel task)
        {
            _logger.Info($"Update task.");

            return _TaskRepository.Update(task);
        }

        /// <summary>
        /// Метод удаляет задачу.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>True если задача успешно удалена, иначе - false.</returns>
        public bool DeleteTask(int taskId)
        {
            _logger.Info($"Delete task. Task id: [{taskId}]");

            return _TaskRepository.Delete(taskId);
        }

        /// <summary>
        /// Метод возвращает все подзадач из БД.
        /// </summary>
        /// <returns>Коллекция подзадач.</returns>
        public List<BoardTaskViewModel> GetAllTasks()
        {
            _logger.Info($"Get all tasks.");

            return _TaskRepository.GetAllTasks()?.ToList();
        }

        /// <summary>
        /// Метод возвращает задачу.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>Модель задачи для WebApi.</returns>
        public TaskViewModel GetTask(int taskId)
        {
            _logger.Info($"Get task. Task id: [{taskId}]");

            return _TaskRepository.Get(taskId);
        }

        public List<BoardTaskViewModel> GetTasksByUser(string userName)
        {
            _logger.Info($"Get tasks of a user. Username: [{userName}]");

            return _TaskRepository.GetTasksByUser(userName).ToList();
        }

        /// <summary>
        /// Метод возвращает список задач пользователя, в которых он является исполнителем
        /// </summary>
        public List<BoardTaskViewModel> GetAssignedTasks(string userName) 
        {
            _logger.Info($"Get tasks assigned to user. Username: [{userName}]");

            return _TaskRepository.GetAssignedTasks(userName).ToList();
        }
    }
}
