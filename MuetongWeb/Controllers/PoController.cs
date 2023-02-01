using Microsoft.AspNetCore.Mvc;
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
    public class PoController : Controller
    {
        private readonly ILogger<PoController> _logger;
        private readonly IPoServices _poServices;
        private readonly IFileServices _fileServices;
        public PoController(ILogger<PoController> logger, IPoServices poServices, IFileServices fileServices)
        {
            _logger = logger;
            _poServices = poServices;
            _fileServices = fileServices;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Po_Index_View, user.Permissions))
                    {
                        var response = await _poServices.IndexAsync(PermissionHelpers.Authenticate(PermissionConstants.Po_Index_Edit, user.Permissions), user);
                        response.Set(user);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("PoController => Index: " + ex.Message);
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
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Po_Approver_View, user.Permissions))
                    {
                        var response = await _poServices.ApproverAsync(PermissionHelpers.Authenticate(PermissionConstants.Po_Approver_Edit, user.Permissions), user);
                        response.Set(user);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("PoController => Approver: " + ex.Message);
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
                _logger.LogError("PoController => GetFile: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
