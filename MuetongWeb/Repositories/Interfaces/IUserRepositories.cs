using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IUserRepositories
    {
        Task<User?> GetLogin(string username, string password);
        Task<int> CountAsync(string? query);
        Task<IEnumerable<User>> GetAsync();
        Task<IEnumerable<User>> GetAsync(string? query, int page, int pageSize);
        Task<User?> GetAsync(long id);
        Task<IEnumerable<User>> GetListAsync(long[] userIds);
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(long id);
        Task<bool> CheckPasswordAsync(long id, string password);
        Task<bool> ChangePasswordAsync(long id, string password);
    }
}
