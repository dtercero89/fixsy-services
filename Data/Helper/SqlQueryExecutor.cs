using Dapper;
using Npgsql;

namespace FixsyWebApi.Data.Helper

{

    public class PostgreSqlQueryExecutor
    {
        private readonly string _connectionString;

        public PostgreSqlQueryExecutor(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FixsyDatabase");
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sqlQuery, parameters);
            }
        }

        public async Task<string> GetCorrelativeAsync(string correlativeCode, bool updateCorrelative)
        {
            string sqlQuery = "SELECT public.\"GetCorrelative\"(@CorrelativeCode, @UpdateCorrelative) AS NextCorrelative;";

            var parameters = new { CorrelativeCode = correlativeCode, UpdateCorrelative = updateCorrelative };
            var result = await ExecuteQueryAsync<string>(sqlQuery, parameters);

            return result.FirstOrDefault();
        }

        public async Task<int> ExecuteCommandAsync(string sqlQuery, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
    }
}
