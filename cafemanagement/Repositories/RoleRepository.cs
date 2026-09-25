using CafeManagement.API.Data;
using CafeManagement.API.Models;
using Dapper;

namespace CafeManagement.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public RoleRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                SELECT RoleId, RoleName, Description, IsDeleted
                FROM Roles
                WHERE IsDeleted = 0
                ORDER BY RoleId
                """;

            return await connection.QueryAsync<Role>(sql);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                SELECT RoleId, RoleName, Description, IsDeleted
                FROM Roles
                WHERE RoleId = @Id
                  AND IsDeleted = 0
                """;

            return await connection.QueryFirstOrDefaultAsync<Role>(
                sql,
                new { Id = id });
        }

        public async Task<int> CreateAsync(Role role)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                INSERT INTO Roles
                    (RoleName, Description, IsDeleted)
                VALUES
                    (@RoleName, @Description, 0);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
                """;

            return await connection.ExecuteScalarAsync<int>(sql, role);
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                UPDATE Roles
                SET RoleName = @RoleName,
                    Description = @Description
                WHERE RoleId = @RoleId
                  AND IsDeleted = 0
                """;

            var rows = await connection.ExecuteAsync(sql, role);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
                UPDATE Roles
                SET IsDeleted = 1
                WHERE RoleId = @Id
                """;

            var rows = await connection.ExecuteAsync(
                sql,
                new { Id = id });

            return rows > 0;
        }
    }
}
