using System.Linq.Expressions;

using Domain.Common.Contracts;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetAsync(Expression<Func<T, bool>> exp);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> exp);
    T UpdateAsync(T entity);
    Task<T> CreateAsync(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> exp);
    bool DeleteAsync(T entity);
    Task<int> SaveChanges();
}
