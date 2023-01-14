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
    public class ApiPoController : Controller
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
    }
}

