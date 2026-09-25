using Microsoft.Data.SqlClient;
using System.Data;

namespace CafeManagement.API.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            string connectionString =
                _configuration.GetConnectionString("CafeDatabase")
                ?? throw new Exception("Không tìm thấy Connection String.");

            return new SqlConnection(connectionString);
        }
    }
}
