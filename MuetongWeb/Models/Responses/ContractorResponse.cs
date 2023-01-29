using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ContractorResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long? ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxNo { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string DirectorName { get; set; } = string.Empty;
        public bool CanDelete { get; set; } = true;
        public ContractorResponse() { }
        public ContractorResponse(Contractor contractor)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            Address = string.IsNullOrWhiteSpace(contractor.Address) ? string.Empty : contractor.Address;
            ProvinceId = contractor.ProvinceId;
            if (contractor.Province != null)
                ProvinceName = contractor.Province.NameTh;
            PhoneNo = string.IsNullOrWhiteSpace(contractor.PhoneNo) ? string.Empty : contractor.PhoneNo;
            Email = string.IsNullOrWhiteSpace(contractor.Email) ? string.Empty : contractor.Email;
            TaxNo = string.IsNullOrWhiteSpace(contractor.TaxNo) ? string.Empty : contractor.TaxNo;
            Type = string.IsNullOrWhiteSpace(contractor.Type) ? string.Empty : contractor.Type;
            DirectorName = string.IsNullOrWhiteSpace(contractor.DirectorName) ? string.Empty : contractor.DirectorName;
            if (contractor.ProjectContractors != null && contractor.ProjectContractors.Any())
                CanDelete = false;
        }
    }
}

