using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? pre);
        Task<IEnumerable<T>> GetAllWithOdata(Expression<Func<T, bool>> predicate, Expression<Func<T,object>>? pre);
        Task<T>? GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        int SaveChanges();
    }
}
