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
    public class PoController : Controller
    {
        private readonly ILogger<PoController> _logger;
        public PoController(ILogger<PoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(1);
        }
    }
}
