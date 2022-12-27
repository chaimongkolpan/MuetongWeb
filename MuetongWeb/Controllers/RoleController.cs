using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;

namespace MuetongWeb.Controllers
{
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;

        private readonly IRoleServices _roleServices;
        public RoleController(ILogger<RoleController> logger, IRoleServices roleServices)
        {
            _logger = logger;
            _roleServices = roleServices;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Role_Index_View, user.Permissions))
                    {
                        var response = await _roleServices.GetRole(PermissionHelpers.Authenticate(PermissionConstants.Role_Index_Edit, user.Permissions));
                        response.Set(user.Role);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> GetDetailAsync(long id)
        {
            try
            {
                var response = await _roleServices.GetRole(id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError("RoleController => GetDetailAsync: " + ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync(RoleUpdateRequest? request)
        {
            try
            {
                if (request != null)
                {
                    var response = await _roleServices.UpdateAsync(request);
                    if (response)
                        return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleController => UpdateAsync: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
