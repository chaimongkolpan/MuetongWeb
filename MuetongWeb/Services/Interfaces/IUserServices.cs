using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Responses;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserInfoModel?> LoginAsync(LoginRequest request);
        Task<UserCollectionResponse?> GetUserAsync(UserRequest request);
        Task<UserDetailResponse?> GetUserAsync(long id);
        Task<(bool, string)> ChangePasswordAsync(long id, UserChangePasswordRequest request);

        Task<bool> AddAsync(UserAddRequest request);
        Task<bool> UpdateAsync(long id, UserUpdateRequest request);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Province>> GetProvince();
        Task<List<WorkLineResponse>> GetWorkLine();
        Task<List<DepartmentResponse>> GetDepartment(long id);
        Task<List<SubDepartmentResponse>> GetSubDepartment(long id);
    }
}
