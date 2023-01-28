using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<CustomerCollectionResponse?> GetAsync(CustomerRequest request);
        Task<CustomerResponse?> GetAsync(long id);
        Task<bool> AddAsync(CustomerAddRequest request);
        Task<bool> UpdateAsync(long id, CustomerUpdateRequest request);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Province>> GetProvince();
    }
}
