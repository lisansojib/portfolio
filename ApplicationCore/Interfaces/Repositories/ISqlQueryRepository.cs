using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Interface for SqlQuery Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISqlQueryRepository<T> where T : class
    {
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> GetData(string query, params object[] parameters);

        /// <summary>
        /// GetData
        /// </summary>
        /// <typeparam name="CT"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<CT> GetData<CT>(string query, params object[] parameters) where CT : class;

        /// <summary>
        /// GetIntData
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int GetIntData(string query);

        /// <summary>
        /// GetDecimalData
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        decimal GetDecimalData(string query);

        /// <summary>
        /// RunSqlCommand
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int RunSqlCommand(string query, params object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool GetBooleanData(string query);

        /// <summary>
        /// RunSqlCommandAsync
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> RunSqlCommandAsync(string query, params object[] parameters);
    }
}
