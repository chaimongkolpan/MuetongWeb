using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Constants;

namespace MuetongWeb.Repositories
{
    public class BillingRepositories : IBillingRepositories
    {
        private readonly MuetongContext _dbContext;
        public BillingRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Billing>> GetAsync()
        {
            return await _dbContext.Billings.ToListAsync();
        }
        public async Task<Billing?> GetAsync(long id)
        {
            return await _dbContext.Billings.FindAsync(id);
        }
        public async Task<IEnumerable<Billing>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Billings.Where(billing => true).ToListAsync();
        }
        public async Task<IEnumerable<Billing>> Search(BillingIndexSearch request, List<long> poIds)
        {
            return await _dbContext.Billings.Where(bill => 
                    (string.IsNullOrWhiteSpace(request.BillingNo) || request.BillingNo == RequestConstants.AllString || bill.BillingNo == request.BillingNo)
                    && (string.IsNullOrWhiteSpace(request.Status) || request.Status == RequestConstants.AllString || bill.Status == request.Status)
                    && bill.PoBillings.Any(pb => poIds.Contains(pb.PoId))
                    && (request.NoPayment == RequestConstants.AllValue 
                        || ((request.NoPayment == 1) == (!bill.IsPay.HasValue || !bill.IsPay.Value)))
                    && (request.NoReceipt == RequestConstants.AllValue 
                        || ((request.NoReceipt == 1) == (!bill.HasReceipt.HasValue || !bill.HasReceipt.Value)))
                    && (request.NoInvoice == RequestConstants.AllValue || ((request.NoInvoice == 1) == (!bill.HasInvoice.HasValue || !bill.HasInvoice.Value)))
                                    )
                                    .Include(bill => bill.User).ThenInclude(user => user.Role)
                                    .Include(bill => bill.User).ThenInclude(user => user.SubDepartment)
                                    .ThenInclude(sub => sub.Department)
                                    .ThenInclude(de => de.Line)
                                    .Include(bill => bill.Approver)
                                    .Include(bill => bill.PaymentTypeNavigation)
                                    .Include(bill => bill.PaymentAccount)
                                    .Include(bill => bill.PoBillings)
                                    .ToListAsync();
        }
        public async Task<bool> AddAsync(Billing bill)
        {
            await _dbContext.Billings.AddAsync(bill);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddPoAsync(List<PoBilling> pos)
        {
            await _dbContext.PoBillings.AddRangeAsync(pos);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePoAsync(long billingId)
        {
            var pos = await _dbContext.PoBillings.Where(po => po.BillingId == billingId).ToListAsync();
            if (pos.Any())
                return true;
            _dbContext.PoBillings.RemoveRange(pos);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Billing bill)
        {
            _dbContext.Billings.Update(bill);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
