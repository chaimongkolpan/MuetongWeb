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
        public BillingController(ILogger<BillingController> logger, IBillingServices billingServices)
        {
            _logger = logger;
            _billingServices = billingServices;
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
    }
}
