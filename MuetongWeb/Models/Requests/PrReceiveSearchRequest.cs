using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class PrReceiveSearchRequest
	{
		public long? ProjectId { get; set; }
		public long? RequesterId { get; set; }
		public string? PrNo { get; set; }
		public UserInfoModel? User { get; set; }
	}
}
