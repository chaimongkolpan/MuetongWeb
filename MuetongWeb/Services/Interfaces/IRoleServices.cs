using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<RoleModel> GetRole(bool editPermit = false);
        Task<RoleResponse?> GetRole(long id);
        Task<bool> UpdateAsync(RoleUpdateRequest request);
    }
}
