using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPaymentAccountRepositories
    {
        Task<PaymentAccount?> GetAsync(long id);
        Task<bool> AddAsync(PaymentAccount paymentAccount);
        Task<bool> AddAsync(List<PaymentAccount> paymentAccounts);
        Task<bool> UpdateAsync(PaymentAccount paymentAccount);
        Task<bool> DeleteAsync(long id);
    }
}
