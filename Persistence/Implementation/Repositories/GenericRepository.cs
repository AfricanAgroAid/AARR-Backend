using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected ApplicationContext _context;
    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> exp)
    {
        var entity = await _context.Set<T>().SingleOrDefaultAsync(exp);
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> exp)
    {
        var entities = await _context.Set<T>().Where(exp).ToListAsync();
        return entities;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);

        return entity;
    }

    public  bool DeleteAsync(T entity)
    {
        _context.Remove(entity);
        return true;
    }
    public async Task<int> SaveChanges()
    {
       var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> exp)
    {
        var result =  await _context.Set<T>().AnyAsync(exp);
        return result;
    }
}
