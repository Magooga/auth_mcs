using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    public interface ICompositeRepository<T, TPrimaryKey> where T : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Получить сущность по ID если ключ композитный из двух значений
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        T Get(TPrimaryKey id1, TPrimaryKey id2);

        /// <summary>
        /// Получить сущность по ID если ключ композитный из двух значений
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        Task<T> GetAsync(TPrimaryKey id1, TPrimaryKey id2);
    }
}
