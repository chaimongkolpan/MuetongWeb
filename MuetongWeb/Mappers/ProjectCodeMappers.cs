using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class ProjectCodeMappers
    {
        public static List<ProjectCode>? ImportCustomerMapper(List<ExcelProjectCodeData> request)
        {
            var result = new List<ProjectCode>();
            foreach (ExcelProjectCodeData projectCode in request)
            {
                var temp = ExcelData(projectCode);
                result.Add(temp);
            }
            return result;
        }
        public static ProjectCode ExcelData(ExcelProjectCodeData request)
        {
            var projectCode = new ProjectCode();
            projectCode.ProjectId = request.ProjectId;
            projectCode.Code = request.Code;
            projectCode.Detail = request.Detail;
            projectCode.Budjet = request.Budjet;
            projectCode.Cost = request.Cost;
            return projectCode;
        }
    }
}
