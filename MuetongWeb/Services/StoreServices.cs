using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class StoreServices : IStoreServices
    {
        private readonly ILogger<StoreServices> _logger;
        public StoreServices
        (
            ILogger<StoreServices> logger
        )
        {
            _logger = logger;
        }
    }
}
