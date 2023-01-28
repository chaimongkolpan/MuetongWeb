using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class DepartmentResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DepartmentResponse() { }
        public DepartmentResponse(Department department)
        {
            Id = department.Id;
            Name = department.Name;
        }
    }
}
