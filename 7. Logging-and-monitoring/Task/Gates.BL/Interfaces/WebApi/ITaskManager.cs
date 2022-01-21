using System;
using System.Collections.Generic;
using Gates.BL.ViewModels;

namespace Gates.BL.Interfaces.WebApi
{
    /// <summary>
    /// Интерфейс для WebApi.
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Метод возвращает задачу.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>Модель задачи для WebApi.</returns>
        TaskViewModel GetTask(int taskId);

        /// <summary>
        /// Метод добавляет задачу в БД.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi.</param>
        /// <returns>True если задача успешно добавлена, иначе - false.</returns>
        bool AddTask(TaskViewModel task);

        /// <summary>
        /// Метод меняет данные задачи.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi.</param>
        /// <returns>True если задача успешно изменена, иначе - false.</returns>
        bool UpdateTask(TaskViewModel task);

        /// <summary>
        /// Метод удаляет задачу.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        /// <returns>True если задача успешно удалена, иначе - false.</returns>
        bool DeleteTask(int taskId);

        /// <summary>
        /// Метод добавляет подзадачу к задаче.
        /// </summary>
        /// <param name="task">Модель задачи из WebApi, которая будет добавлена в виде подзадачи.</param>
        /// <param name="parenTaskId">Идентификатор задачи - родителя</param>
        /// <returns>True если подзадача успешно добавлена, иначе - false.</returns>
        bool AddSubTask(TaskViewModel task, int parenTaskId);

        /// <summary>
        /// Метод возвращает все подзадач из БД.
        /// </summary>
        /// <returns>Перечисление подзадач.</returns>
        List<BoardTaskViewModel> GetAllTasks();

        /// <summary>
        /// Метод возращает список задач пользователя
        /// </summary>
        /// <returns></returns>
        List<BoardTaskViewModel> GetTasksByUser(string userName);

        /// <summary>
        /// Метод возвращает список задач пользователя, в которых он является исполнителем
        /// </summary>
        List<BoardTaskViewModel> GetAssignedTasks(string userName);

    }
}
