using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ProjectResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContractNo { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public long CustomerId { get; set; }
        public ProjectResponse(Project project)
		{
            Id = project.Id;
            Name = project.Name;
            ContractNo = project.ContractNo;
            Address = project.Address;
            ProvinceId = project.ProvinceId;
            CustomerId = project.CustomerId;
        }
	}
}

