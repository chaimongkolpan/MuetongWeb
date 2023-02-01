using System;
using MuetongWeb.Helpers;
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
        public PoIndexPrResponse(IEnumerable<PrDetail> details, List<Models.Entities.File> files, List<Models.Entities.File> approveFiles)
        {
            foreach (var detail in details)
            {
                var tmp = new PoIndexPrDetailResponse(detail);
                var prFile = files.Where(file => file.EntityId == detail.Pr.Id).ToList();
                if (prFile.Any()) tmp.SetFiles(prFile);
                var approvePrFile = approveFiles.Where(file => file.EntityId == detail.Pr.Id).ToList();
                if (approvePrFile.Any()) tmp.SetApproveFiles(approvePrFile);
                Details.Add(tmp);
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
        public DateTime? CreateDate { get; set; }
        public List<FileResponse> Files { get; set; } = new List<FileResponse>();
        public List<string> FilePreviews { get; set; } = new List<string>();
        public List<FileResponse> ApproveFiles { get; set; } = new List<FileResponse>();
        public List<string> ApproveFilePreviews { get; set; } = new List<string>();
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
            CreateDate = detail.Pr.CreateDate;
        }
        public void SetFiles(List<Models.Entities.File> files)
        {
            Files.AddRange(files.Select(file => new FileResponse(file)).ToList());
            FilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention, file.Path)).ToList());
        }
        public void SetApproveFiles(List<Models.Entities.File> files)
        {
            ApproveFiles.AddRange(files.Select(file => new FileResponse(file)).ToList());
            ApproveFilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention, file.Path)).ToList());
        }
    }
}

