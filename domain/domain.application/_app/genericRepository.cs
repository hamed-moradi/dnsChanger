using Dapper;
using domain.repository._app;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace domain.application._app {
    public interface IGenericRepository<T> {
        //Sync
        int Execute(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        IEnumerable<T> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = default, CommandType? commandType = default);
        int QueryExecute(string sql, object param, int? commandTimeout = default, CommandType? commandType = default);
        T QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        SqlMapper.GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);

        //Async
        Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<int> QueryExecuteAsync(string sql, object param, int? commandTimeout = default, CommandType? commandType = default);
        Task<T> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = default, CommandType? commandType = default);
    }
    public class GenericRepository<T>: IGenericRepository<T> {
        //Sync
        public int Execute(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return ConnectionKeeper.SqlConnection.Execute(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public IEnumerable<T> Query(string sql, object param, bool buffered, int? commandTimeout, CommandType? commandType) {
            return ConnectionKeeper.SqlConnection.Query<T>(sql, param, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }
        public int QueryExecute(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return ConnectionKeeper.SqlConnection.QueryFirstOrDefault<int>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public T QueryFirstOrDefault(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return ConnectionKeeper.SqlConnection.QueryFirstOrDefault<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public SqlMapper.GridReader QueryMultiple(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return ConnectionKeeper.SqlConnection.QueryMultiple(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }

        //Async
        public async Task<int> ExecuteAsync(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return await ConnectionKeeper.SqlConnection.ExecuteAsync(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public async Task<IEnumerable<T>> QueryAsync(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return await ConnectionKeeper.SqlConnection.QueryAsync<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public async Task<int> QueryExecuteAsync(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return await ConnectionKeeper.SqlConnection.QueryFirstOrDefaultAsync<int>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return await ConnectionKeeper.SqlConnection.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param, int? commandTimeout, CommandType? commandType) {
            return await ConnectionKeeper.SqlConnection.QueryMultipleAsync(sql, param, commandTimeout: commandTimeout, commandType: commandType);
        }
    }
}
