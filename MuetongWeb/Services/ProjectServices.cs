using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly ILogger<ProjectServices> _logger;
        public ProjectServices
        (
            ILogger<ProjectServices> logger
        )
        {
            _logger = logger;
        }
    }
}
