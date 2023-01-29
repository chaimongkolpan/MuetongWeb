using System;
using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
	public class PoIndexPrSearch
    {
        public long? ProjectId { get; set; }
        public string? PrNo { get; set; }
        public long? ProductId { get; set; }
        public UserInfoModel? User { get; set; }
    }
}

