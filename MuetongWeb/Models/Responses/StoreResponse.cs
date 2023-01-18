using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class StoreResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? TaxNo { get; set; }
        public string? ContractName { get; set; }
        public string? Email { get; set; }
        public List<PaymentAccountResponse> Payments { get; set; } = new List<PaymentAccountResponse>();
        public StoreResponse() { }
        public StoreResponse(Store store) 
        { 
            Id = store.Id;
            Name = store.Name;
            Address = store.Address;
            ProvinceId = store.ProvinceId;
            PhoneNo = store.PhoneNo;
            TaxNo = store.TaxNo;
            ContractName = store.ContractName;
            Email = store.Email;
            foreach(var account in store.PaymentAccounts)
            {
                Payments.Add(new PaymentAccountResponse(account));
            }
        }
    }
    public class PaymentAccountResponse
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string AccountNo { get; set; } = null!;
        public string? AccountName { get; set; }
        public string? Bank { get; set; }
        public string? Type { get; set; }
        public PaymentAccountResponse() { }
        public PaymentAccountResponse(PaymentAccount account)
        {
            Id = account.Id;
            StoreId = account.StoreId;
            AccountNo = account.AccountNo;
            AccountName = account.AccountName;
            Bank = account.Bank;
            Type = account.Type;
        }
    }
}
