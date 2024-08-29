using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.DbAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        #region Load Data Using Procedure
        public async Task<List<T>> LoadDataUsingProcedure<T, U>(string storedProcedure,
           U paramiters,
           string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            var data = await connection.QueryAsync<T>(storedProcedure, paramiters,
                commandType: CommandType.StoredProcedure);

            return data.ToList();
        }

        public async Task SaveDataUsingProcedure<T>(string storedProcedure,
           T paramiters,
           string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(storedProcedure, paramiters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateDataUsingProcedure<T, U>(string storedProcedure,
          U paramiters,
          string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            var effectedRowCount = await connection.ExecuteAsync(storedProcedure, paramiters,
                commandType: CommandType.StoredProcedure);

            return effectedRowCount;
        }

        public async Task<T> SaveDataUsingProcedureAndReturnId<T, U>(string storedProcedure,
          U paramiters,
          string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.ExecuteScalarAsync<T>(storedProcedure, paramiters,
                commandType: CommandType.StoredProcedure);
        }
        public async Task<int> CountDataUsingProcedure(string storedProcedure, string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.ExecuteScalarAsync<int>(storedProcedure,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SaveDataSetUsingProcedure(string storedProcedure, DataTable dataTable,
            string typeName = "", string connectionId = "Default")
        {
            var p = new
            {
                items = dataTable.AsTableValuedParameter(typeName)
            };

            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.ExecuteAsync(storedProcedure, p,
                 commandType: CommandType.StoredProcedure);
            }
        }
        #endregion End load data using procedure

        #region Data Access using Query
        public async Task<List<T>> LoadDataUsingQuery<T, U>(string sqlQuery,
           U paramiters, string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                var data = await connection.QueryAsync<T>(sqlQuery, paramiters);

                return data.ToList();
            }
        }

        public async Task SaveDataUsingQuery<T>(string sqlQuery,
         T paramiters,
         string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(sqlQuery, paramiters);
        }

        public async Task<T> SaveDataUsingQueryAndReturnId<T, U>(string sqlQuery,
          U paramiters,
          string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.ExecuteScalarAsync<T>(sqlQuery, paramiters);
        }
        #endregion End Data Access using query

    }
}
