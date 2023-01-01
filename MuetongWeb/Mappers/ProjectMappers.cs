using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class ProjectMappers
    {
        public static List<Project>? ImportCustomerMapper(List<ExcelProjectData> request, long userId)
        {
            var result = new List<Project>();
            foreach (ExcelProjectData project in request)
            {
                var temp = ExcelData(project, userId);
                result.Add(temp);
            }
            return result;
        }
        public static Project ExcelData(ExcelProjectData request, long userId)
        {
            var project = new Project();
            project.Name = request.Name;
            project.ContractNo = request.ContractNo;
            project.Address = request.Address;
            project.ProvinceId = request.ProvinceId;
            project.CustomerId = request.CustomerId;
            project.UserId = userId;
            project.CreateDate = DateTime.Now;
            return project;
        }
    }
}
