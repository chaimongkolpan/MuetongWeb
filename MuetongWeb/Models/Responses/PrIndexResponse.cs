using System;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class PrIndexResponse
    {
        public int WaitingCount { get; set; } = 0;
        public int UnreadCancelCount { get; set; } = 0;
        public List<PrResponse> All { get; set; } = new List<PrResponse>();
        public List<PrResponse> Waiting { get; set; } = new List<PrResponse>();
        public List<PrResponse> Cancel { get; set; } = new List<PrResponse>();
        public PrIndexResponse() { }
        public PrIndexResponse(IEnumerable<Pr> prs)
        {
            foreach(var pr in prs)
            {
                var status = pr.Status;
                var tmp = new PrResponse(pr);
                if (status == StatusConstants.PrCancel)
                {
                    if (!tmp.IsReadCancel)
                    {
                        UnreadCancelCount++;
                    }
                    else
                        All.Add(tmp);
                    Cancel.Add(tmp);
                }
                else if (status == StatusConstants.PrWaitingApprove)
                {
                    WaitingCount++;
                    Waiting.Add(tmp);
                    All.Add(tmp);
                }
                else
                    All.Add(tmp);
                
            }
        }
    }
	public class PrResponse
    {
        public long Id { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string PrNo { get; set; } = string.Empty;
        public bool IsAdvancePay { get; set; } = false;
        public long? ContractId { get; set; }
        public string ContractorName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public bool IsReadCancel { get; set; } = false;
        public List<PrDetailResponse> Details { get; set; } = new List<PrDetailResponse>();
        public List<FileResponse> Files { get; set; } = new List<FileResponse>();
        public List<string> FilePreviews { get; set; } = new List<string>();
        public PrResponse() { }
        public PrResponse(Pr pr)
        {
            Id = pr.Id;
            ProjectId = pr.ProjectId;
            if (pr.Project != null)
                ProjectName = pr.Project.Name;
            PrNo = pr.PrNo;
            if (pr.IsAdvancePay.HasValue)
            {
                IsAdvancePay = pr.IsAdvancePay.Value;
                ContractId = pr.ContractorId;
                if (pr.Contractor != null)
                    ContractorName = pr.Contractor.Name;
            }
            RequesterName = string.Format("{0} {1}", pr.User.Firstname, pr.User.Lastname);
            if (pr.Approver != null)
                ApproverName = string.Format("{0} {1}", pr.Approver.Firstname, pr.Approver.Lastname);
            if (pr.PrDetails.Any())
            {
                foreach(var detail in pr.PrDetails)
                {
                    Details.Add(new PrDetailResponse(detail));
                }
            }
            CreateDate = pr.CreateDate;
            IsReadCancel = pr.IsReadCancel.HasValue ? pr.IsReadCancel.Value : false;
            Status = pr.Status;
        }
        public void SetFiles(List<Models.Entities.File> files)
        {
            Files.AddRange(files.Select(file => new FileResponse(file)).ToList());
            FilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention,file.Path)).ToList());
        }
    }
    public class PrDetailResponse
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public DateTime? UseDate { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public long? ProjectCodeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public PrDetailResponse() { }
        public PrDetailResponse(PrDetail detail)
        {
            Id = detail.Id;
            ProductId = detail.ProductId;
            if(detail.Product != null)
            {
                Name = detail.Product.Name;
                Unit = detail.Product.Unit;
            }
            Quantity = detail.Quantity;
            UseDate = detail.UseDate;
            if(detail.PoDetail != null && detail.PoDetail.Po != null)
                PlanTransferDate = detail.PoDetail.Po.PlanTransferDate;
            ProjectCodeId = detail.ProjectCodeId;
            if(detail.ProjectCode != null)
                Code = detail.ProjectCode.Code;
            Remark = string.IsNullOrWhiteSpace(detail.Remark) ? string.Empty : detail.Remark;
            Status = string.IsNullOrWhiteSpace(detail.Status) ? string.Empty : detail.Status;
        }
    }
}

