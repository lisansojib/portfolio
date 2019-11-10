using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Interface for EfRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEfRepository<T> where T : IBaseEntity
    {
        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// GetSingleByExpr
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        T GetSingleByExpr(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        int Count();
        
        /// <summary>
        /// ListAll
        /// </summary>
        /// <returns></returns>
        List<T> ListAll();

        /// <summary>
        /// ListAll
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> ListAll(int page, int pageSize);

        /// <summary>
        /// ListAll
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="filterByExpressionModel"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> ListAll(int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order, out int count);

        /// <summary>
        /// ListAll
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="filterByExpressionModel"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> ListAll(Expression<Func<T, bool>> criteria, int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order, out int count);
        
        /// <summary>
        /// List
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IQueryable<T> List(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">Entity Value</param>
        /// <param name="tableName">Table Name</param>
        /// <returns></returns>
        T Add(T entity, string tableName);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// GetSingleByExprAsync
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<T> GetSingleByExprAsync(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// ExistsAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// ListAllAsync
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ListAllAsync();

        /// <summary>
        /// ListAllAsync
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> ListAllAsync(int page, int pageSize);

        /// <summary>
        /// ListAllAsync
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="filterByExpressionModel"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<List<T>> ListAllAsync(int offset, int limit, FilterByExpressionModel filterByExpressionModel, string sort, string order);
        
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="entity">Entity Value</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }
}
