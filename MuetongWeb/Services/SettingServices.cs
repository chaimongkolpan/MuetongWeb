using MuetongWeb.Constants;
using MuetongWeb.Mappers;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;

namespace MuetongWeb.Services
{
    public class SettingServices : ISettingServices
    {
        private readonly ILogger<SettingServices> _logger;
        private readonly IFileServices _fileServices;

        private readonly ICustomerRepositories _customerRepositories;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IContractorRepositories _contractorRepositories;
        private readonly IStoreRepositories _storeRepositories;
        private readonly IPaymentAccountRepositories _paymentAccountRepositories;
        private readonly IProductRepositories _productRepositories;
        private readonly ISettingConstantRepositories _settingConstantRepositories;
        public SettingServices
        (
            ILogger<SettingServices> logger,
            IFileServices fileServices,
            ICustomerRepositories customerRepositories,
            IProjectRepositories projectRepositories,
            IContractorRepositories contractorRepositories,
            IStoreRepositories storeRepositories,
            IPaymentAccountRepositories paymentAccountRepositories,
            IProductRepositories productRepositories,
            ISettingConstantRepositories settingConstantRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;

            _customerRepositories = customerRepositories;
            _projectRepositories = projectRepositories;
            _contractorRepositories = contractorRepositories;
            _storeRepositories = storeRepositories;
            _paymentAccountRepositories = paymentAccountRepositories;
            _productRepositories = productRepositories;
            _settingConstantRepositories = settingConstantRepositories;
        }
        public async Task<SettingCollectionResponse> GetAll()
        {
            var settings = await _settingConstantRepositories.GetAsync();
            var response = new SettingCollectionResponse(settings);
            return response;
        }
        public async Task<bool> Add(string name, string type)
        {
            var setting = new SettingConstant()
            {
                Name = name,
                Detail = name,
                Type = type
            };
            await _settingConstantRepositories.AddAsync(setting);
            return true;
        }
        public async Task<bool> Delete(long id)
        {
            await _settingConstantRepositories.DeleteAsync(id);
            return true;
        }
        public async Task<SettingResponse> ImportCustomerFileAsync(SettingImportCustomerRequest request, long userId)
        {
            try
            {
                var schemas = new List<ExcelDataSchema>()
                {
                    ExcelSchemaConstant.Customer,
                    ExcelSchemaConstant.Project,
                    ExcelSchemaConstant.ProjectCode,
                    ExcelSchemaConstant.Contractor,
                    ExcelSchemaConstant.ProjectContractor
                };
                var result = await _fileServices.ReadExcel(request, schemas);
                if (result == null)
                    return new SettingResponse("No data");
                if (result.CustomerDatas != null && result.CustomerDatas.Any())
                {
                    foreach (var customerData in result.CustomerDatas)
                    {
                        var customer = CustomerMappers.ExcelData(customerData, userId);
                        if (customer != null)
                        {
                            if (await _customerRepositories.AddAsync(customer))
                            {
                                customerData.InsertedId = customer.Id;
                            }
                        }
                    }
                    if (result.ProjectDatas != null && result.ProjectDatas.Any())
                    {
                        foreach (var projectData in result.ProjectDatas)
                        {
                            var customer = result.CustomerDatas.FirstOrDefault(customer => customer.Id == projectData.CustomerId);
                            if (customer != null && customer.InsertedId.HasValue)
                            {
                                var project = ProjectMappers.ExcelData(projectData, userId);
                                if (project != null)
                                {
                                    project.CustomerId = customer.InsertedId.Value;
                                    if (await _projectRepositories.AddAsync(project))
                                    {
                                        projectData.InsertedId = project.Id;
                                    }
                                }
                            }
                        }
                        if (result.ProjectCodeDatas != null && result.ProjectCodeDatas.Any())
                        {
                            foreach (var projectCodeData in result.ProjectCodeDatas)
                            {
                                var project = result.ProjectDatas.FirstOrDefault(project => project.Id == projectCodeData.ProjectId);
                                if (project != null && project.InsertedId.HasValue)
                                {
                                    var projectCode = ProjectCodeMappers.ExcelData(projectCodeData);
                                    if (projectCode != null)
                                    {
                                        projectCode.ProjectId = project.InsertedId.Value;
                                        await _projectRepositories.AddCodeAsync(projectCode);
                                    }
                                }
                            }
                        }
                    }
                }
                if (result.ContractorDatas != null && result.ContractorDatas.Any())
                {
                    foreach (var contractorData in result.ContractorDatas)
                    {
                        var contractor = ContractorMappers.ExcelData(contractorData, userId);
                        if (contractor != null)
                        {
                            await _contractorRepositories.AddAsync(contractor);
                        }
                    }
                    if (result.CustomerDatas != null && result.CustomerDatas.Any()
                        && result.ProjectDatas != null && result.ProjectDatas.Any()
                        && result.ProjectContractorDatas != null && result.ProjectContractorDatas.Any())
                    {
                        List<ProjectContractor> projectContractors = new List<ProjectContractor>();
                        foreach (var projectContractorData in result.ProjectContractorDatas)
                        {
                            var projectContractor = ProjectContractorMappers.ExcelData(projectContractorData);
                            if (projectContractor != null)
                            {
                                projectContractors.Add(projectContractor);
                            }
                        }
                        if (projectContractors.Any())
                        {
                            await _projectRepositories.AddContractorAsync(projectContractors);
                        }
                    }
                }
                    
                return new SettingResponse();
            }
            catch(Exception ex)
            {
                _logger.LogError("SettingServices => ImportCustomerFileAsync: " + ex.Message);
                return new SettingResponse(MessageConstants.SettingInvalidFileFormat);
            }
        }
        public async Task<SettingResponse> ImportStoreFileAsync(SettingImportStoreRequest request, long userId)
        {
            try
            {
                var schemas = new List<ExcelDataSchema>()
                {
                    ExcelSchemaConstant.Store,
                    ExcelSchemaConstant.Payment,
                    ExcelSchemaConstant.Product
                };
                var result = await _fileServices.ReadExcel(request, schemas);
                if (result == null)
                    return new SettingResponse("No data");
                if (result.StoreDatas != null && result.StoreDatas.Any())
                {
                    foreach (var storeData in result.StoreDatas)
                    {
                        var store = StoreMappers.ExcelData(storeData, userId);
                        if (store != null)
                        {
                            if (await _storeRepositories.AddAsync(store))
                            {
                                storeData.InsertedId = store.Id;
                            }
                        }
                    }
                    if (result.PaymentDatas != null && result.PaymentDatas.Any())
                    {
                        List<PaymentAccount> payments = new List<PaymentAccount>();
                        foreach (var paymentData in result.PaymentDatas)
                        {
                            var store = result.StoreDatas.FirstOrDefault(store => store.Id == paymentData.StoreId);
                            if (store != null && store.InsertedId.HasValue)
                            {
                                var payment = PaymentAccountMappers.ExcelData(paymentData);
                                if (payment != null)
                                {
                                    payments.Add(payment);
                                }
                            }
                        }
                        if (payments.Any())
                        {
                            await _paymentAccountRepositories.AddAsync(payments);
                        }
                    }
                }
                if (result.ProductDatas != null && result.ProductDatas.Any())
                {
                    List<Product> products = new List<Product>();
                    foreach (var productData in result.ProductDatas)
                    {
                        var product = ProductMappers.ExcelData(productData, userId);
                        if (product != null)
                        {
                            products.Add(product);
                        }
                    }
                    if (products.Any())
                    {
                        await _productRepositories.AddAsync(products);
                    }
                }
                return new SettingResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError("SettingServices => ImportStoreFileAsync: " + ex.Message);
                return new SettingResponse(MessageConstants.SettingInvalidFileFormat);
            }
        }
    }
}
