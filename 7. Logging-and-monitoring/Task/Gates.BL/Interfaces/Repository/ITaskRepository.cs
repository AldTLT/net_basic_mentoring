using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gates.BL.ViewModels;

namespace Gates.BL.Interfaces.Repository
{
    public interface ITaskRepository : IRepository<TaskViewModel>
    {

        /// <summary>
        /// Получает список всех задач.
        /// </summary>
        /// <returns>Список всех задач</returns>
        ICollection<BoardTaskViewModel> GetAllTasks();

        /// <summary>
        /// Добавляет подзадачу к задаче.
        /// </summary>
        /// <returns>Возращает True если подзадача успешно добавлена,иначе False</returns>
        bool AddSubTask();

        /// <summary>
        /// Возращает список задачей пользователя.
        /// </summary>
        /// <returns>Список задач для конкретного пользователя</returns>
        ICollection<BoardTaskViewModel> GetTasksByUser(string userName);

        /// <summary>
        /// Метод возвращает список задач пользователя, в которых он является исполнителем
        /// </summary>
        List<BoardTaskViewModel> GetAssignedTasks(string userName);
        

    }
}
