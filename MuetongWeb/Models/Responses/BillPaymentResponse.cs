using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class BillPaymentResponse
    {
        public List<SettingConstantResponse> ExtraType { get; set; } = new List<SettingConstantResponse>();
        public List<PaymentAccountResponse> Payments { get; set; } = new List<PaymentAccountResponse>();
        public List<SettingConstantResponse> PaymentType { get; set; } = new List<SettingConstantResponse>();
        public BillPaymentResponse() { }
        public BillPaymentResponse(IEnumerable<SettingConstant> extras, List<PaymentAccountResponse> payments, IEnumerable<SettingConstant> payTypes)
        {
            Payments = payments;
            foreach (var extra in extras)
            {
                ExtraType.Add(new SettingConstantResponse(extra));
            }
            foreach (var payment in payTypes)
            {
                PaymentType.Add(new SettingConstantResponse(payment));
            }
        }
    }
}
