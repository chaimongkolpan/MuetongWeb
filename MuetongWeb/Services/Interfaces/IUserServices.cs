using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserInfoModel?> LoginAsync(LoginRequest request);
        Task<UserCollectionResponse?> GetUserAsync(UserRequest request);
        Task<UserDetailResponse?> GetUserAsync(long id);
        Task<(bool, string)> ChangePasswordAsync(long id, UserChangePasswordRequest request);
    }
}
