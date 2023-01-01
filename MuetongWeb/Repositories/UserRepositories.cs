using Microsoft.EntityFrameworkCore;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;
namespace MuetongWeb.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly MuetongContext _dbContext;
        public UserRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetLogin(string username, string password)
        {
            var user = await _dbContext.Users.Where(user => user.Username == username && user.Password == password)
                                        .Include(user => user.Role).ThenInclude(role => role.RolePermissions).ThenInclude(rolePermission => rolePermission.Permission)
                                        .Include(user => user.SubDepartment)
                                        .ThenInclude(subDepartment => subDepartment.Department)
                                        .ThenInclude(department => department.Line)
                                        .Include(user => user.Province)
                                        .FirstOrDefaultAsync();
            return user;
        }
        public async Task<int> CountAsync(string? query)
        {
            var count = await _dbContext.Users.CountAsync(user => string.IsNullOrWhiteSpace(query)
                                            || string.Format("{0} {1}", user.Firstname, user.Lastname).Contains(query)
                                            || (user.PhoneNo ?? "").Contains(query)
                                            || (user.CitizenId ?? "").Contains(query)
                                            || (user.Email ?? "").Contains(query)
                                            || (user.EmployeeId ?? "").Contains(query)
                                        );
            return count;
        }
        public async Task<IEnumerable<User>> GetAsync(string? query, int page, int pageSize)
        {
            var users = await _dbContext.Users.Where(user => string.IsNullOrWhiteSpace(query)
                                            || string.Format("{0} {1}", user.Firstname, user.Lastname).Contains(query)
                                            || (user.PhoneNo ?? "").Contains(query)
                                            || (user.CitizenId ?? "").Contains(query)
                                            || (user.Email ?? "").Contains(query)
                                            || (user.EmployeeId ?? "").Contains(query)
                                        )
                                        .Skip((page - 1) * pageSize).Take(pageSize)
                                        .Include(user => user.SubDepartment)
                                        .ThenInclude(subDepartment => subDepartment.Department)
                                        .ThenInclude(department => department.Line)
                                        .Include(user => user.Province)
                                        .Include(user => user.Role)
                                        .ToListAsync();
            return users;
        }
        public async Task<User?> GetAsync(long id)
        {
            return await _dbContext.Users.Where(user => user.Id == id)
                                        .Include(user => user.SubDepartment)
                                        .ThenInclude(subDepartment => subDepartment.Department)
                                        .ThenInclude(department => department.Line)
                                        .Include(user => user.Province)
                                        .Include(user => user.Role)
                                        .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            var tmp = await _dbContext.Users.FindAsync(user.Id);
            if (tmp == null)
                return false;
            tmp.Firstname = user.Firstname;
            tmp.Lastname = user.Lastname;
            tmp.Address = user.Address;
            tmp.ProvinceId = user.ProvinceId;
            tmp.PhoneNo = user.PhoneNo;
            tmp.Email = user.Email;
            tmp.SubDepartmentId = user.SubDepartmentId;
            tmp.RoleId = user.RoleId;
            tmp.UserId = user.UserId;
            tmp.ModifyDate = user.ModifyDate;
            tmp.EmployeeId = user.EmployeeId;
            tmp.CitizenId = user.CitizenId;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null || user.Username == "admin")
                return false;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckPasswordAsync(long id, string password)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return false;
            return user.Password == password;
        }
        public async Task<bool> ChangePasswordAsync(long id, string password)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return false;
            user.Password = password;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
