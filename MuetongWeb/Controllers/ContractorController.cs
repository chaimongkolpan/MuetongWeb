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
    public class ContractorController : Controller
    {
        private readonly ILogger<ContractorController> _logger;
        private readonly IContractorServices _contractorServices;
        public ContractorController(ILogger<ContractorController> logger, IContractorServices contractorServices)
        {
            _logger = logger;
            _contractorServices = contractorServices;
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
                        ContractorRequest request = RequestHelpers.GetContractorRequest(Request);
                        var response = await _contractorServices.GetAsync(request);
                        if (response != null)
                            return View(response);
                        else
                            return View(new ContractorCollectionResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
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
                        var response = await _contractorServices.GetAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Get: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ContractorAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _contractorServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, ContractorUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _contractorServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Update: " + ex.Message);
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
                        var response = await _contractorServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Province")]
        [HttpGet]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _contractorServices.GetProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorController => GetProvince: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
