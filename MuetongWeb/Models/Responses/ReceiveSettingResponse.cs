using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class ReceiveSettingConstantResponse
    {
        public List<SettingConstantResponse> Billing { get; set; } = new List<SettingConstantResponse>();
        public List<SettingConstantResponse> Receipt { get; set; } = new List<SettingConstantResponse>();
        public ReceiveSettingConstantResponse() { }
        public ReceiveSettingConstantResponse(IEnumerable<SettingConstant> billing, IEnumerable<SettingConstant> receipt)
        {
            foreach (var bill in billing)
            {
                Billing.Add(new SettingConstantResponse(bill));
            }
            foreach (var rece in receipt)
            {
                Receipt.Add(new SettingConstantResponse(rece));
            }
        }
    }
    public class SettingConstantResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SettingConstantResponse() { }
        public SettingConstantResponse(SettingConstant setting) 
        { 
            Id = setting.Id;
            Name = setting.Name;
        }
    }
}
