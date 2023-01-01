using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class SubDepartmentRepositories : ISubDepartmentRepositories
    {
        private readonly MuetongContext _dbContext;
        public SubDepartmentRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SubDepartment>> GetByDepartmentAsync(long departmentId)
        {
            return await _dbContext.SubDepartments.Where(sub => sub.DepartmentId == departmentId).ToListAsync();
        }
        public async Task<SubDepartment?> GetAsync(long id)
        {
            return await _dbContext.SubDepartments.Where(sub => sub.Id == id)
                                                  .Include(sub => sub.Department)
                                                  .ThenInclude(department => department.Line)
                                                  .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(SubDepartment subDepartment)
        {
            await _dbContext.SubDepartments.AddAsync(subDepartment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(SubDepartment subDepartment)
        {
            var tmp = await _dbContext.SubDepartments.FindAsync(subDepartment.Id);
            if (tmp == null)
                return false;
            tmp.Name = subDepartment.Name;
            tmp.UserId = subDepartment.UserId;
            tmp.ModifyDate = subDepartment.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.SubDepartments.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
