using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория, предназначенного для чтения
    /// </summary>
    /// <typeparam name="T">Тип Entity для репозитория</typeparam>
    /// <typeparam name="TPrimaryKey">тип первичного ключа</typeparam>
    public interface IReadRepository<T, TPrimaryKey> : IRepository where T : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Запросить все сущности в базе
        /// </summary>
        /// <returns>IQueryable массив сущностей</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Запросить все сущности в базе
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>сущность</returns>
        T Get(TPrimaryKey id);

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>сущность</returns>
        Task<T> GetAsync(TPrimaryKey id);
    }
}
