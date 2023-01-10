using System.Collections.Generic;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PrServices : IPrServices
    {
        private readonly ILogger<PrServices> _logger;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPrRepositories _prRepositories;
        private readonly IProductRepositories _productRepositories;
        public PrServices
        (
            ILogger<PrServices> logger,
            IProjectRepositories projectRepositories,
            IPrRepositories prRepositories,
            IProductRepositories productRepositories
        )
        {
            _logger = logger;
            _projectRepositories = projectRepositories;
            _prRepositories = prRepositories;
            _productRepositories = productRepositories;
        }
        #region Page Service
        public async Task<PrModel> IndexAsync(bool canEdit, UserInfoModel user)
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
                var response = await Task.FromResult<PrModel>(new PrModel(projectResponses, canEdit));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => IndexAsync: " + ex.Message);
                return new PrModel();
            }
        }
        public async Task<PrIndexResponse> IndexSearchAsync(bool canEdit, PrIndexSearchRequest request)
        {
            try
            {
                var response = await Task.FromResult<PrIndexResponse>(new PrIndexResponse());
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("PrServices => IndexSearchAsync: " + ex.Message);
                return new PrIndexResponse();
            }
        }
        #endregion
        #region Api Service
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
        public async Task<List<ContractorResponse>> GetContractorByProject(long projectId)
        {
            IEnumerable<ProjectContractor> projectContractors = await _projectRepositories.GetContractorAsync(projectId);
            var contractors = projectContractors.Select(project => project.Contractor).DistinctBy(contractor => contractor.Id).ToList();
            var response = new List<ContractorResponse>();
            foreach (var contractor in contractors)
            {
                response.Add(new ContractorResponse(contractor));
            }
            return response;
        }
        public async Task<List<ProjectCodeResponse>> GetProjectCode(long projectId)
        {
            IEnumerable<ProjectCode> codes = await _projectRepositories.GetCodeAsync(projectId);
            var response = new List<ProjectCodeResponse>();
            foreach (var code in codes)
            {
                response.Add(new ProjectCodeResponse(code));
            }
            return response;
        }
        public async Task<List<ProductResponse>> GetProduct()
        {
            IEnumerable<Product> products = await _productRepositories.GetAsync();
            var response = new List<ProductResponse>();
            foreach (var product in products)
            {
                response.Add(new ProductResponse(product));
            }
            return response;
        }
        #endregion
    }
}
