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
    public class ContractorController : Controller
    {
        private readonly ILogger<ContractorController> _logger;
        private readonly IWorkLineServices _workLineServices;
        public ContractorController(ILogger<ContractorController> logger, IWorkLineServices workLineServices)
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
