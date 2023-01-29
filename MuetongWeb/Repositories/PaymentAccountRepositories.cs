using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories
{
    public class PaymentAccountRepositories : IPaymentAccountRepositories
    {
        private readonly MuetongContext _dbContext;
        public PaymentAccountRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaymentAccount?> GetAsync(long id)
        {
            return await _dbContext.PaymentAccounts.FindAsync(id);
        }
        public async Task<bool> AddAsync(PaymentAccount paymentAccount)
        {
            await _dbContext.PaymentAccounts.AddAsync(paymentAccount);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAsync(List<PaymentAccount> paymentAccounts)
        {
            await _dbContext.PaymentAccounts.AddRangeAsync(paymentAccounts);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(PaymentAccount paymentAccount)
        {
            var acc = await _dbContext.PaymentAccounts.FindAsync(paymentAccount.Id);
            if (acc == null)
                return false;
            acc.AccountNo = paymentAccount.AccountNo;
            acc.AccountName = paymentAccount.AccountName;
            acc.Bank = paymentAccount.Bank;
            acc.Type = paymentAccount.Type;
            _dbContext.PaymentAccounts.Update(acc);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var acc = await _dbContext.PaymentAccounts.FindAsync(id);
            if (acc == null)
                return false;
            _dbContext.PaymentAccounts.Remove(acc);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
