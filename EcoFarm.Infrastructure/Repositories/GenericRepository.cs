using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Interfaces;
using EcoFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAuditableEntity, new()
    {
        protected readonly EcoContext _ecoContext;

        public GenericRepository(EcoContext EcoContext)
        {
            _ecoContext = EcoContext;
        }

        public IQueryable<T> GetQueryable()
        {
            return _ecoContext.Set<T>().AsNoTracking().Where(x => !x.IS_DELETE);
        }

        public IQueryable<T> GetTrackingQueryable()
        {
            return _ecoContext.Set<T>().Where(x => !x.IS_DELETE);
        }

        public IQueryable<T> GetQueryableIncludeIsDelete()
        {
            return _ecoContext.Set<T>().AsNoTracking();
        }

        public async Task<T> FindAsync(string id)
        {
            return await GetQueryable().FirstOrDefaultAsync(x => x.ID.Equals(id));
        }

        public T Find(string id)
        {
            return this.GetQueryable().FirstOrDefault(x => x.ID.Equals(id));
        }

        public void Add(T entity)
        {

            _ecoContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _ecoContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _ecoContext.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _ecoContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            //if (entity != null && entity.VERSION > 9)
            _ecoContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _ecoContext.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _ecoContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _ecoContext.Set<T>().RemoveRange(entities);
        }

        public void RemoveRange(Expression<Func<T, bool>> predicate)
        {
            var entities = _ecoContext.Set<T>().Where(predicate);
            _ecoContext.Set<T>().RemoveRange(entities);
        }

        public async Task<int> CountAsync()
        {
            return await this.GetQueryable().CountAsync();
        }

        public async Task<int> CountIncludeIsDeleteAsync()
        {
            return await _ecoContext.Set<T>().CountAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _ecoContext.SaveChangesAsync(cancellationToken);
        }
    }
}
