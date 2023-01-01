using MuetongWeb.Services.Interfaces;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Services
{
    public class WorkLineServices : IWorkLineServices
    {
        private readonly ILogger<WorkLineServices> _logger;
        private readonly ILineRepositories _lineRepositories;
        private readonly IDepartmentRepositories _departmentRepositories;
        private readonly ISubDepartmentRepositories _subDepartmentRepositories;
        public WorkLineServices
        (
            ILogger<WorkLineServices> logger,
            ILineRepositories lineRepositories,
            IDepartmentRepositories departmentRepositories,
            ISubDepartmentRepositories subDepartmentRepositories
        )
        {
            _logger = logger;
            _lineRepositories = lineRepositories;
            _departmentRepositories = departmentRepositories;
            _subDepartmentRepositories = subDepartmentRepositories;
        }
    }
}
