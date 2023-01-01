using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class ContractorServices : IContractorServices
    {
        private readonly ILogger<ContractorServices> _logger;
        public ContractorServices
        (
            ILogger<ContractorServices> logger
        )
        {
            _logger = logger;
        }
    }
}
