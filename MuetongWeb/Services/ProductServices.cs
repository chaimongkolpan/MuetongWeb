using MuetongWeb.Services.Interfaces;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Models.Responses;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ILogger<ProductServices> _logger;
        private readonly IProductRepositories _productRepositories;
        public ProductServices
        (
            ILogger<ProductServices> logger,
            IProductRepositories productRepositories
        )
        {
            _logger = logger;
            _productRepositories = productRepositories;
        }
        public async Task<List<ProductResponse>> GetAsync(ProductRequest request)
        {
            try
            {
                var products = await _productRepositories.GetAsync(request);
                var response = new List<ProductResponse>();
                foreach (var product in products)
                {
                    response.Add(new ProductResponse(product));
                }
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("ProductServices => GetAsync: " + ex.Message);
                return new List<ProductResponse>();
            }
        }
        public async Task<bool> AddAsync(ProductAddRequest request)
        {
            try
            {
                var product = new Product()
                {
                    Name = request.Name,
                    Unit = request.Unit,
                    UserId = request.User.Id,
                    CreateDate = DateTime.Now
                };
                await _productRepositories.AddAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductServices => AddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateAsync(long id, ProductUpdateRequest request)
        {
            try
            {
                var product = await _productRepositories.GetAsync(id);
                if (product == null)
                    return false;
                product.Name = request.Name;
                product.Unit = request.Unit;
                product.ModifyDate = DateTime.Now;
                await _productRepositories.UpdateAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductServices => UpdateAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                await _productRepositories.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductServices => DeleteAsync: " + ex.Message);
                return false;
            }
        }
    }
}
