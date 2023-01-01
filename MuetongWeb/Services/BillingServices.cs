using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class BillingServices : IBillingServices
    {
        private readonly ILogger<BillingServices> _logger;
        public BillingServices
        (
            ILogger<BillingServices> logger
        )
        {
            _logger = logger;
        }
    }
}
