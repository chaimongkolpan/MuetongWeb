using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class ProjectRepositories : IProjectRepositories
    {
        private readonly MuetongContext _dbContext;
        public ProjectRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Project
        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await _dbContext.Projects
                                   .OrderBy(project => project.Name)
                                   .Include(project => project.Customer)
                                   .Include(project => project.Province)
                                   .Include(project => project.ProjectCodes)
                                   .Include(project => project.ProjectUsers).ThenInclude(user => user.User)
                                   .Include(project => project.ProjectContractors).ThenInclude(contractor => contractor.Contractor)
                                   .ToListAsync();
        }
        public async Task<IEnumerable<Project>> GetByCustomerAsync(long customerId)
        {
            return await _dbContext.Projects.Where(project => project.CustomerId == customerId)
                                   .OrderBy(project => project.Name)
                                   .Include(project => project.Customer)
                                   .Include(project => project.Province)
                                   .ToListAsync();
        }
        public async Task<IEnumerable<Project>> GetByUserIdAsync(long userId)
        {
            return await _dbContext.Projects.Where(project => project.ProjectUsers != null && project.ProjectUsers.Any(pUser => pUser.UserId == userId))
                                   .OrderBy(project => project.Name)
                                   .Include(project => project.Customer)
                                   .Include(project => project.Province)
                                   .ToListAsync();
        }
        public async Task<Project?> GetAsync(long id)
        {
            return await _dbContext.Projects.Where(project => project.Id == id)
                                   .Include(project => project.Customer)
                                   .Include(project => project.Province)
                                   .Include(project => project.ProjectCodes)
                                   .Include(project => project.ProjectUsers).ThenInclude(user => user.User)
                                   .Include(project => project.ProjectContractors).ThenInclude(contractor => contractor.Contractor)
                                   .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Project project)
        {
            var tmp = await _dbContext.Projects.FindAsync(project.Id);
            if (tmp == null)
                return false;
            tmp.Name = project.Name;
            tmp.ContractNo = project.ContractNo;
            tmp.Address = project.Address;
            tmp.ProvinceId = project.ProvinceId;
            tmp.UserId = project.UserId;
            tmp.ModifyDate = project.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await DeleteCodeAllAsync(id);
            await DeleteContractorByProjectAsync(id);
            await DeleteUserByProjectAsync(id);
            var tmp = await _dbContext.Projects.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        #endregion
        #region User
        public async Task<bool> AddUserAsync(ProjectUser projectUser)
        {
            await _dbContext.ProjectUsers.AddAsync(projectUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddUserAsync(List<ProjectUser> projectUsers)
        {
            await _dbContext.ProjectUsers.AddRangeAsync(projectUsers);
            await _dbContext.SaveChangesAsync();
                return true;
        }
        public async Task<bool> DeleteUserAsync(long id)
        {
            var tmp = await _dbContext.ProjectUsers.FirstOrDefaultAsync(projectUser => projectUser.UserId == id);
            if (tmp == null)
                return false;
            _dbContext.ProjectUsers.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserByProjectAsync(long id)
        {
            var tmp = await _dbContext.ProjectUsers.Where(projectUser => projectUser.ProjectId == id).ToListAsync();
            if (tmp.Any())
            {
                _dbContext.ProjectUsers.RemoveRange(tmp);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        #endregion
        #region Contractor
        public async Task<IEnumerable<ProjectContractor>> GetContractorAsync(long projectId)
        {
            return await _dbContext.ProjectContractors.Where(project => project.ProjectId == projectId)
                                                      .Include(project => project.Contractor)
                                                      .ToListAsync();
        }
        public async Task<bool> AddContractorAsync(List<ProjectContractor> projectContractors)
        {
            await _dbContext.ProjectContractors.AddRangeAsync(projectContractors);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddContractorAsync(ProjectContractor projectContractor)
        {
            await _dbContext.ProjectContractors.AddAsync(projectContractor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteContractorAsync(long id)
        {
            var tmp = await _dbContext.ProjectContractors.FirstOrDefaultAsync(projectContractor => projectContractor.ContractorId == id);
            if (tmp == null)
                return false;
            _dbContext.ProjectContractors.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteContractorByProjectAsync(long id)
        {
            var tmp = await _dbContext.ProjectContractors.Where(projectContractor => projectContractor.ProjectId == id).ToListAsync();
            if (tmp.Any())
            {
                _dbContext.ProjectContractors.RemoveRange(tmp);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        #endregion
        #region Code
        public async Task<IEnumerable<ProjectCode>> GetCodeAsync(long projectId)
        {
            return await _dbContext.ProjectCodes.Where(projectCode => projectCode.ProjectId == projectId)
                                   .ToListAsync();
        }
        public async Task<ProjectCode?> FindAsync(long id)
        {
            return await _dbContext.ProjectCodes.FindAsync(id);
        }
        public async Task<ProjectCode?> FindByCodeAsync(long projectId, string codeRequest)
        {
            return await _dbContext.ProjectCodes.FirstOrDefaultAsync(code => code.ProjectId == projectId && code.Code == codeRequest);
        }
        public async Task<bool> AddCodeAsync(ProjectCode projectCode)
        {
            await _dbContext.ProjectCodes.AddAsync(projectCode);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddCodeListAsync(List<ProjectCode> projectCodes)
        {
            await _dbContext.ProjectCodes.AddRangeAsync(projectCodes);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCodeAsync(ProjectCode projectCode)
        {
            var tmp = await _dbContext.ProjectCodes.FindAsync(projectCode.Id);
            if (tmp == null)
                return false;
            tmp.Detail = projectCode.Detail;
            tmp.Code = projectCode.Code;
            tmp.Budjet = projectCode.Budjet;
            tmp.Cost = projectCode.Cost;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCodeAsync(long id)
        {
            var tmp = await _dbContext.ProjectCodes.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.ProjectCodes.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCodeAllAsync(long id)
        {
            var tmp = await _dbContext.ProjectCodes.Where(projectCode => projectCode.ProjectId == id).ToListAsync();
            if (tmp.Any())
            {
                _dbContext.ProjectCodes.RemoveRange(tmp);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        #endregion
    }
}
