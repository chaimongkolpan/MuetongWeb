using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class DepartmentRepositories : IDepartmentRepositories
    {
        private readonly MuetongContext _dbContext;
        public DepartmentRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Department>> GetByLineAsync(long lineId)
        {
            return await _dbContext.Departments.Where(x => x.LineId == lineId)
                                               .Include(department => department.SubDepartments)
                                               .ToListAsync();
        }
        public async Task<Department?> GetAsync(long id)
        {
            return await _dbContext.Departments.Where(department => department.Id == id)
                                               .Include(department => department.Line)
                                               .Include(department => department.SubDepartments)
                                               .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Department department)
        {
            var tmp = await _dbContext.Departments.FindAsync(department.Id);
            if (tmp == null)
                return false;
            tmp.Name = department.Name;
            tmp.UserId = department.UserId;
            tmp.ModifyDate = department.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.Departments.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
