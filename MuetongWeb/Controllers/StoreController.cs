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
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IStoreServices _storeServices;
        public StoreController(ILogger<StoreController> logger, IStoreServices storeServices)
        {
            _logger = logger;
            _storeServices = storeServices;
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
                        StoreRequest request = new StoreRequest();
                        var response = await _storeServices.GetAsync(request);
                        if (response != null)
                            return View(response);
                        else
                            return View(new StoreCollectionResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(StoreAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _storeServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, StoreUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _storeServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => Update: " + ex.Message);
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
                        var response = await _storeServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Province")]
        [HttpGet]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _storeServices.GetProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => GetProvince: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{storeid}/Account/Add")]
        [HttpPost]
        public async Task<IActionResult> AddAccount(long storeid, PaymentAccountAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _storeServices.AddAccountAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => AddAccount: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{storeid}/Account/Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAccount(long storeid, long id, PaymentAccountUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _storeServices.UpdateAccountAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => UpdateAccount: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("{storeid}/Account/Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(long storeid, long id)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _storeServices.DeleteAccountAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreController => DeleteAccount: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
