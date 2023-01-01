using MuetongWeb.Constants;

namespace MuetongWeb.Models.Requests
{
    public class SettingImportStoreDataModel
    {
        public List<ExcelStoreData>? StoreDatas { get; set; }
        public List<ExcelPaymentData>? PaymentDatas { get; set; }
        public List<ExcelProductData>? ProductDatas { get; set; }
    }
}
