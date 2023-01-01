using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class LineRepositories : ILineRepositories
    {
        private readonly MuetongContext _dbContext;
        public LineRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Line>> GetAsync()
        {
            return await _dbContext.Lines
                                   .Include(line => line.Departments)
                                   .ThenInclude(department => department.SubDepartments)
                                   .ToListAsync();
        }
        public async Task<Line?> GetAsync(long id)
        {
            return await _dbContext.Lines.Where(line => line.Id == id)
                                   .Include(line => line.Departments)
                                   .ThenInclude(department => department.SubDepartments)
                                   .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Line line)
        {
            await _dbContext.Lines.AddAsync(line);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Line line)
        {
            var tmp = await _dbContext.Lines.FindAsync(line.Id);
            if (tmp == null)
                return false;
            tmp.Name = line.Name;
            tmp.UserId = line.UserId;
            tmp.ModifyDate = line.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.Lines.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
