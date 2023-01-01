using MuetongWeb.Constants;
using MuetongWeb.Models.Requests;
using MuetongWeb.Services.Interfaces;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace MuetongWeb.Services
{
    public class FileServices : IFileServices
    {
        private readonly ILogger<FileServices> _logger;
        public FileServices
        (
            ILogger<FileServices> logger
        )
        {
            _logger = logger;
        }
        public async Task<SettingImportCustomerDataModel?> ReadExcel(SettingImportCustomerRequest request, List<ExcelDataSchema> schemas)
        {
            try
            {
                if (request.CustomerFile == null)
                    return null;
                var ms = new MemoryStream();
                await request.CustomerFile.CopyToAsync(ms);
                var excel = new ExcelPackage(ms);
                var response = new SettingImportCustomerDataModel();
                foreach (ExcelDataSchema schema in schemas)
                {
                    #region Customer
                    if (schema.SheetName == "Customer")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelCustomerData> customer = new List<ExcelCustomerData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelCustomerData customerData = new ExcelCustomerData();
                            customerData.Id = sheet.GetValue<long>(rowIndex, 1);
                            customerData.Name = sheet.GetValue<string>(rowIndex, 2);
                            if (string.IsNullOrWhiteSpace(customerData.Name))
                                break;
                            customerData.Detail = sheet.GetValue<string?>(rowIndex, 3);
                            customerData.Address = sheet.GetValue<string?>(rowIndex, 4);
                            customerData.ProvinceId = sheet.GetValue<long?>(rowIndex, 5);
                            customerData.PhoneNo = sheet.GetValue<string?>(rowIndex, 6);
                            customerData.Email = sheet.GetValue<string?>(rowIndex, 7);
                            customerData.TaxNo = sheet.GetValue<string?>(rowIndex, 8);
                            customerData.BranchNo = sheet.GetValue<string?>(rowIndex, 9);
                            //for (int i = 0; i < schema.Datas.Count; i++)
                            //{
                            //    var dataSchema = schema.Datas[i];
                            //    var value = sheet.GetValue(rowIndex, i);
                            //}
                            customer.Add(customerData);
                            rowIndex++;
                        }
                        response.CustomerDatas = customer;
                    }
                    #endregion
                    #region Project
                    if (schema.SheetName == "Project")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelProjectData> project = new List<ExcelProjectData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelProjectData projectData = new ExcelProjectData();
                            projectData.Id = sheet.GetValue<long>(rowIndex, 1);
                            projectData.Name = sheet.GetValue<string>(rowIndex, 2);
                            if (string.IsNullOrWhiteSpace(projectData.Name))
                                break;
                            projectData.ContractNo = sheet.GetValue<string>(rowIndex, 3);
                            projectData.Address = sheet.GetValue<string?>(rowIndex, 4);
                            projectData.ProvinceId = sheet.GetValue<long?>(rowIndex, 5);
                            projectData.CustomerId = sheet.GetValue<long>(rowIndex, 6);
                            project.Add(projectData);
                            rowIndex++;
                        }
                        response.ProjectDatas = project;
                    }
                    #endregion
                    #region ProjectCode
                    if (schema.SheetName == "ProjectCode")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelProjectCodeData> projectCode = new List<ExcelProjectCodeData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelProjectCodeData projectCodeData = new ExcelProjectCodeData();
                            projectCodeData.Code = sheet.GetValue<string>(rowIndex, 1);
                            if (string.IsNullOrWhiteSpace(projectCodeData.Code))
                                break;
                            projectCodeData.Detail = sheet.GetValue<string?>(rowIndex, 2);
                            projectCodeData.Budjet = sheet.GetValue<decimal?>(rowIndex, 3);
                            projectCodeData.Cost = sheet.GetValue<decimal?>(rowIndex, 4);
                            projectCodeData.ProjectId = sheet.GetValue<long>(rowIndex, 5);
                            projectCode.Add(projectCodeData);
                            rowIndex++;
                        }
                        response.ProjectCodeDatas = projectCode;
                    }
                    #endregion
                    #region Contractor
                    if (schema.SheetName == "Contractor")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelContractorData> contractor = new List<ExcelContractorData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelContractorData contractorData = new ExcelContractorData();
                            contractorData.Id = sheet.GetValue<long>(rowIndex, 1);
                            contractorData.Name = sheet.GetValue<string>(rowIndex, 2);
                            if (string.IsNullOrWhiteSpace(contractorData.Name))
                                break;
                            contractorData.Address = sheet.GetValue<string?>(rowIndex, 3);
                            contractorData.PhoneNo = sheet.GetValue<string?>(rowIndex, 4);
                            contractorData.Email = sheet.GetValue<string?>(rowIndex, 5);
                            contractorData.ProvinceId = sheet.GetValue<long?>(rowIndex, 6);
                            contractorData.TaxNo = sheet.GetValue<string?>(rowIndex, 7);
                            contractorData.DirectorName = sheet.GetValue<string?>(rowIndex, 8);
                            contractorData.Type = sheet.GetValue<string?>(rowIndex, 9);
                            contractor.Add(contractorData);
                            rowIndex++;
                        }
                        response.ContractorDatas = contractor;
                    }
                    #endregion
                    #region ProjectContractor
                    if (schema.SheetName == "ProjectContractor")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelProjectContractorData> projectContractor = new List<ExcelProjectContractorData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelProjectContractorData projectContractorData = new ExcelProjectContractorData();
                            projectContractorData.ProjectId = sheet.GetValue<long>(rowIndex, 1);
                            if (projectContractorData.ProjectId == 0)
                                break;
                            projectContractorData.ContractorId = sheet.GetValue<long>(rowIndex, 2);
                            projectContractor.Add(projectContractorData);
                            rowIndex++;
                        }
                        response.ProjectContractorDatas = projectContractor;
                    }
                    #endregion
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("FileServices => ReadExcel(Customer): request=" + JsonConvert.SerializeObject(request) + " error:" + ex.Message);
                throw;
            }
        }
        public async Task<SettingImportStoreDataModel?> ReadExcel(SettingImportStoreRequest request, List<ExcelDataSchema> schemas)
        {
            try
            {
                if (request.StoreFile == null)
                    return null;
                var ms = new MemoryStream();
                await request.StoreFile.CopyToAsync(ms);
                var excel = new ExcelPackage(ms);
                var response = new SettingImportStoreDataModel();
                foreach (ExcelDataSchema schema in schemas)
                {
                    #region Store
                    if (schema.SheetName == "Store")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelStoreData> store = new List<ExcelStoreData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelStoreData storeData = new ExcelStoreData();
                            storeData.Id = sheet.GetValue<long>(rowIndex, 1);
                            storeData.Name = sheet.GetValue<string>(rowIndex, 2);
                            if (string.IsNullOrWhiteSpace(storeData.Name))
                                break;
                            storeData.Address = sheet.GetValue<string?>(rowIndex, 3);
                            storeData.ProvinceId = sheet.GetValue<long?>(rowIndex, 4);
                            storeData.PhoneNo = sheet.GetValue<string?>(rowIndex, 5);
                            storeData.Email = sheet.GetValue<string?>(rowIndex, 6);
                            storeData.TaxNo = sheet.GetValue<string?>(rowIndex, 7);
                            storeData.ContractName = sheet.GetValue<string?>(rowIndex, 8);
                            store.Add(storeData);
                            rowIndex++;
                        }
                        response.StoreDatas = store;
                    }
                    #endregion
                    #region Payment
                    if (schema.SheetName == "Payment")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelPaymentData> payment = new List<ExcelPaymentData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelPaymentData paymentData = new ExcelPaymentData();
                            paymentData.StoreId = sheet.GetValue<long>(rowIndex, 1);
                            if (paymentData.StoreId == 0)
                                break;
                            paymentData.AccountNo = sheet.GetValue<string>(rowIndex, 2);
                            paymentData.AccountName = sheet.GetValue<string?>(rowIndex, 3);
                            paymentData.Bank = sheet.GetValue<string?>(rowIndex, 4);
                            paymentData.Type = sheet.GetValue<string?>(rowIndex, 5);
                            payment.Add(paymentData);
                            rowIndex++;
                        }
                        response.PaymentDatas = payment;
                    }
                    #endregion
                    #region Product
                    if (schema.SheetName == "Product")
                    {
                        var sheet = excel.Workbook.Worksheets[schema.SheetName];
                        int rowIndex = 2;
                        List<ExcelProductData> product = new List<ExcelProductData>();
                        while (sheet.Rows[rowIndex] != null)
                        {
                            ExcelProductData productData = new ExcelProductData();
                            productData.Name = sheet.GetValue<string>(rowIndex, 1);
                            if (string.IsNullOrWhiteSpace(productData.Name))
                                break;
                            productData.Unit = sheet.GetValue<string>(rowIndex, 2);
                            product.Add(productData);
                            rowIndex++;
                        }
                        response.ProductDatas = product;
                    }
                    #endregion
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("FileServices => ReadExcel(Store): request=" + JsonConvert.SerializeObject(request) + " error:" + ex.Message);
                throw;
            }
        }
    }
}
