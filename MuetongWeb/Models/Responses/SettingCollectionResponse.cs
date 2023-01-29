using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class SettingCollectionResponse
    {
        public List<SettingConstantResponse> BillingReceive { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> ReceiptReceive { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> CreditType { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> PaymentType { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> ExtraType { get; set; } = new List<SettingConstantResponse>();
        public SettingCollectionResponse() { }
        public SettingCollectionResponse(IEnumerable<SettingConstant> settings)
        {
            foreach (var setting in settings)
            {
                if(setting.Type == SettingConstants.ReceiveBillingType)
                    BillingReceive.Add(new SettingConstantResponse(setting));
                if (setting.Type == SettingConstants.ReceiveReceiptType)
                    ReceiptReceive.Add(new SettingConstantResponse(setting));
                if (setting.Type == SettingConstants.CreditType)
                    CreditType.Add(new SettingConstantResponse(setting));
                if (setting.Type == SettingConstants.PaymentType)
                    PaymentType.Add(new SettingConstantResponse(setting));
                if (setting.Type == SettingConstants.ExtraType)
                    ExtraType.Add(new SettingConstantResponse(setting));
            }
        }
    }
}
