using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class PoRepositories : IPoRepositories
    {
        private readonly MuetongContext _dbContext;
        public PoRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Po>> GetAsync()
        {
            return await _dbContext.Pos.OrderBy(po => po.CreateDate)
                                       .Include(po => po.PoDetails)
                                       .Include(po => po.User)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<Po>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Pos//.Where(po => po.ProjectId == projectId)
                                       .OrderBy(po => po.CreateDate)
                                       .Include(po => po.PoDetails)
                                       .Include(po => po.User)
                                       .ToListAsync();
        }
    }
}
