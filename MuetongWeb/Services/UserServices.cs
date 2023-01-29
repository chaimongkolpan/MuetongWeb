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
        private readonly IProvinceRepositories _provinceRepositories;
        private readonly ILineRepositories _lineRepositories;
        private readonly IDepartmentRepositories _departmentRepositories;
        private readonly ISubDepartmentRepositories _subDepartmentRepositories;
        public UserServices
        (
            ILogger<UserServices> logger,
            IRolePermissionRepositories rolePermissionRepositories,
            IUserRepositories userRepositories,
            IProvinceRepositories provinceRepositories,
            ILineRepositories lineRepositories,
            IDepartmentRepositories departmentRepositories,
            ISubDepartmentRepositories subDepartmentRepositories
        )
        {
            _logger = logger;
            _rolePermissionRepositories = rolePermissionRepositories;
            _userRepositories = userRepositories;
            _provinceRepositories = provinceRepositories;
            _lineRepositories = lineRepositories;
            _departmentRepositories = departmentRepositories;
            _subDepartmentRepositories = subDepartmentRepositories;
        }
        #region Page Service
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
        #endregion
        #region Api Service
        public async Task<bool> AddAsync(UserAddRequest request)
        {
            var user = new User()
            {
                Username = request.Username,
                Password = EncryptionHelpers.Encrypt(request.Username),
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Address = request.Address,
                ProvinceId = request.ProvinceId,
                PhoneNo = request.Phone,
                Email = request.Email,
                SubDepartmentId= request.SubDepartmentId,
                RoleId = request.RoleId,
                UserId = request.User.Id,
                CreateDate = DateTime.Now,
                EmployeeId = request.EmployeeId,
                CitizenId = request.Idcard
            };
            await _userRepositories.AddAsync(user);
            return true;
        }
        public async Task<bool> UpdateAsync(long id, UserUpdateRequest request)
        {
            var user = await _userRepositories.GetAsync(id);
            if (user == null)
                return false;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.Address = request.Address;
            user.ProvinceId = request.ProvinceId;
            user.PhoneNo = request.Phone;
            user.Email = request.Email;
            user.SubDepartmentId = request.SubDepartmentId;
            user.RoleId = request.RoleId;
            user.ModifyDate = DateTime.Now;
            user.EmployeeId = request.EmployeeId;
            user.CitizenId = request.Idcard;
            await _userRepositories.UpdateAsync(user);
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _userRepositories.DeleteAsync(id);
            return true;
        }
        public async Task<IEnumerable<Province>> GetProvince()
        {
            try
            {
                var provinces = await _provinceRepositories.GetAsync();
                return provinces;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetProvince: " + ex.Message);
                throw;
            }
        }
        public async Task<List<WorkLineResponse>> GetWorkLine()
        {
            try
            {
                var lines = await _lineRepositories.GetAsync();
                var result = new List<WorkLineResponse>();
                foreach (var line in lines)
                    result.Add(new WorkLineResponse(line));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetWorkLine: " + ex.Message);
                throw;
            }
        }
        public async Task<List<DepartmentResponse>> GetDepartment(long id)
        {
            try
            {
                var departments = await _departmentRepositories.GetByLineAsync(id);
                var result = new List<DepartmentResponse>();
                foreach (var department in departments)
                    result.Add(new DepartmentResponse(department));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetDepartment: " + ex.Message);
                throw;
            }
        }
        public async Task<List<SubDepartmentResponse>> GetSubDepartment(long id)
        {
            try
            {
                var subDepartments = await _subDepartmentRepositories.GetByDepartmentAsync(id);
                var result = new List<SubDepartmentResponse>();
                foreach (var subDepartment in subDepartments)
                    result.Add(new SubDepartmentResponse(subDepartment));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserServices => GetSubDepartment: " + ex.Message);
                throw;
            }
        }
        #endregion
    }
}
