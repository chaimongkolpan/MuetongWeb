using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class TypeSettingConstantResponse
    {
        public List<SettingConstantResponse> CreditType { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> PaymentType { get; set; } = new List<SettingConstantResponse>();
        public TypeSettingConstantResponse() { }
        public TypeSettingConstantResponse(IEnumerable<SettingConstant> credits, IEnumerable<SettingConstant> payments)
        {
            foreach (var credit in credits)
            {
                CreditType.Add(new SettingConstantResponse(credit));
            }
            foreach (var payment in payments)
            {
                PaymentType.Add(new SettingConstantResponse(payment));
            }
        }
    }
}
