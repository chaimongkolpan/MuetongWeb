using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class ProjectContractorMappers
    {
        public static List<ProjectContractor>? ImportCustomerMapper(List<ExcelProjectContractorData> request)
        {
            var result = new List<ProjectContractor>();
            foreach (ExcelProjectContractorData project in request)
            {
                var temp = ExcelData(project);
                result.Add(temp);
            }
            return result;
        }
        public static ProjectContractor ExcelData(ExcelProjectContractorData request)
        {
            var project = new ProjectContractor();
            project.ProjectId = request.ProjectId;
            project.ContractorId = request.ContractorId;
            return project;
        }
    }
}
