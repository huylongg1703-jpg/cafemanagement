using CafeManagement.API.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace CafeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DatabaseController(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestDatabase()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                var result = await connection.QueryFirstAsync<int>(
                    "SELECT 1"
                );

                return Ok(new
                {
                    message = "Kết nối SQL Server bằng Dapper thành công!",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Kết nối Database thất bại!",
                    error = ex.Message
                });
            }
        }
    }
}