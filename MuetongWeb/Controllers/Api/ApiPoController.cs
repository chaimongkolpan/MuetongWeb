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

namespace MuetongWeb.Controllers.Api
{
    [Route("api/po")]
    public class ApiPoController : ControllerBase
    {
        private readonly ILogger<ApiPoController> _logger;
        private readonly IPoServices _poServices;
        private readonly IPrServices _prServices;
        public ApiPoController(ILogger<ApiPoController> logger, IPoServices poServices, IPrServices prServices)
        {
            _logger = logger;
            _poServices = poServices;
            _prServices = prServices;
        }

        public IActionResult Index()
        {
            return Ok(new { Status = true });
        }

        [Route("Cancel/{id}")]
        [HttpGet]
        public async Task<IActionResult> PoIndexCancel(long id, [FromQuery]string? remark)
        {
            try
            {
                return Ok(await _poServices.Cancel(id, remark));
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexCancel: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Approve/{id}")]
        [HttpPost]
        public async Task<IActionResult> PoIndexApprove(long id, PoApproveRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        return Ok(await _poServices.Approve(id, user.Id, request));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexAdd: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Read/{id}")]
        [HttpGet]
        public async Task<IActionResult> PoIndexRead(long id)
        {
            try
            {
                return Ok(await _poServices.Read(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexRead: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> PoIndexUpdate(long id, PoIndexUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(request.JsonDetails)) request.Details = JsonConvert.DeserializeObject<List<PoDetailUpdateRequest>>(request.JsonDetails);
                        if (!string.IsNullOrWhiteSpace(request.JsonOther)) request.Other = JsonConvert.DeserializeObject<PoDetailUpdateRequest>(request.JsonOther);
                        var response = await _poServices.UpdateIndexPo(id, user.Id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexUpdate: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> PoIndexAdd(PoIndexAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        if (!string.IsNullOrWhiteSpace(request.JsonDetails)) request.Details = JsonConvert.DeserializeObject<List<PoDetailRequest>>(request.JsonDetails);
                        if (!string.IsNullOrWhiteSpace(request.JsonOther)) request.Other = JsonConvert.DeserializeObject<PoDetailRequest>(request.JsonOther);
                        var response = await _poServices.IndexAddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexAdd: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Search")]
        [HttpPost]
        public async Task<IActionResult> PoIndexSearch(PoIndexSearchRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _poServices.IndexSearchAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexSearch: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("SearchPr")]
        [HttpPost]
        public async Task<IActionResult> PoIndexPrSearch(PoIndexPrSearch request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _poServices.IndexSearchPrAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoIndexPrSearch: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Requester/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetPrRequester(long projectId)
        {
            try
            {
                var response = await _poServices.GetRequesterByProject(projectId);
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
                var response = await _poServices.GetPrNoByProject(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetPrNo: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("PoNo/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetPoNo(long projectId)
        {
            try
            {
                var response = await _poServices.GetPoNoByProject(projectId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetPoNo: " + ex.Message);
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
        [Route("Store")]
        [HttpGet]
        public async Task<IActionResult> GetStore()
        {
            try
            {
                var response = await _poServices.GetStore();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetStore: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Receive")]
        [HttpGet]
        public async Task<IActionResult> GetReceive()
        {
            try
            {
                var response = await _poServices.GetReceive();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetReceive: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Type")]
        [HttpGet]
        public async Task<IActionResult> GetTypeAsync()
        {
            try
            {
                var response = await _poServices.GetTypeAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetType: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("NextPo")]
        [HttpGet]
        public async Task<IActionResult> GetNextPoNoAsync()
        {
            try
            {
                var response = await _poServices.GetPoNo();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPrController => GetNextPoNoAsync: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Files/{id}/{type}")]
        [HttpGet]
        public async Task<IActionResult> GetFiles(long id, string type)
        {
            try
            {
                var response = await _poServices.GetFiles(id, type);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => GetFiles: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("DisapproveReceive/{poId}")]
        [HttpGet]
        public async Task<IActionResult> PoDisapproveReceiveAsync(long poId)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _poServices.DisapproveReceiveAsync(poId);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiPoController => PoDisapproveReceiveAsync: " + ex.Message);
            }
            return BadRequest();
        }
    }
}

