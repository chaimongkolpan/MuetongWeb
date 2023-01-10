using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ContractorResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? Type { get; set; }
        public string? DirectorName { get; set; }
        public ContractorResponse() { }
        public ContractorResponse(Contractor contractor)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            Address = contractor.Address;
            ProvinceId = contractor.ProvinceId;
            PhoneNo = contractor.PhoneNo;
            Email = contractor.Email;
            TaxNo = contractor.TaxNo;
            Type = contractor.Type;
            DirectorName = contractor.DirectorName;
        }
    }
}

