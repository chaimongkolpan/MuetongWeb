using System;
using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
	public class PoIndexSearchRequest
    {
        public long? ProjectId { get; set; }
        public long? RequesterId { get; set; }
        public string? PrNo { get; set; }
        public string? Status { get; set; }
        public UserInfoModel? User { get; set; }
    }
}

