using MuetongWeb.Constants;

namespace MuetongWeb.Models.Requests
{
    public class SettingImportCustomerDataModel
    {
        public List<ExcelCustomerData>? CustomerDatas { get; set; }
        public List<ExcelProjectData>? ProjectDatas { get; set; }
        public List<ExcelProjectCodeData>? ProjectCodeDatas { get; set; }
        public List<ExcelContractorData>? ContractorDatas { get; set; }
        public List<ExcelProjectContractorData>? ProjectContractorDatas { get; set; }
    }
}
