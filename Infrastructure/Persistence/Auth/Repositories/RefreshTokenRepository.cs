using Application.Abstraction.Auth.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; 

namespace Infrastructure.Persistence.Auth.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IConfiguration _config;
        public RefreshTokenRepository(IConfiguration config) => _config = config;

        public async Task<string> GenerateToken(string username)
        {
            string token = Guid.NewGuid().ToString();
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = "INSERT INTO refresh_tokens (token_user, refresh_token, created_at) VALUES (@username, @token, NOW())";
            await conn.ExecuteAsync(sql, new { username, token });
            return token;
        }

        public async Task<bool> ValidateRefreshToken(string username, string refreshToken)
        {
            using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = "SELECT COUNT(1) FROM refresh_tokens WHERE users = @username AND refresh_token = @refreshToken";
            int count = await conn.ExecuteScalarAsync<int>(sql, new { username, refreshToken });
            return count > 0;
        }
    }

}
