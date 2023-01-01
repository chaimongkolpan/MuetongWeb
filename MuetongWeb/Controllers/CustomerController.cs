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
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IWorkLineServices _workLineServices;
        public CustomerController(ILogger<CustomerController> logger, IWorkLineServices workLineServices)
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
