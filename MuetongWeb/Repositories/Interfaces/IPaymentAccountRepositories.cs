using MuetongWeb.Models.Entities;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPaymentAccountRepositories
    {
        Task<bool> AddAsync(PaymentAccount paymentAccount);
        Task<bool> AddAsync(List<PaymentAccount> paymentAccounts);
    }
}
