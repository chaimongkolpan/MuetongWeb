﻿using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Responses;
using MuetongWeb.Services;

namespace MuetongWeb.Controllers
{
    [Route("[controller]")]
    public class PrController : Controller
    {
        private readonly ILogger<PrController> _logger;
        private readonly IPrServices _prServices;
        public PrController(ILogger<PrController> logger, IPrServices prServices)
        {
            _logger = logger;
            _prServices = prServices;
        }
        #region Pr Index
        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_View, user.Permissions))
                    {
                        var response = await _prServices.IndexAsync(PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_Edit, user.Permissions), user);
                        response.Set(user);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("PrController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        #endregion
        #region Pr Approver
        [Route("Approver")]
        public IActionResult Approver()
        {
            return View(1);
        }
        #endregion
        #region Pr Receive
        [Route("Receive")]
        public IActionResult Receive()
        {
            return View(1);
        }
        #endregion
    }
}
