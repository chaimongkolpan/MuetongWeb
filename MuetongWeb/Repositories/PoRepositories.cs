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
    }
}
