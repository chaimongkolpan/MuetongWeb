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
    }
}
