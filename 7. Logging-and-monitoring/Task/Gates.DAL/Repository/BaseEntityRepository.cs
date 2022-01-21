using Gates.BL.Interfaces.Repository;
using Gates.DAL.DataAccess;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.DAL.Repository
{
    /// <summary>
    /// Базовый класс с методами работы с бд
    /// </summary>
    /// <typeparam name="TEntity">Сущность Entity</typeparam>
    internal class BaseEntityRepository<TEntity>
        where TEntity : class
    {
        private readonly UserContext _dbContext;
        private readonly ILogger _logger;

        public BaseEntityRepository(UserContext context, ILogger logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        /// <summary>
        /// Добавление ресурса в БД.
        /// </summary>
        /// <param name="item">Ресурс.</param>
        /// <returns>true если ресурс успешно добавлен, иначе - false.</returns>
        public bool Create(TEntity item)
        {
            try
            {
                _logger.Debug($"[{GetType()}]. Create.");

                _dbContext.Set<TEntity>().Add(item);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"[{GetType()}]. Create. Error Message: {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// Метод удаляет ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>true если ресурс успешно  удален, иначе - false.</returns>
        private bool DeleteObj(object id)
        {
            try
            {
                _logger.Debug($"[{GetType()}]. Delete.");

                var entity = _dbContext.Set<TEntity>().Find(id);

                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    _dbContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"[{GetType()}]. Delete. Error Message: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Метод удаляет ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>true если ресурс успешно  удален, иначе - false.</returns>
        public bool Delete(int id)
        {
            return DeleteObj(id);
        }

        /// <summary>
        /// Метод удаляет ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>true если ресурс успешно  удален, иначе - false.</returns>
        public bool Delete(string id)
        {
            return DeleteObj(id);
        }

        /// <summary>
        /// Метод извлекает ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>Ресурс.</returns>
        private TEntity GetObj(object id)
        {
            try
            {
                _logger.Debug($"[{GetType()}]. Get.");

                return _dbContext.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                _logger.Error($"[{GetType()}]. Get. Error Message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Метод извлекает ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>Ресурс.</returns>
        public TEntity Get(int id)
        {
           return GetObj(id);
        }

        /// <summary>
        /// Метод извлекает ресурс из БД.
        /// </summary>
        /// <param name="id">Id ресурса.</param>
        /// <returns>Ресурс.</returns>
        public TEntity Get(string id)
        {
            return GetObj(id);
        }

        /// <summary>
        /// Метод возвращает список ресурсов из БД.
        /// </summary>
        /// <returns>Список ресурсов.</returns>
        public List<TEntity> GetList()
        {
            try
            {
                _logger.Debug($"[{GetType()}]. GetList.");
                return _dbContext.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"[{GetType()}]. GetList. Error Message: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Метод обновляет ресурс.
        /// </summary>
        /// <param name="item">Ресурс.</param>
        /// <returns>true если ресурс успешно обновлен, иначе - false.</returns>
        public bool Update(TEntity item)
        {
            try
            {
                _logger.Debug($"[{GetType()}]. Update.");

                _dbContext.Entry<TEntity>(item).State = EntityState.Modified;
                _dbContext.SaveChanges();

            return true;
            }

            catch (Exception ex)
            {
                _logger.Debug($"[{GetType()}]. Update. Error Message: {ex}");
                return false;
            }
        }
    }
}
