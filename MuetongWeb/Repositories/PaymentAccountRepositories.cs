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
        // Get
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
    }
}
