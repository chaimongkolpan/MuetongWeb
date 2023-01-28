using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductResponse>> GetAsync(ProductRequest request);
        Task<bool> AddAsync(ProductAddRequest request);
        Task<bool> UpdateAsync(long id, ProductUpdateRequest request);
        Task<bool> DeleteAsync(long id);
    }
}
