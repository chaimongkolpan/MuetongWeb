using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PrServices : IPrServices
    {
        private readonly ILogger<PrServices> _logger;
        public PrServices
        (
            ILogger<PrServices> logger
        )
        {
            _logger = logger;
        }
        public async Task<PrIndexResponse> IndexAsync(bool canEdit)
        {
            try
            {
                var response = await Task.FromResult<PrIndexResponse>(new PrIndexResponse());
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => IndexSearchAsync: " + ex.Message);
                return new PrIndexResponse();
            }
        }
        public async Task<PrIndexResponse> IndexSearchAsync(bool canEdit, PrIndexSearchRequest request)
        {
            try
            {
                var response = await Task.FromResult<PrIndexResponse>(new PrIndexResponse());
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("PrServices => IndexSearchAsync: " + ex.Message);
                return new PrIndexResponse();
            }
        }
    }
}
