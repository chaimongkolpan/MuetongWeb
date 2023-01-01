using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class ProductMappers
    {
        public static List<Product>? ImportCustomerMapper(List<ExcelProductData> request, long userId)
        {
            var result = new List<Product>();
            foreach (ExcelProductData product in request)
            {
                var temp = ExcelData(product, userId);
                result.Add(temp);
            }
            return result;
        }
        public static Product ExcelData(ExcelProductData request, long userId)
        {
            var product = new Product();
            product.Name = request.Name;
            product.Unit = request.Unit;
            product.UserId = userId;
            product.CreateDate = DateTime.Now;
            return product;
        }
    }
}
