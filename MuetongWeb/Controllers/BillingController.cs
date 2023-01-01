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
    public class BillingController : Controller
    {
        private readonly ILogger<BillingController> _logger;
        public BillingController(ILogger<BillingController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(1);
        }
    }
}
