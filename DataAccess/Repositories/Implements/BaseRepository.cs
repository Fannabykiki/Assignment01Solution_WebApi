﻿using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        protected readonly PRN231_AS1Context _context;

        public BaseRepository( PRN231_AS1Context context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = _dbSet.Add(entity).Entity;

            return await Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return Task.FromResult(true);
        }

        public async Task<T>? GetAsync(Expression<Func<T, bool>>? predicate)
        {
            var result = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await result.FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? pre)
        {
            var result = predicate == null ? _dbSet : _dbSet.Include(pre).Where(predicate);

            return result.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllWithOdata(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? pre)
        {
            return await GetAllAsync(predicate,pre).ToListAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate)
        {
            return predicate == null ? _dbSet : _dbSet.Where(predicate);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = _dbSet.Update(entity).Entity;

            return await Task.FromResult(result);
        }
    }
}