using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Services;
using MuetongWeb.Services.Interfaces;
using Newtonsoft.Json;


namespace MuetongWeb.Controllers.Api
{
    [Route("api/billing")]
    public class ApiBillingController : ControllerBase
    {
        private readonly ILogger<ApiBillingController> _logger;
        private readonly IPoServices _poServices;
        private readonly IBillingServices _billingServices;
        public ApiBillingController(ILogger<ApiBillingController> logger, IPoServices poServices, IBillingServices billingServices)
        {
            _logger = logger;
            _poServices = poServices;
            _billingServices = billingServices;
        }
        public IActionResult Index()
        {
            return Ok(new { Status = true });
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> BillingIndexAdd(BillingIndexAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(request.JsonDetails)) request.Pos = JsonConvert.DeserializeObject<List<PoResponse>>(request.JsonDetails);
                        request.User = user;
                        var response = await _billingServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => BillingIndexAdd: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("SendApprove/{id}")]
        [HttpGet]
        public async Task<IActionResult> BillingIndexSendApprove(long id)
        {
            try
            {
                var response = await _billingServices.SendApproveAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => BillingIndexSendApprove: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> BillingIndexUpdate(long id, BillingIndexUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _billingServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => BillingIndexUpdate: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Search")]
        [HttpPost]
        public async Task<IActionResult> BillingIndexSearch(BillingIndexSearch request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _billingServices.Search(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => BillingIndexSearch: " + ex.Message);
            }
            return BadRequest();
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
                _logger.LogError("ApiBillingController => GetStore: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("SearchPo")]
        [HttpPost]
        public async Task<IActionResult> BillingIndexPoSearch(BillingIndexPoSearch request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _billingServices.SearchPo(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => BillingIndexPoSearch: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("PrNo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPrNo(long id)
        {
            try
            {
                var response = await _billingServices.GetPrNoByProject(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => GetPrNo: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("PoNo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPoNo(long id)
        {
            try
            {
                var response = await _billingServices.GetPoNoByProject(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => GetPoNo: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("BillingNo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBillingNo(long id)
        {
            try
            {
                var response = await _billingServices.GetBillingNoByProject(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => GetBillingNo: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Requester/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRequester(long id)
        {
            try
            {
                var response = await _billingServices.GetRequesterByProject(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => GetRequester: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Approve/{id}")]
        [HttpGet]
        public async Task<IActionResult> Approve(long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var request = new BillingApproveRequest(user);
                        var response = await _billingServices.ApproveAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => Approve: " + ex.Message);
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
        [Route("Read/{id}")]
        [HttpGet]
        public async Task<IActionResult> Read(long id)
        {
            try
            {
                var response = await _billingServices.ReadAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => Read: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Cancel/{id}")]
        [HttpGet]
        public async Task<IActionResult> Cancel(long id, string? remark)
        {
            try
            {
                var request = new BillingCancelRequest(remark);
                var response = await _billingServices.CancelAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => Cancel: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Payment/{id}")]
        [HttpGet]
        public async Task<IActionResult> Payment(long id)
        {
            try
            {
                var response = await _billingServices.GetPaymentByBill(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => Payment: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Route("Files/{id}/{type}")]
        [HttpGet]
        public async Task<IActionResult> GetFiles(long id, string type)
        {
            try
            {
                var response = await _billingServices.GetFiles(id, type);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("ApiBillingController => GetFiles: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
