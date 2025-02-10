using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDapperData.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfigurationManager _config;

        public DataAccess(IConfigurationManager config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T,P>(string query, P parameters, string connId = "con")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connId));

            return await connection.QueryAsync<T>(query, parameters); 
        }

        public async Task SaveDataAsync<P>(string query, P parameters, string connId = "con")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connId));

            await connection.ExecuteAsync(query, parameters);
        }
    }
}
