using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class PoIndexPrResponse
    {
        public List<PoIndexPrDetailResponse> Details { get; set; } = new List<PoIndexPrDetailResponse>();
        public PoIndexPrResponse() { }
        public PoIndexPrResponse(IEnumerable<PrDetail> details)
        {
            foreach (var detail in details)
            {
                Details.Add(new PoIndexPrDetailResponse(detail));
            }
        }
    }
    public class PoIndexPrDetailResponse
	{
		public long Id { get; set; }
		public string? ProjectName { get; set; }
		public string? PrNo { get; set; }
		public string? RequesterName { get; set; }
		public string? ProductName { get; set; }
		public decimal? Quantity { get; set; }
        public string? Unit { get; set; }
        public DateTime? UseDate { get; set; }
        public string? ProjectCode { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public List<IFormFile>? PrFiles { get; set; }
        public List<IFormFile>? PrApproveFiles { get; set; }
        public PoIndexPrDetailResponse() { }
        public PoIndexPrDetailResponse(PrDetail detail)
        {
            Id = detail.Id;
            ProjectName = detail.Pr.Project.Name ?? string.Empty;
            PrNo = detail.Pr.PrNo;
            var user = detail.Pr.User;
            RequesterName = string.Format("{0} {1}", user.Firstname ?? string.Empty, user.Lastname ?? string.Empty);
            ProductName = detail.Product?.Name ?? string.Empty;
            Quantity = detail.Quantity;
            Unit = detail.Product?.Unit ?? string.Empty;
            UseDate = detail.UseDate;
            ProjectCode = detail.ProjectCode?.Code ?? string.Empty;
            Status = detail.Status ?? string.Empty;
            Remark = detail.Remark;
        }
    }
}

