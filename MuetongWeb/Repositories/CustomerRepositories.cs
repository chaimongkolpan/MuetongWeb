using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class CustomerRepositories : ICustomerRepositories
    {
        private readonly MuetongContext _dbContext;
        public CustomerRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _dbContext.Customers
                                   .Include(customer => customer.Province)
                                   .Include(customer => customer.Projects)
                                   .OrderBy(customer => customer.Name)
                                   .ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetAsync(string? query, long? provinceId, int page, int pageSize)
        {
            return await _dbContext.Customers.Where(customer => (!provinceId.HasValue || customer.ProvinceId == provinceId.Value)
                                        && (string.IsNullOrWhiteSpace(query)
                                           || customer.Name.Contains(query)
                                           || (!string.IsNullOrWhiteSpace(customer.Detail) && customer.Detail.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.PhoneNo) && customer.PhoneNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.Address) && customer.Address.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.Email) && customer.Email.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.TaxNo) && customer.TaxNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.BranchNo) && customer.BranchNo.Contains(query))
                                        )
                                    )
                                   .OrderBy(customer => customer.Name)
                                   .Skip((page - 1) * pageSize).Take(pageSize)
                                   .Include(customer => customer.Province)
                                   .Include(customer => customer.Projects)
                                   .ToListAsync();
        }
        public async Task<int> CountAsync(string? query, long? provinceId)
        {
            var count = await _dbContext.Customers.CountAsync(customer => (!provinceId.HasValue || customer.ProvinceId == provinceId.Value)
                                        && (string.IsNullOrWhiteSpace(query)
                                           || customer.Name.Contains(query)
                                           || (!string.IsNullOrWhiteSpace(customer.Detail) && customer.Detail.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.PhoneNo) && customer.PhoneNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.Address) && customer.Address.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.Email) && customer.Email.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.TaxNo) && customer.TaxNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(customer.BranchNo) && customer.BranchNo.Contains(query))
                                           )
                                        );
            return count;
        }
        public async Task<Customer?> GetAsync(long id)
        {
            return await _dbContext.Customers.Where(customer => customer.Id == id)
                                   .Include(customer => customer.Province)
                                   .Include(customer => customer.Projects)
                                   .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Customer customer)
        {
            var tmp = await _dbContext.Customers.FindAsync(customer.Id);
            if (tmp == null)
                return false;
            tmp.Name = customer.Name;
            tmp.Detail = customer.Detail;
            tmp.Address = customer.Address;
            tmp.ProvinceId = customer.ProvinceId;
            tmp.PhoneNo = customer.PhoneNo;
            tmp.Email = customer.Email;
            tmp.TaxNo = customer.TaxNo;
            tmp.BranchNo = customer.BranchNo;
            tmp.UserId = customer.UserId;
            tmp.ModifyDate = customer.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.Customers.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
