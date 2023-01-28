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
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerServices _customerServices;
        public CustomerController(ILogger<CustomerController> logger, ICustomerServices customerServices)
        {
            _logger = logger;
            _customerServices = customerServices;
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
                        CustomerRequest request = RequestHelpers.GetCustomerRequest(Request);
                        var response = await _customerServices.GetAsync(request);
                        if (response != null)
                            return View(response);
                        else
                            return View(new CustomerCollectionResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => Index: " + ex.Message);
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
                        var response = await _customerServices.GetAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => Get: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(CustomerAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _customerServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, CustomerUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _customerServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => Update: " + ex.Message);
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
                        var response = await _customerServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Province")]
        [HttpGet]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _customerServices.GetProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerController => GetProvince: " + ex.Message);
            }
            return BadRequest();
        }

    }
}
