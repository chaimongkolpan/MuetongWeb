using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Requests;
using MuetongWeb.Services;
using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Controllers
{
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IPrServices _prServices;
        public ApiController(ILogger<ApiController> logger, IPrServices prServices)
        {
            _logger = logger;
            _prServices = prServices;
        }

        public IActionResult Index()
        {
            return Ok(new { Status = true });
        }
        #region Pr
        [Route("Pr/Search")]
        [HttpPost]
        public async Task<IActionResult> PrIndexSearch(PrIndexSearchRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_View, user.Permissions))
                    {
                        var response = await _prServices.IndexSearchAsync(PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_Edit, user.Permissions), request);
                        // response.Set(user.Role);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiController => PrIndexSearch: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        #endregion
    }
}

