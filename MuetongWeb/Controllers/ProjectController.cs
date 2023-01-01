using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Controllers
{
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IWorkLineServices _workLineServices;
        public ProjectController(ILogger<ProjectController> logger, IWorkLineServices workLineServices)
        {
            _logger = logger;
            _workLineServices = workLineServices;
        }
        public IActionResult Index()
        {
            return View(1);
        }
    }
}
