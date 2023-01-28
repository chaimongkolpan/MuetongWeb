using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Requests;
using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Controllers.Api
{
    [Route("api/user")]
    public class ApiUserController : ControllerBase
    {
        private readonly ILogger<ApiUserController> _logger;
        private readonly IUserServices _userServices;
        public ApiUserController(ILogger<ApiUserController> logger, IUserServices userServices)
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
                    if (user != null)
                    {
                        return Ok(user);
                    }
                    HttpContext.Session.Clear();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => Index: " + ex.Message);
            }
            return BadRequest("Can not get permission");
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _userServices.GetUserAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => Get: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _userServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, UserUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _userServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => Update: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _userServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Province")]
        [HttpGet]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _userServices.GetProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => GetProvince: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Workline")]
        [HttpGet]
        public async Task<IActionResult> GetWorkLine()
        {
            try
            {
                var response = await _userServices.GetWorkLine();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => GetWorkLine: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Department/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDepartment(long id)
        {
            try
            {
                var response = await _userServices.GetDepartment(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => GetDepartment: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("SubDepartment/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSubDepartment(long id)
        {
            try
            {
                var response = await _userServices.GetSubDepartment(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiUserController => GetSubDepartment: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
