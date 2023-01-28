using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IContractorServices
    {
        Task<ContractorCollectionResponse?> GetAsync(ContractorRequest request);
        Task<ContractorResponse?> GetAsync(long id);
        Task<bool> AddAsync(ContractorAddRequest request);
        Task<bool> UpdateAsync(long id, ContractorUpdateRequest request);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Province>> GetProvince();
    }
}
