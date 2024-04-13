using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public abstract class Repository<T, TPrimaryKey> : ReadRepository<T, TPrimaryKey>, IRepository<T, TPrimaryKey> where T : class, IEntity<TPrimaryKey>
    {
        protected Repository(DbContext context) : base(context)
        { 
        
        }
        public T Add(T entity)
        {
            var objToReturn = Context.Set<T>().Add(entity);
            return objToReturn.Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await Context.Set<T>().AddAsync(entity)).Entity;
        }

        public void AddRange(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;// Чтобы изменения сохранились, нам явным образом надо установить для его состояния значение EntityState.Modified
                                                               //  объект получаем из базы данных в пределах одного контекста, а пытаемся изменить в другом контексте
        }
    }
}
