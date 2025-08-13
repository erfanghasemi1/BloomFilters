using Dapper;
using MySqlConnector;

namespace BloomFilters.Query
{
    public class LoginQuery
    {
        private readonly string _connectionString;

        public LoginQuery(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("mysqlconnection")!;
        }

        public bool CheckUserLogin(LoginModel request)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select uuid from login where username = @u and password = @p";

                Guid? id = connection.QueryFirstOrDefault<Guid?>(query, new
                {
                    u = request.username,
                    p = request.password
                });

                return id.HasValue;
            }
        }
    }
}
