using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PoServices : IPoServices
    {
        private readonly ILogger<PoServices> _logger;
        public PoServices
        (
            ILogger<PoServices> logger
        )
        {
            _logger = logger;
        }
    }
}
