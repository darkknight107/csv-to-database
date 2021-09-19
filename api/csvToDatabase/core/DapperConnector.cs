using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace core
{
    public class DapperConnector<TEntity> : IDbConnector<TEntity>
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly NpgsqlConnection _connection;
        private Dictionary<string, object> _dynamicParametersDictionary;

        public DapperConnector(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            _connection = new NpgsqlConnection(_connectionString);
            _dynamicParametersDictionary = new Dictionary<string, object>();
        }
        
        public async Task<IEnumerable<TEntity>> Query(ISpecification specification)
        {
            var parameters = specification.GetType().GetProperties();
            foreach (var parameter in parameters)
            {
                var value = parameter.GetValue(specification);
                _dynamicParametersDictionary.Add($"@{parameter.Name}", value);
            }
            
            var dynamicParameters = new DynamicParameters(_dynamicParametersDictionary);
            return await _connection.QueryAsync<TEntity>(specification.SqlCommand(), dynamicParameters);
        }
        
    }
}