using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Auth.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<string> GenerateToken(string username);
        Task<bool> ValidateRefreshToken(string username, string refreshToken);
    }
}
