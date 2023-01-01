using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class ContractorMappers
    {
        public static List<Contractor>? ImportCustomerMapper(List<ExcelContractorData> request, long userId)
        {
            var result = new List<Contractor>();
            foreach (ExcelContractorData contractor in request)
            {
                var temp = ExcelData(contractor, userId);
                result.Add(temp);
            }
            return result;
        }
        public static Contractor ExcelData(ExcelContractorData request, long userId)
        {
            var contractor = new Contractor();
            contractor.Name = request.Name;
            contractor.Address = request.Address;
            contractor.ProvinceId = request.ProvinceId;
            contractor.PhoneNo = request.PhoneNo;
            contractor.Email = request.Email;
            contractor.TaxNo = request.TaxNo;
            contractor.DirectorName = request.DirectorName;
            contractor.Type = request.Type;
            contractor.UserId = userId;
            contractor.CreateDate = DateTime.Now;
            return contractor;
        }
    }
}
