using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PoServices : IPoServices
    {
        private readonly ILogger<PoServices> _logger;
        private readonly IFileServices _fileServices;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPoRepositories _poRepositories;
        private readonly IPrRepositories _prRepositories;
        private readonly IProductRepositories _productRepositories;
        public PoServices
        (
            ILogger<PoServices> logger,
            IFileServices fileServices,
            IProjectRepositories projectRepositories,
            IPoRepositories poRepositories,
            IPrRepositories prRepositories,
            IProductRepositories productRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;
            _projectRepositories = projectRepositories;
            _poRepositories = poRepositories;
            _prRepositories = prRepositories;
            _productRepositories = productRepositories;
        }
        #region Page Service
        public async Task<PoModel> IndexAsync(bool canEdit, UserInfoModel user)
        {
            try
            {
                IEnumerable<Project> projects;
                if (RoleHelpers.CanSeeAllProject(user.Role))
                    projects = await _projectRepositories.GetAsync();
                else
                    projects = await _projectRepositories.GetByUserIdAsync(user.Id);
                var projectResponses = new List<ProjectResponse>();
                foreach (var project in projects)
                {
                    projectResponses.Add(new ProjectResponse(project));
                }
                var response = new PoModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexAsync: " + ex.Message);
                return new PoModel();
            }
        }
        public async Task<PoModel> ApproverAsync(bool canEdit, UserInfoModel user)
        {
            try
            {
                IEnumerable<Project> projects;
                if (RoleHelpers.CanSeeAllProject(user.Role))
                    projects = await _projectRepositories.GetAsync();
                else
                    projects = await _projectRepositories.GetByUserIdAsync(user.Id);
                var projectResponses = new List<ProjectResponse>();
                foreach (var project in projects)
                {
                    projectResponses.Add(new ProjectResponse(project));
                }
                var response = new PoModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => ApproverAsync: " + ex.Message);
                return new PoModel();
            }
        }
        #endregion
        #region Api Service

        public async Task<PoIndexResponse> IndexSearchAsync(PoIndexSearchRequest request)
        {
            try
            {
                var prs = await _prRepositories.SearchAsync(request);
                var pos = new List<Po>();
                var response = new PoIndexResponse(pos, prs);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexSearchAsync: " + ex.Message);
                return new PoIndexResponse();
            }
        }
        public async Task<List<UserResponse>> GetRequesterByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var users = prs.Select(pr => pr.User).DistinctBy(user => user.Id).ToList();
            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                response.Add(new UserResponse(user));
            }
            return response;
        }
        public async Task<List<string>> GetPrNoByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var response = prs.Select(pr => pr.PrNo).Distinct().ToList();
            return response;
        }
        public async Task<List<string>> GetPoNoByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var response = prs.Select(pr => pr.PrNo).Distinct().ToList();
            return response;
        }

        #endregion
    }
}
