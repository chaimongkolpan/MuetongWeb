using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class StoreServices : IStoreServices
    {
        private readonly ILogger<StoreServices> _logger;
        private readonly IStoreRepositories _storeRepositories;
        private readonly IProvinceRepositories _provinceRepositories;
        private readonly IPaymentAccountRepositories _paymentAccountRepositories;
        public StoreServices
        (
            ILogger<StoreServices> logger,
            IStoreRepositories storeRepositories,
            IProvinceRepositories provinceRepositories,
            IPaymentAccountRepositories paymentAccountRepositories
        )
        {
            _logger = logger;
            _storeRepositories = storeRepositories;
            _provinceRepositories = provinceRepositories;
            _paymentAccountRepositories = paymentAccountRepositories;
        }
        public async Task<StoreCollectionResponse> GetAsync(StoreRequest request)
        {
            try
            {
                var stores = await _storeRepositories.GetAsync(request);
                var response = new StoreCollectionResponse(stores);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => GetAsync: " + ex.Message);
                return new StoreCollectionResponse();
            }
        }
        public async Task<bool> AddAsync(StoreAddRequest request)
        {
            try
            {
                var store = new Store()
                {
                    Name = request.Name,
                    Address = request.Address,
                    ProvinceId = request.ProvinceId,
                    PhoneNo = request.PhoneNo,
                    TaxNo = request.TaxNo,
                    ContractName = request.ContractName,
                    Email = request.Email,
                    UserId = request.User.Id,
                    CreateDate = DateTime.Now
                };
                await _storeRepositories.AddAsync(store);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => AddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateAsync(long id, StoreUpdateRequest request)
        {
            try
            {
                var store = await _storeRepositories.GetAsync(id);
                if (store == null)
                    return false;
                store.Name = request.Name;
                store.Address = request.Address;
                store.ProvinceId = request.ProvinceId;
                store.PhoneNo = request.PhoneNo;
                store.TaxNo = request.TaxNo;
                store.ContractName = request.ContractName;
                store.Email = request.Email;
                store.ModifyDate = DateTime.Now;
                await _storeRepositories.UpdateAsync(store);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => UpdateAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                await _storeRepositories.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => DeleteAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<long> AddAccountAsync(PaymentAccountAddRequest request)
        {
            try
            {
                var acc = new PaymentAccount()
                {
                    StoreId = request.StoreId,
                    AccountName = request.AccountName,
                    AccountNo = request.AccountNo,
                    Bank = request.Bank,
                    Type = request.Type
                };
                await _paymentAccountRepositories.AddAsync(acc);
                return acc.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => AddAccountAsync: " + ex.Message);
                return 0;
            }
        }
        public async Task<bool> UpdateAccountAsync(long id, PaymentAccountUpdateRequest request)
        {
            try
            {
                var acc = await _paymentAccountRepositories.GetAsync(id);
                if (acc == null)
                    return false;
                acc.AccountNo = request.AccountNo;
                acc.AccountName = request.AccountName;
                acc.Bank = request.Bank;
                acc.Type = request.Type;
                await _paymentAccountRepositories.UpdateAsync(acc);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => UpdateAccountAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAccountAsync(long id)
        {
            try
            {
                await _paymentAccountRepositories.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("StoreServices => DeleteAccountAsync: " + ex.Message);
                return false;
            }
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
                _logger.LogError("StoreServices => GetProvince: " + ex.Message);
                throw;
            }
        }
    }
}
