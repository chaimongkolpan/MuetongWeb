using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class CustomerResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long ProvinceId { get; set; } = 1;
        public string ProvinceName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxNo { get; set; } = string.Empty;
        public string BranchNo { get; set; } = string.Empty;
        public bool CanDelete { get; set; } = true;
        public CustomerResponse() { }
        public CustomerResponse(Customer customer)
        {
            Id = customer.Id;
            Name = string.IsNullOrWhiteSpace(customer.Name) ? string.Empty : customer.Name;
            Detail = string.IsNullOrWhiteSpace(customer.Detail) ? string.Empty : customer.Detail;
            Address = string.IsNullOrWhiteSpace(customer.Address) ? string.Empty : customer.Address;
            ProvinceId = customer.ProvinceId.HasValue ? customer.ProvinceId.Value : 1;
            ProvinceName = customer.Province == null ? string.Empty : customer.Province.NameTh;
            PhoneNo = string.IsNullOrWhiteSpace(customer.PhoneNo) ? string.Empty : customer.PhoneNo;
            Email = string.IsNullOrWhiteSpace(customer.Email) ? string.Empty : customer.Email;
            TaxNo = string.IsNullOrWhiteSpace(customer.TaxNo) ? string.Empty : customer.TaxNo;
            BranchNo = string.IsNullOrWhiteSpace(customer.BranchNo) ? string.Empty : customer.BranchNo;
            if (customer.Projects != null && customer.Projects.Any())
                CanDelete = false;
        }
    }
}
