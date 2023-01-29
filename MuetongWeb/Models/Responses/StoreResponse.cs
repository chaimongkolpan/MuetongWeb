using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class StoreResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long? ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string TaxNo { get; set; } = string.Empty;
        public string ContractName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<PaymentAccountResponse> Payments { get; set; } = new List<PaymentAccountResponse>();
        public bool CanDelete { get; set; } = true;
        public StoreResponse() { }
        public StoreResponse(Store store) 
        { 
            Id = store.Id;
            Name = string.IsNullOrWhiteSpace(store.Name) ? string.Empty : store.Name;
            Address = string.IsNullOrWhiteSpace(store.Address) ? string.Empty : store.Address;
            ProvinceId = store.ProvinceId;
            if (store.Province != null)
                ProvinceName = store.Province.NameTh;
            PhoneNo = string.IsNullOrWhiteSpace(store.PhoneNo) ? string.Empty : store.PhoneNo;
            TaxNo = string.IsNullOrWhiteSpace(store.TaxNo) ? string.Empty : store.TaxNo;
            ContractName = string.IsNullOrWhiteSpace(store.ContractName) ? string.Empty : store.ContractName;
            Email = string.IsNullOrWhiteSpace(store.Email) ? string.Empty : store.Email;
            foreach (var account in store.PaymentAccounts)
            {
                Payments.Add(new PaymentAccountResponse(account));
            }
            if ((store.Pos != null && store.Pos.Any()) || (store.Billings != null && store.Billings.Any()))
                CanDelete = false;
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
        public bool CanDelete { get; set; } = true;
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
