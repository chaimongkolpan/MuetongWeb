using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ILogger<CustomerServices> _logger;
        public CustomerServices
        (
            ILogger<CustomerServices> logger
        )
        {
            _logger = logger;
        }
    }
}
