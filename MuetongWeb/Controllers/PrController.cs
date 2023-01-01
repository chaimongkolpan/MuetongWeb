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
    public class PrController : Controller
    {
        private readonly ILogger<PrController> _logger;
        public PrController(ILogger<PrController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(1);
        }
    }
}
