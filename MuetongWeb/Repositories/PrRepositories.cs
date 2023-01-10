using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class PrRepositories : IPrRepositories
    {
        private readonly MuetongContext _dbContext;
        public PrRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pr>> GetAsync()
        {
            return await _dbContext.Prs.OrderBy(pr => pr.CreateDate)
                                       .Include(pr => pr.PrDetails)
                                       .Include(pr => pr.User)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Prs.Where(pr => pr.ProjectId == projectId)
                                       .OrderBy(pr => pr.CreateDate)
                                       .Include(pr => pr.PrDetails)
                                       .Include(pr => pr.User)
                                       .ToListAsync();
        }
    }
}
