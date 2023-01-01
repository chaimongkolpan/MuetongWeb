using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class BillingRepositories : IBillingRepositories
    {
        private readonly MuetongContext _dbContext;
        public BillingRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
