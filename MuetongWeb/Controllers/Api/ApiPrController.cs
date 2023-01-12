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
using Newtonsoft.Json;

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
        public async Task<IActionResult> PrIndexAdd(PrIndexAddRequest? request)
        {
            try
            {
                if (request == null)
                    return BadRequest();
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        if(!string.IsNullOrWhiteSpace(request.JsonDetails))
                        request.Details = JsonConvert.DeserializeObject<List<PrDetailRequest>>(request.JsonDetails);
                        var response = await _prServices.IndexAddAsync(user.Id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrIndexAdd: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> PrIndexUpdate(long id, PrIndexUpdateRequest? request)
        {
            try
            {
                if (request == null)
                    return BadRequest();
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(request.JsonDetails))
                            request.Details = JsonConvert.DeserializeObject<List<PrDetailUpdateRequest>>(request.JsonDetails);
                        var response = await _prServices.IndexUpdateAsync(id, user.Id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrIndexUpdate: " + ex.Message);
            }
            return BadRequest();
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
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _prServices.IndexSearchAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrIndexSearch: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> PrById(long id)
        {
            try
            {
                var response = await _prServices.Get(id);
                if (response == null)
                    return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrById: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Approve/{id}")]
        [HttpGet]
        public async Task<IActionResult> PrApprove(long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _prServices.Approve(id, user.Id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrApprove: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Cancel/{id}")]
        [HttpGet]
        public async Task<IActionResult> PrCancel(long id)
        {
            try
            {
                return Ok(await _prServices.Cancel(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrCancel: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Read/{id}")]
        [HttpGet]
        public async Task<IActionResult> PrRead(long id)
        {
            try
            {
                return Ok(await _prServices.Read(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => PrRead: " + ex.Message);
            }
            return BadRequest();
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

