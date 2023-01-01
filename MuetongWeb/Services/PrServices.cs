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
    }
}
