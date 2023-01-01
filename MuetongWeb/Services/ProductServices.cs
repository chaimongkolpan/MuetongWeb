using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ILogger<ProductServices> _logger;
        public ProductServices
        (
            ILogger<ProductServices> logger
        )
        {
            _logger = logger;
        }
    }
}
