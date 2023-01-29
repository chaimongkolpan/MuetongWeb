using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ProjectResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long? ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public long CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public bool CanDelete { get; set; } = true;
        public List<ProjectCodeResponse> Codes { get; set; } = new List<ProjectCodeResponse>();
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
        public List<ContractorResponse> Contractors { get; set; } = new List<ContractorResponse>();
        public ProjectResponse() { }
        public ProjectResponse(Project project)
		{
            Id = project.Id;
            Name = string.IsNullOrWhiteSpace(project.Name) ? string.Empty : project.Name;
            ContractNo = string.IsNullOrWhiteSpace(project.ContractNo) ? string.Empty : project.ContractNo;
            Address = string.IsNullOrWhiteSpace(project.Address) ? string.Empty : project.Address;
            ProvinceId = project.ProvinceId;
            if (project.Province != null)
                ProvinceName = project.Province.NameTh;
            CustomerId = project.CustomerId;
            if (project.Customer != null)
                CustomerName = project.Customer.Name;
            if (project.ProjectCodes != null)
            {
                foreach(var code in project.ProjectCodes)
                {
                    Codes.Add(new ProjectCodeResponse(code));
                }
            }
            if (project.ProjectUsers != null)
            {
                foreach (var pUser in project.ProjectUsers)
                {
                    if (pUser.User != null)
                        Users.Add(new UserResponse(pUser.User));
                }
            }
            if (project.ProjectContractors != null)
            {
                foreach (var contractor in project.ProjectContractors)
                {
                    if (contractor.Contractor != null)
                        Contractors.Add(new ContractorResponse(contractor.Contractor));
                }
            }
            // Check can delete
        }
	}
}

