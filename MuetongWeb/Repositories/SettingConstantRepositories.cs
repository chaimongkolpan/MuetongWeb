using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class SettingConstantRepositories : ISettingConstantRepositories
    {
        private readonly MuetongContext _dbContext;
        public SettingConstantRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
