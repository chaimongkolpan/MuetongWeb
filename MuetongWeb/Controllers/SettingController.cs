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
    public class SettingController : Controller
    {
        private readonly ILogger<SettingController> _logger;
        private readonly ISettingServices _settingServices;
        public SettingController(ILogger<SettingController> logger, ISettingServices settingServices)
        {
            _logger = logger;
            _settingServices = settingServices;
        }
        public IActionResult Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Setting_Index_View, user.Permissions))
                    {
                        return View(new SettingResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _settingServices.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => GetAll: " + ex.Message);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(string name, string type)
        {
            try
            {
                return Ok(await _settingServices.Add(name, type));
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return Ok(await _settingServices.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("ImportCustomer")]
        public async Task<IActionResult> ImportCustomerAsync(SettingImportCustomerRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _settingServices.ImportCustomerFileAsync(request, user.Id);
                        // response.Set(user);
                        return View("Index", response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => ImportCustomerAsync: " + ex.Message);
            }
            return View("Index", new SettingResponse(MessageConstants.SettingSomethingWrong));
        }
        [HttpPost]
        [Route("ImportStore")]
        public async Task<IActionResult> ImportStoreAsync(SettingImportStoreRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _settingServices.ImportStoreFileAsync(request, user.Id);
                        // response.Set(user);
                        return View("Index", response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingController => ImportStoreAsync: " + ex.Message);
            }
            return View("Index", new SettingResponse(MessageConstants.SettingSomethingWrong));
        }
    }
}
