using Core.Entities; 

namespace Application.Abstraction.Persistence.Users
{
    public interface IUserRepository
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<MenuItem>> GetMenusByRoleAsync(string role);
    }
}
