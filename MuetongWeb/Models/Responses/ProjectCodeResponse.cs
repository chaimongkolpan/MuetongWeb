using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ProjectCodeResponse
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string Code { get; set; } = null!;
        public string? Detail { get; set; }
        public decimal? Budjet { get; set; }
        public decimal? Cost { get; set; }
        public long? ParentId { get; set; }
        public ProjectCodeResponse() { }
        public ProjectCodeResponse(ProjectCode projectCode)
        {
            Id = projectCode.Id;
            ProjectId = projectCode.ProjectId;
            Code = projectCode.Code;
            Detail = projectCode.Detail;
            Budjet = projectCode.Budjet;
            Cost = projectCode.Cost;
            ParentId = projectCode.ParentId;
        }
    }
}

