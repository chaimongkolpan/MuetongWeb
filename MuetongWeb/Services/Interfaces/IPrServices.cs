using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services.Interfaces
{
    public interface IPrServices
    {
        Task<PrIndexResponse> IndexSearchAsync(bool canEdit, PrIndexSearchRequest request);
    }
}
