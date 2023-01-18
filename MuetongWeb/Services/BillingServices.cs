using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class BillingServices : IBillingServices
    {
        private readonly ILogger<BillingServices> _logger;
        private readonly IFileServices _fileServices;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPoRepositories _poRepositories;
        public BillingServices
        (
            ILogger<BillingServices> logger,
            IFileServices fileServices,
            IProjectRepositories projectRepositories,
            IPoRepositories poRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;
            _projectRepositories = projectRepositories;
            _poRepositories = poRepositories;
        }

        public async Task<bool> AddAsync(BillingIndexAddRequest request)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => AddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateAsync(BillingIndexUpdateRequest request)
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => UpdateAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<BillingIndexResponse> Search(BillingIndexSearch request)
        {
            try
            {

                return new BillingIndexResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => Search: " + ex.Message);
                return new BillingIndexResponse();
            }
        }
        public async Task<BillingIndexPoResponse> SearchPo(BillingIndexPoSearch request)
        {
            try
            {
                var pos = await _poRepositories.SearchBillingAsync(request);
                var response = new BillingIndexPoResponse(pos);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => SearchPo: " + ex.Message);
                return new BillingIndexPoResponse();
            }
        }
    }
}
