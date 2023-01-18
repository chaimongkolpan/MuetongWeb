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
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> BillingIndexUpdate(BillingIndexUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _billingServices.UpdateAsync(request);
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
    }
}
