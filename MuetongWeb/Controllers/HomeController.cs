using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using System.Diagnostics;

namespace MuetongWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServices _userServices;
        public HomeController(ILogger<HomeController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user == null)
                        return View(new PageModel());
                    if (string.IsNullOrWhiteSpace(user.HomePageUrl))
                        return View(new PageModel(user));
                    if (user.HomePageUrl == "~/" || user.HomePageUrl == "/")
                        return View(new PageModel(user));
                    return Redirect(user.HomePageUrl);
                }
                else
                    return View(ViewConstants.LoginView);
            }
            catch (Exception ex)
            {
                _logger.LogError("HomeController => Index: " + ex.Message);
                return View(ViewConstants.LoginView, new LoginModel(MessageConstants.LoginCatch));
            }
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest? request)
        {
            try
            {
                if (request == null) 
                    return View(new LoginModel(MessageConstants.LoginInvalidRequest));
                _logger.LogInformation("HomeController => LoginAsync: Username=" + request.Username + " Password=" + request.Password);
                var user = await _userServices.LoginAsync(request);
                if (user == null) 
                    return View(new LoginModel(MessageConstants.LoginInvalidRequest));
                _logger.LogInformation("HomeController => LoginAsync: Login success");
                var homepageUrl = string.IsNullOrWhiteSpace(user.HomePageUrl) ? ViewConstants.DefaultHomePage : user.HomePageUrl;
                SessionHelpers.SetUserInfo(HttpContext.Session, user);
                return Redirect(homepageUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("HomeController => LoginAsync: " + ex.Message);
                return View(new LoginModel(MessageConstants.LoginCatch));
            }
        }
        [Route("Logout")]
        public IActionResult LogoutAsync()
        {
            HttpContext.Session.Clear();
            return View(ViewConstants.LoginView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}