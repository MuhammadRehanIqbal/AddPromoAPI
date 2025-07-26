using Core.Entities; 
using System.Security.Claims; 

namespace Application.Abstraction.Auth.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
