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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;
        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.User_Index_View, user.Permissions))
                    {
                        UserRequest request = RequestHelpers.GetUserRequest(Request);
                        var response = await _userServices.GetUserAsync(request);
                        if (response != null)
                            return View(response);
                        else
                            return View(new UserCollectionResponse(MessageConstants.UserNotFound));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _userServices.GetUserAsync(user.Id);
                        if (response != null)
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
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(UserChangePasswordRequest? request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _userServices.GetUserAsync(user.Id);
                        if (response != null)
                        {
                            if (request == null)
                            {
                                response.SetMessage(MessageConstants.UserChangePasswordRequestInvalid);
                            }
                            else
                            {
                                (bool IsSuccess, string Message) result = await _userServices.ChangePasswordAsync(user.Id, request);
                                if(result.IsSuccess)
                                    response.SetSuccess(result.Message);
                                else
                                    response.SetMessage(result.Message);
                            }
                            return View(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoleController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
    }
}
