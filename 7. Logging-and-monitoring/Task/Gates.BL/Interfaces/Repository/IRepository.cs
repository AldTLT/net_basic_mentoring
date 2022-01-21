using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.BL.Interfaces.Repository
{
    // Базовый репозиторий с базовыми методами.
    public interface IRepository<TViewModel>
        where TViewModel : class
    {
        /// <summary>
        /// Получение списка объектов
        /// </summary>
        /// <returns>Список всех сущностей указанных в T</returns>
        List<TViewModel> GetList();

        /// <summary>
        /// Получение объекта по Id
        /// </summary>
        /// <param name="id">Id объекта который мы хотим получить</param>
        /// <returns>Возращает объект T</returns>
        TViewModel Get(int id);

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="item">Имя объекта,который создаём</param>
        bool Create(TViewModel item);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id объекта,который удаляем</param>
        bool Delete(int id);        

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Название объекта,который обновляем</param>
        bool Update(TViewModel item);

        
    }
}
