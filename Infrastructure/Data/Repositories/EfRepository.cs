using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IEfRepository<T> where T : class, IBaseEntity
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetSingleByExpr(Expression<Func<T, bool>> criteria)
        {
            return _dbSet.FirstOrDefault(criteria);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetSingleByExprAsync(Expression<Func<T, bool>> criteria)
        {
            return await _dbSet.FirstOrDefaultAsync(criteria);
        }

        public bool Exists(int id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public IQueryable<T> ListAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public List<T> ListAll(int page, int pageSize)
        {
            return _dbSet
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<T> ListAll(int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order, out int count)
        {
            try
            {
                count = 0;
                if (filterByExpressionModel == null && string.IsNullOrEmpty(sort))
                {
                    count = _dbSet.Count();
                    return _dbSet.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
                }
                else if (filterByExpressionModel == null && !string.IsNullOrEmpty(sort))
                {
                    var orderByExpression = $"{sort} {order}";
                    count = _dbSet.Count();
                    return _dbSet.OrderBy(orderByExpression).Skip(offset).Take(limit).ToList();
                }
                else if (filterByExpressionModel != null && string.IsNullOrEmpty(sort))
                {
                    var records = _dbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray());
                    count = records.Count();
                    return records.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
                }
                else
                {
                    var orderByExpression = $"{sort} {order}";
                    var records = _dbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray());
                    count = records.Count();
                    return records.OrderBy(orderByExpression).Skip(offset).Take(limit).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> ListAll(Expression<Func<T, bool>> criteria, int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order, out int count)
        {
            try
            {
                count = 0;
                var filteredDbSet = _dbSet.Where(criteria);
                if (filterByExpressionModel == null && string.IsNullOrEmpty(sort))
                {
                    count = filteredDbSet.Count();
                    return filteredDbSet.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
                }
                else if (filterByExpressionModel == null && !string.IsNullOrEmpty(sort))
                {
                    var orderByExpression = $"{sort} {order}";
                    count = filteredDbSet.Count();
                    return filteredDbSet.OrderBy(orderByExpression).Skip(offset).Take(limit).ToList();
                }
                else if (filterByExpressionModel != null && string.IsNullOrEmpty(sort))
                {
                    var records = filteredDbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray());
                    count = records.Count();
                    return records.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToList();
                }
                else
                {
                    var orderByExpression = $"{sort} {order}";
                    var records = filteredDbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray());
                    count = records.Count();
                    return records.OrderBy(orderByExpression).Skip(offset).Take(limit).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> ListAllAsync(int page, int pageSize)
        {
            return await _dbSet
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<T>> ListAllAsync(int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order)
        {
            try
            {
                if (filterByExpressionModel == null && string.IsNullOrEmpty(sort))
                {
                    return await _dbSet.OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToListAsync();
                }
                else if (filterByExpressionModel == null && !string.IsNullOrEmpty(sort))
                {
                    var orderByExpression = $"{sort} {order}";
                    return await _dbSet.OrderBy(orderByExpression).Skip(offset).Take(limit).ToListAsync();
                }
                else if (filterByExpressionModel != null && string.IsNullOrEmpty(sort))
                {
                    return await _dbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray()).OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToListAsync();
                }
                else
                {
                    var orderByExpression = $"{sort} {order}";
                    return await _dbSet.Where(filterByExpressionModel.Expression, filterByExpressionModel.Parameters.ToArray()).OrderBy(orderByExpression).Skip(offset).Take(limit).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> List(Expression<Func<T, bool>> criteria)
        {
            return _dbSet.Where(criteria).AsQueryable();
        }

        public T Add(T entity, string tableName)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity, string tableName)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
