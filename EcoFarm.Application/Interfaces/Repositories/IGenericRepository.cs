using System.Linq.Expressions;
using EcoFarm.Domain.Common.Interfaces;

namespace EcoFarm.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetQueryable();
    IQueryable<T> GetTrackingQueryable();
    IQueryable<T> GetQueryableIncludeIsDelete();

    Task<T> FindAsync(string id);
    T Find(string id);

    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void RemoveRange(Expression<Func<T, bool>> predicate);

    Task<int> CountAsync();
    Task<int> CountIncludeIsDeleteAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}