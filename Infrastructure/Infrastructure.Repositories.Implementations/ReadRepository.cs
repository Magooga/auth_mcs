using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий для чтения
    /// </summary>
    /// <typeparam name="T">тип сущности</typeparam>
    /// <typeparam name="TPrimaryKey">Основной ключ</typeparam>
    public abstract class ReadRepository<T, TPrimaryKey> : IReadRepository<T, TPrimaryKey> where T : class, IEntity<TPrimaryKey>
    {
        protected readonly DatabaseContext Context;
        protected DbSet<T> EntitySet;

        protected ReadRepository(DatabaseContext context)
        {
            Context = context;
            EntitySet = Context.Set<T>();
        }

        public virtual T Get(TPrimaryKey id)
        {
            return EntitySet.Find(id);
        }

        /// <summary>
        /// Запросить все сущности в базе
        /// </summary>
        /// <returns>IQueryable массив сущностей</returns>
        public virtual IQueryable<T> GetAll()
        {
            //asNoTracking? EntitySet.AsNoTracking() : EntitySet; // esli est parametr otslegivaemi/ne otslegivaemi zapros dlia db  
            return EntitySet;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(TPrimaryKey id)
        {
            return await EntitySet.FindAsync((object)id);
        }
    }

}
