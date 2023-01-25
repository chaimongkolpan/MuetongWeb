using System;
namespace MuetongWeb.Models.Requests
{
    public class PrIndexUpdateRequest
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string? PrNo { get; set; }
        public bool AdvancePay { get; set; } = false;
        public long ContractorId { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? JsonDetails { get; set; }
        public List<PrDetailUpdateRequest>? Details { get; set; }
    }
    public class PrDetailUpdateRequest
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public long ProjectCodeId { get; set; }
        public DateTime? UseDate { get; set; }
        public string? Remark { get; set; }
    }
}

