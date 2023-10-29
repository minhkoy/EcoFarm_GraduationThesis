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
        private readonly EcoContext _EcoContext;

        public GenericRepository(EcoContext EcoContext)
        {
            _EcoContext = EcoContext;
        }

        public IQueryable<T> GetQueryable()
        {
            return _EcoContext.Set<T>().AsNoTracking().Where(x => !x.IS_DELETE.HasValue || !x.IS_DELETE.Value);
        }

        public IQueryable<T> GetTrackingQueryable()
        {
            return _EcoContext.Set<T>().Where(x => !x.IS_DELETE.HasValue || !x.IS_DELETE.Value);
        }

        public IQueryable<T> GetQueryableIncludeIsDelete()
        {
            return _EcoContext.Set<T>().AsNoTracking();
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
            _EcoContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _EcoContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _EcoContext.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _EcoContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            //if (entity != null && entity.VERSION > 9)
            _EcoContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _EcoContext.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _EcoContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _EcoContext.Set<T>().RemoveRange(entities);
        }

        public void RemoveRange(Expression<Func<T, bool>> predicate)
        {
            var entities = _EcoContext.Set<T>().Where(predicate);
            _EcoContext.Set<T>().RemoveRange(entities);
        }

        public async Task<int> CountAsync()
        {
            return await this.GetQueryable().CountAsync();
        }

        public async Task<int> CountIncludeIsDeleteAsync()
        {
            return await _EcoContext.Set<T>().CountAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _EcoContext.SaveChangesAsync(cancellationToken);
        }
    }
}
