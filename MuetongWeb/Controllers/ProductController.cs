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
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductServices _productServices;
        public ProductController(ILogger<ProductController> logger, IProductServices productServices)
        {
            _logger = logger;
            _productServices = productServices;
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
                        ProductRequest request = new ProductRequest();
                        var response = await _productServices.GetAsync(request);
                        if (response != null)
                            return View(response);
                        else
                            return View(new List<ProductResponse>());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController => Index: " + ex.Message);
            }
            return Redirect(ViewConstants.DefaultHomePage);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        request.User = user;
                        var response = await _productServices.AddAsync(request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController => Add: " + ex.Message);
            }
            return BadRequest();
        }
        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(long id, ProductUpdateRequest request)
        {
            try
            {
                if (SessionHelpers.SessionAlive(HttpContext.Session))
                {
                    var user = SessionHelpers.GetUserInfo(HttpContext.Session);
                    if (user != null)
                    {
                        var response = await _productServices.UpdateAsync(id, request);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController => Update: " + ex.Message);
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
                        var response = await _productServices.DeleteAsync(id);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductController => Delete: " + ex.Message);
            }
            return BadRequest();
        }
    }
}
