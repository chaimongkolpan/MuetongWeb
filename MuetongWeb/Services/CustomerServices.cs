using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ILogger<CustomerServices> _logger;
        private readonly ICustomerRepositories _customerRepositories;
        private readonly IProvinceRepositories _provinceRepositories;
        public CustomerServices
        (
            ILogger<CustomerServices> logger,
            ICustomerRepositories customerRepositories,
            IProvinceRepositories provinceRepositories
        )
        {
            _logger = logger;
            _customerRepositories = customerRepositories;
            _provinceRepositories = provinceRepositories;
        }
        public async Task<CustomerCollectionResponse?> GetAsync(CustomerRequest request)
        {
            try
            {
                var customers = await _customerRepositories.GetAsync(request.Query, request.ProvinceId, request.Page, request.PageSize);
                if (!customers.Any())
                    return null;
                var count = await _customerRepositories.CountAsync(request.Query, request.ProvinceId);
                var response = new CustomerCollectionResponse(customers, count, request.Page, request.PageSize);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerServices => GetAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<CustomerResponse?> GetAsync(long id)
        {
            try
            {
                var customer = await _customerRepositories.GetAsync(id);
                if (customer == null)
                    return null;
                var response = new CustomerResponse(customer);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerServices => GetAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> AddAsync(CustomerAddRequest request)
        {
            var customer = new Customer()
            {
                Name = request.Name,
                Detail = request.Detail,
                Address = request.Address,
                ProvinceId = request.ProvinceId,
                PhoneNo = request.PhoneNo,
                Email = request.Email,
                TaxNo = request.TaxNo,
                BranchNo = request.BranchNo,
                UserId = request.User.Id,
                CreateDate = DateTime.Now
            };
            await _customerRepositories.AddAsync(customer);
            return true;
        }
        public async Task<bool> UpdateAsync(long id, CustomerUpdateRequest request)
        {
            var customer = await _customerRepositories.GetAsync(id);
            if (customer == null)
                return false;
            customer.Name = request.Name;
            customer.Detail = request.Detail;
            customer.Address = request.Address;
            customer.ProvinceId = request.ProvinceId;
            customer.PhoneNo = request.PhoneNo;
            customer.Email = request.Email;
            customer.TaxNo = request.TaxNo;
            customer.BranchNo = request.BranchNo;
            customer.ModifyDate = DateTime.Now;
            await _customerRepositories.UpdateAsync(customer);
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _customerRepositories.DeleteAsync(id);
            return true;
        }
        public async Task<IEnumerable<Province>> GetProvince()
        {
            try
            {
                var provinces = await _provinceRepositories.GetAsync();
                return provinces;
            }
            catch (Exception ex)
            {
                _logger.LogError("CustomerServices => GetProvince: " + ex.Message);
                throw;
            }
        }
    }
}
