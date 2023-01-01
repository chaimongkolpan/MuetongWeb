using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class PaymentAccountMappers
    {
        public static List<PaymentAccount>? ImportCustomerMapper(List<ExcelPaymentData> request)
        {
            var result = new List<PaymentAccount>();
            foreach (ExcelPaymentData payment in request)
            {
                var temp = ExcelData(payment);
                result.Add(temp);
            }
            return result;
        }
        public static PaymentAccount ExcelData(ExcelPaymentData request)
        {
            var payment = new PaymentAccount();
            payment.StoreId = request.StoreId;
            payment.AccountNo = request.AccountNo;
            payment.AccountName = request.AccountName;
            payment.Bank = request.Bank;
            payment.Type = request.Type;
            return payment;
        }
    }
}
