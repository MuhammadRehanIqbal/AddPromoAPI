using Application.Abstraction.Persistence.Users;
using Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Persistence.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config) => _config = config;

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")); 
            string sql = @"SELECT u.user_id AS UserId,u.user_name AS UserName, 
                            u.first_name AS FirstName, 
                            u.last_name AS LastName, 
                            u.email AS Email, 
                            r.role_name AS Role
                            FROM users u
                            INNER JOIN user_roles ur ON ur.user_id = u.user_id
                            INNER JOIN roles r ON r.role_id = ur.role_id
                            WHERE u.email = @Email AND u.password = @Password;
                            ";

            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = username, Password = password });
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE user_id = @userId", new { userId });
        }

        public async Task<List<MenuItem>> GetMenusByRoleAsync(string role)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = @"SELECT m.label, m.route, m.icon, m.`order`
                   FROM roles r 
                   INNER JOIN role_menu rm ON rm.role_id = r.role_id
                   INNER JOIN menu m ON m.menu_id = rm.menu_id
                   WHERE r.role_name = @role_name;";

            var result = await conn.QueryAsync<MenuItem>(sql, new { role_name = role });
            return result.ToList(); 
        }
    }
}
