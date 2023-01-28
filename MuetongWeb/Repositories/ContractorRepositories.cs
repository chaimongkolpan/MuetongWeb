using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class ContractorRepositories : IContractorRepositories
    {
        private readonly MuetongContext _dbContext;
        public ContractorRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Contractor>> GetAsync()
        {
            return await _dbContext.Contractors
                                   .Include(contractor => contractor.Province)
                                   .Include(contractor => contractor.ProjectContractors)
                                   .OrderBy(contractor => contractor.Name)
                                   .ToListAsync();
        }
        public async Task<IEnumerable<Contractor>> GetAsync(string? query, long? provinceId, int page, int pageSize)
        {
            return await _dbContext.Contractors.Where(contractor => (!provinceId.HasValue || contractor.ProvinceId == provinceId.Value)
                                        && (string.IsNullOrWhiteSpace(query)
                                           || contractor.Name.Contains(query)
                                           || (!string.IsNullOrWhiteSpace(contractor.PhoneNo) && contractor.PhoneNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.Address) && contractor.Address.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.Email) && contractor.Email.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.TaxNo) && contractor.TaxNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.DirectorName) && contractor.DirectorName.Contains(query))
                                        )
                                    )
                                   .OrderBy(contractor => contractor.Name)
                                   .Skip((page - 1) * pageSize).Take(pageSize)
                                   .Include(contractor => contractor.Province)
                                   .Include(contractor => contractor.ProjectContractors)
                                   .ToListAsync();
        }
        public async Task<int> CountAsync(string? query, long? provinceId)
        {
            var count = await _dbContext.Contractors.CountAsync(contractor => (!provinceId.HasValue || contractor.ProvinceId == provinceId.Value)
                                        && (string.IsNullOrWhiteSpace(query)
                                           || contractor.Name.Contains(query)
                                           || (!string.IsNullOrWhiteSpace(contractor.PhoneNo) && contractor.PhoneNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.Address) && contractor.Address.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.Email) && contractor.Email.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.TaxNo) && contractor.TaxNo.Contains(query))
                                           || (!string.IsNullOrWhiteSpace(contractor.DirectorName) && contractor.DirectorName.Contains(query))
                                        )
                                        );
            return count;
        }
        public async Task<Contractor?> GetAsync(long id)
        {
            return await _dbContext.Contractors.Where(contractor => contractor.Id == id)
                                   .Include(contractor => contractor.Province)
                                   .Include(contractor => contractor.ProjectContractors)
                                   .FirstOrDefaultAsync();
        }
        public async Task<bool> AddAsync(Contractor contractor)
        {
            await _dbContext.Contractors.AddAsync(contractor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Contractor contractor)
        {
            var tmp = await _dbContext.Contractors.FindAsync(contractor.Id);
            if (tmp == null)
                return false;
            tmp.Name = contractor.Name;
            tmp.Address = contractor.Address;
            tmp.ProvinceId = contractor.ProvinceId;
            tmp.PhoneNo = contractor.PhoneNo;
            tmp.Email = contractor.Email;
            tmp.TaxNo = contractor.TaxNo;
            tmp.Type = contractor.Type;
            tmp.DirectorName = contractor.DirectorName;
            tmp.ModifyDate = contractor.ModifyDate;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var tmp = await _dbContext.Contractors.FindAsync(id);
            if (tmp == null)
                return false;
            _dbContext.Remove(tmp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
