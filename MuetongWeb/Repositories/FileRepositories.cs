using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class FileRepositories : IFileRepositories
    {
        private readonly MuetongContext _dbContext;
        public FileRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
