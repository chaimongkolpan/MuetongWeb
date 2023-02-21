using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class PrReceiveAddRequest
    {
        public List<PrReceiveDetailRequest>? Details { get; set; }
        public List<IFormFile>? Files { get; set; }
        public UserInfoModel? User { get; set; }
    }
    public class PrReceiveDetailRequest
    {
        public long DetailId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Remark { get; set; }
        public List<IFormFile>? Files { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
