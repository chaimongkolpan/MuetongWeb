using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IBillingRepositories
    {
        Task<IEnumerable<Billing>> GetAsync();
        Task<Billing?> GetAsync(long id);
        Task<IEnumerable<Billing>> GetByProjectAsync(long projectId);
        Task<IEnumerable<Billing>> Search(BillingIndexSearch request, List<long> poIds);
        Task<bool> AddAsync(Billing bill);
        Task<bool> AddPoAsync(List<PoBilling> pos);
        Task<bool> DeletePoAsync(long billingId);
        Task<bool> UpdateAsync(Billing bill);
    }
}
