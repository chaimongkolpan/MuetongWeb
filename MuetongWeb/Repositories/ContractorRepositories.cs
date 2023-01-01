using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class ContractorRepositories : IContractorRepositories
    {
        private readonly MuetongContext _dbContext;
        public ContractorRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Get
        public async Task<bool> AddAsync(Contractor contractor)
        {
            await _dbContext.Contractors.AddAsync(contractor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
