using EcoFarm.Application.Common.Extensions;
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
using TokenHandler.Interfaces;

namespace EcoFarm.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        protected readonly EcoContext _ecoContext;
        protected readonly IAuthService _authService; 

        public GenericRepository(EcoContext EcoContext, IAuthService authService)
        {
            _ecoContext = EcoContext;
            _authService = authService;
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
            //var user = _authService.GetUserInfoByToken().Result;
            var username = _authService.GetUsername();
            entity.CREATED_BY = username;
            entity.CREATED_TIME = DateTime.Now.ToVnDateTime();
            entity.MODIFIED_BY = username;
            entity.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            _ecoContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            //var user = await _authService.GetUserInfoByToken();
            var username = _authService.GetUsername();
            entity.CREATED_BY = username;
            entity.CREATED_TIME = DateTime.Now.ToVnDateTime();
            entity.MODIFIED_BY = username;
            entity.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            await _ecoContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(x =>
            {
                //var user = _authService.GetUserInfoByToken().Result;
                var username = _authService.GetUsername();
                x.CREATED_BY = username;
                x.CREATED_TIME = DateTime.Now.ToVnDateTime();
                x.MODIFIED_BY = username;
                x.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            });
            _ecoContext.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            entities.ToList().ForEach(x =>
            {
                //var user = await _authService.GetUserInfoByToken();
                var username = _authService.GetUsername();
                x.CREATED_BY = username;
                x.CREATED_TIME = DateTime.Now.ToVnDateTime();
                x.MODIFIED_BY = username;
                x.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            });
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
            var username = _authService.GetUsername();
            entity.IS_DELETE = true;
            entity.MODIFIED_BY = username;
            entity.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            _ecoContext.Set<T>().Update(entity);
            //_ecoContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(x =>
            {
                var username = _authService.GetUsername();
                x.IS_DELETE = true;
                x.MODIFIED_BY = username;
                x.MODIFIED_TIME = DateTime.Now.ToVnDateTime();
            });
            _ecoContext.Set<T>().UpdateRange(entities);
            //_ecoContext.Set<T>().RemoveRange(entities);
        }

        public void ForceRemove(T entity)
        {
            _ecoContext.Set<T>().Remove(entity);
        }

        public void ForceRemoveRange(IEnumerable<T> entities)
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
