using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class SubDepartmentResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public SubDepartmentResponse() { }
        public SubDepartmentResponse(SubDepartment subDepartment) 
        { 
            Id = subDepartment.Id;
            Name = subDepartment.Name;
        }
    }
}
