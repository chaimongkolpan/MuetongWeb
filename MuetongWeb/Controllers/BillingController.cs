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
        private readonly IBillingServices _billingServices;
        private readonly IFileServices _fileServices;
        public BillingController(ILogger<BillingController> logger, IBillingServices billingServices, IFileServices fileServices)
        {
            _logger = logger;
            _billingServices = billingServices;
            _fileServices = fileServices;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Billing_Index_View, user.Permissions))
                    {
                        var response = await _billingServices.IndexAsync(PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_Edit, user.Permissions), user);
                        response.Set(user);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Approver")]
        public async Task<IActionResult> Approver()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Billing_Approver_View, user.Permissions))
                    {
                        var response = await _billingServices.IndexAsync(PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_Edit, user.Permissions), user);
                        response.Set(user);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingController => Approver: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("File/{id}/{filename}")]
        [HttpGet]
        public async Task<IActionResult> GetFile(long id, string filename)
        {
            try
            {
                var response = await _fileServices.GetFileAsync(id);
                if (response == null)
                    return BadRequest();
                response.Position = 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingController => GetFile: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
