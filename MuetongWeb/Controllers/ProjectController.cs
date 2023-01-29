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
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectServices _projectServices;
        public ProjectController(ILogger<ProjectController> logger, IProjectServices projectServices)
        {
            _logger = logger;
            _projectServices = projectServices;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Setting_Index_View, user.Permissions))
                    {
                        var response = await _projectServices.GetAsync();
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ProjectAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _projectServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, ProjectUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => Update: " + ex.Message);
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
                        var response = await _projectServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Province")]
        [HttpGet]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _projectServices.GetProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => GetProvince: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Customer")]
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var response = await _projectServices.GetCustomer();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => GetCustomer: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("User")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var response = await _projectServices.GetUserAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => GetUser: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Contractor")]
        [HttpGet]
        public async Task<IActionResult> GetContractor()
        {
            try
            {
                var response = await _projectServices.GetContractorAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => GetContractor: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{pid}/User/Add/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddUser(long pid, long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.AddUser(pid, id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => AddUser: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{pid}/User/Delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(long pid, long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.DeleteUser(pid, id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => DeleteUser: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{pid}/Contractor/Add/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddContractor(long pid, long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.AddContractor(pid, id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => AddContractor: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{pid}/Contractor/Delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteContractor(long pid, long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.DeleteContractor(pid, id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProjectController => DeleteContractor: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{pid}/Code/Import")]
        [HttpPost]
        public async Task<IActionResult> Import(long pid, ProjectCodeImportRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _projectServices.ImportCode(pid, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Import: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
