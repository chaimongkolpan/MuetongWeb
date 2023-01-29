using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly ILogger<ProjectServices> _logger;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IProvinceRepositories _provinceRepositories;
        private readonly IContractorRepositories _contractorRepositories;
        private readonly ICustomerRepositories _customerRepositories;
        private readonly IUserRepositories _userRepositories;
        private readonly IFileServices _fileServices;
        public ProjectServices
        (
            ILogger<ProjectServices> logger, 
            IProjectRepositories projectRepositories,
            IProvinceRepositories provinceRepositories,
            IContractorRepositories contractorRepositories,
            ICustomerRepositories customerRepositories,
            IUserRepositories userRepositories,
            IFileServices fileServices
        )
        {
            _logger = logger;
            _projectRepositories = projectRepositories;
            _provinceRepositories = provinceRepositories;
            _contractorRepositories = contractorRepositories;
            _customerRepositories = customerRepositories;
            _userRepositories = userRepositories;
            _fileServices = fileServices;
        }
        #region Project
        public async Task<ProjectCollectionResponse> GetAsync()
        {
            try
            {
                var projects = await _projectRepositories.GetAsync();
                var response = new ProjectCollectionResponse(projects);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServices => GetAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> AddAsync(ProjectAddRequest request)
        {
            var project = new Project()
            {
                Name = request.Name,
                Address = request.Address,
                ProvinceId = request.ProvinceId,
                ContractNo = request.ContractNo,
                CustomerId = request.CustomerId,
                UserId = request.User.Id,
                CreateDate = DateTime.Now
            };
            await _projectRepositories.AddAsync(project);
            return true;
        }
        public async Task<bool> UpdateAsync(long id, ProjectUpdateRequest request)
        {
            var project = await _projectRepositories.GetAsync(id);
            if (project == null)
                return false;
            project.Name = request.Name;
            project.Address = request.Address;
            project.ProvinceId = request.ProvinceId;
            project.ContractNo = request.ContractNo;
            project.ModifyDate = DateTime.Now;
            await _projectRepositories.UpdateAsync(project);
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _projectRepositories.DeleteAsync(id);
            return true;
        }
        #endregion
        #region Code
        public async Task<ProjectResponse> ImportCode(long projectId, ProjectCodeImportRequest request)
        {
            try
            {
                var codes = await _fileServices.ImportProjectCodeExcel(request, projectId);
                if(codes.Any())
                {
                    foreach(var code in codes)
                    {
                        var tmp = await _projectRepositories.FindByCodeAsync(projectId, code.Code);
                        if (tmp == null)
                            await _projectRepositories.AddCodeAsync(code);
                        else
                        {
                            tmp.Budjet = code.Budjet;
                            tmp.Cost = code.Cost;
                            tmp.Detail = code.Detail;
                            await _projectRepositories.UpdateCodeAsync(tmp);
                        }
                    }
                }
                // check for delete (not now doing)
                // find parent
            }
            catch(Exception ex)
            {
                _logger.LogError("ProjectServices => ImportCode: " + ex.Message);
            }
            var project = await _projectRepositories.GetAsync(projectId);
            if (project == null)
                return new ProjectResponse();
            var response = new ProjectResponse(project);
                return response;
        }
        #endregion
        #region User
        public async Task<List<UserResponse>> GetUserAsync()
        {
            try
            {
                var users = await _userRepositories.GetAsync();
                var response = new List<UserResponse>();
                foreach (var user in users)
                {
                    response.Add(new UserResponse(user));
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServices => GetUserAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<ProjectResponse> AddUser(long projectId, long id)
        {
            var pUser = new ProjectUser()
            {
                ProjectId = projectId,
                UserId = id
            };
            await _projectRepositories.AddUserAsync(pUser);
            var project = await _projectRepositories.GetAsync(projectId);
            if (project == null)
                return new ProjectResponse();
            var response = new ProjectResponse(project);
            return response;
        }
        public async Task<ProjectResponse> DeleteUser(long projectId, long id)
        {
            await _projectRepositories.DeleteUserAsync(id);
            var project = await _projectRepositories.GetAsync(projectId);
            if (project == null)
                return new ProjectResponse();
            var response = new ProjectResponse(project);
            return response;
        }
        #endregion
        #region Contractor
        public async Task<List<ContractorResponse>> GetContractorAsync()
        {
            try
            {
                var contractors = await _contractorRepositories.GetAsync();
                var response = new List<ContractorResponse>();
                foreach (var contractor in contractors)
                {
                    response.Add(new ContractorResponse(contractor));
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServices => GetContractorAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<ProjectResponse> AddContractor(long projectId, long id)
        {
            var pContractor = new ProjectContractor()
            {
                ProjectId = projectId,
                ContractorId = id
            };
            await _projectRepositories.AddContractorAsync(pContractor);
            var project = await _projectRepositories.GetAsync(projectId);
            if (project == null)
                return new ProjectResponse();
            var response = new ProjectResponse(project);
            return response;
        }
        public async Task<ProjectResponse> DeleteContractor(long projectId, long id)
        {
            await _projectRepositories.DeleteContractorAsync(id);
            var project = await _projectRepositories.GetAsync(projectId);
            if (project == null)
                return new ProjectResponse();
            var response = new ProjectResponse(project);
            return response;
        }
        #endregion
        #region Other
        public async Task<IEnumerable<Province>> GetProvince()
        {
            try
            {
                var provinces = await _provinceRepositories.GetAsync();
                return provinces;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServices => GetProvince: " + ex.Message);
                throw;
            }
        }
        public async Task<List<CustomerResponse>> GetCustomer()
        {
            try
            {
                var customers = await _customerRepositories.GetAsync();
                var response = new List<CustomerResponse>();
                foreach (var customer in customers)
                {
                    response.Add(new CustomerResponse(customer));
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectServices => GetCustomer: " + ex.Message);
                throw;
            }
        }
        #endregion
    }
}
