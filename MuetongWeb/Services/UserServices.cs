using MuetongWeb.Services.Interfaces;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Pages;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Responses;
using MuetongWeb.Constants;

namespace MuetongWeb.Services
{
    public class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IRolePermissionRepositories _rolePermissionRepositories;
        private readonly IUserRepositories _userRepositories;
        public UserServices
        (
            ILogger<UserServices> logger,
            IRolePermissionRepositories rolePermissionRepositories,
            IUserRepositories userRepositories
        )
        {
            _logger = logger;
            _rolePermissionRepositories = rolePermissionRepositories;
            _userRepositories = userRepositories;
        }
        public async Task<UserInfoModel?> LoginAsync(LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                    return null;
                var user = await _userRepositories.GetLogin(request.Username, EncryptionHelpers.Encrypt(request.Password));
                if (user == null)
                    return null;
                var permissions = user.Role.RolePermissions.Select(rolePermission => rolePermission.Permission).ToList();
                return new UserInfoModel(user, permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => LoginAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<UserCollectionResponse?> GetUserAsync(UserRequest request)
        {
            try
            {
                var users = await _userRepositories.GetAsync(request.Query, request.Page, request.PageSize);
                if (!users.Any())
                    return null;
                var count = await _userRepositories.CountAsync(request.Query);
                var response = new UserCollectionResponse(users, count, request.Page, request.PageSize);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetUserAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<UserDetailResponse?> GetUserAsync(long id)
        {
            try
            {
                var user = await _userRepositories.GetAsync(id);
                if (user == null)
                    return null;
                var response = new UserDetailResponse(user);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetUserAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<(bool, string)> ChangePasswordAsync(long id, UserChangePasswordRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.OldPassword) || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
                    return (false, MessageConstants.UserChangePasswordRequestInvalid);
                if (!await _userRepositories.CheckPasswordAsync(id, EncryptionHelpers.Encrypt(request.OldPassword))) 
                    return (false, MessageConstants.UserChangePasswordRequestWrong);
                if (request.NewPassword != request.ConfirmPassword)
                    return (false, MessageConstants.UserChangePasswordRequestNotMatch);
                var result = await _userRepositories.ChangePasswordAsync(id, EncryptionHelpers.Encrypt(request.NewPassword));
                return (result, (result ? MessageConstants.UserChangePasswordSuccess : MessageConstants.UserChangePasswordFail));
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => ChangePasswordAsync: " + ex.Message);
                throw;
            }
        }
    }
}
