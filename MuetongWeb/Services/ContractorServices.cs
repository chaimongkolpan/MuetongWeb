using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class ContractorServices : IContractorServices
    {
        private readonly ILogger<ContractorServices> _logger;
        private readonly IContractorRepositories _contractorRepositories;
        private readonly IProvinceRepositories _provinceRepositories;
        public ContractorServices
        (
            ILogger<ContractorServices> logger,
            IContractorRepositories contractorRepositories,
            IProvinceRepositories provinceRepositories
        )
        {
            _logger = logger;
            _contractorRepositories = contractorRepositories;
            _provinceRepositories = provinceRepositories;
        }
        public async Task<ContractorCollectionResponse?> GetAsync(ContractorRequest request)
        {
            try
            {
                var contractors = await _contractorRepositories.GetAsync(request.Query, request.ProvinceId, request.Page, request.PageSize);
                if (!contractors.Any())
                    return null;
                var count = await _contractorRepositories.CountAsync(request.Query, request.ProvinceId);
                var response = new ContractorCollectionResponse(contractors, count, request.Page, request.PageSize);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorServices => GetAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<ContractorResponse?> GetAsync(long id)
        {
            try
            {
                var contractor = await _contractorRepositories.GetAsync(id);
                if (contractor == null)
                    return null;
                var response = new ContractorResponse(contractor);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("ContractorServices => GetAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> AddAsync(ContractorAddRequest request)
        {
            var contractor = new Contractor()
            {
                Name = request.Name,
                Address = request.Address,
                ProvinceId = request.ProvinceId,
                PhoneNo = request.PhoneNo,
                Email = request.Email,
                TaxNo = request.TaxNo,
                Type = request.Type,
                DirectorName = request.DirectorName,
                UserId = request.User.Id,
                CreateDate = DateTime.Now
            };
            await _contractorRepositories.AddAsync(contractor);
            return true;
        }
        public async Task<bool> UpdateAsync(long id, ContractorUpdateRequest request)
        {
            var contractor = await _contractorRepositories.GetAsync(id);
            if (contractor == null)
                return false;
            contractor.Name = request.Name;
            contractor.Address = request.Address;
            contractor.ProvinceId = request.ProvinceId;
            contractor.PhoneNo = request.PhoneNo;
            contractor.Email = request.Email;
            contractor.TaxNo = request.TaxNo;
            contractor.Type = request.Type;
            contractor.DirectorName = request.DirectorName;
            contractor.ModifyDate = DateTime.Now;
            await _contractorRepositories.UpdateAsync(contractor);
            return true;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _contractorRepositories.DeleteAsync(id);
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
                _logger.LogError("ContractorServices => GetProvince: " + ex.Message);
                throw;
            }
        }
    }
}
