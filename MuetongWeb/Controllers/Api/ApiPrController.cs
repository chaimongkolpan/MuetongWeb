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
    [Route("api/pr")]
    public class ApiPrController : ControllerBase
    {
        private readonly ILogger<ApiPrController> _logger;
        private readonly IPrServices _prServices;
        public ApiPrController(ILogger<ApiPrController> logger, IPrServices prServices)
        {
            _logger = logger;
            _prServices = prServices;
        }

        public IActionResult Index()
        {
            return Ok(new { Status = true });
        }
        #region Pr
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> PrIndexAdd(PrIndexSearchRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null && PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_View, user.Permissions))
                    {
                        var response = await _prServices.IndexSearchAsync(PermissionHelpers.Authenticate(PermissionConstants.Pr_Index_Edit, user.Permissions), request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrIndexAdd: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Search")]
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
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrIndexSearch: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Requester/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetPrRequester(long projectId)
        {
            try
            {
                var response = await _prServices.GetRequesterByProject(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetPrRequester: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("PrNo/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetPrNo(long projectId)
        {
            try
            {
                var response = await _prServices.GetPrNoByProject(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetPrNo: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Contractor/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetContractor(long projectId)
        {
            try
            {
                var response = await _prServices.GetContractorByProject(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetContractor: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Product")]
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var response = await _prServices.GetProduct();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetProduct: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Code/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetProjectCode(long projectId)
        {
            try
            {
                var response = await _prServices.GetProjectCode(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetProjectCode: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

