using System;
using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class PoIndexResponse
    {
        public int WaitingCount { get; set; } = 0;
        public int PrCount { get; set; } = 0;
        public int UnreadCancelCount { get; set; } = 0;
        public List<PoResponse> All { get; set; } = new List<PoResponse>();
        public List<PoResponse> Waiting { get; set; } = new List<PoResponse>();
        public List<PrResponse> Pr { get; set; } = new List<PrResponse>();
        public List<PoResponse> Cancel { get; set; } = new List<PoResponse>();
        public PoIndexResponse() { }
        public PoIndexResponse(IEnumerable<Po> pos, IEnumerable<Pr> prs)
        {
            PrCount = prs.Count();
            foreach (var pr in prs)
            {
                Pr.Add(new PrResponse(pr));
            }
            foreach (var po in pos)
            {
                //var status = po.Status;
                //var tmp = new PoResponse(po);
                //if (status == StatusConstants.PoCancel)
                //{
                //    if (!tmp.IsReadCancel) UnreadCancelCount++;
                //    Cancel.Add(tmp);
                //}
                //else if (status == StatusConstants.PoWaitingApprove)
                //{
                //    WaitingCount++;
                //    Waiting.Add(tmp);
                //}
                //else
                //{
                //    All.Add(tmp);
                //}
            }
        }
    }
    public class PoResponse
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
        public bool IsReadCancel { get; set; } = false;
        public List<PrDetailResponse> Details { get; set; } = new List<PrDetailResponse>();
        public PoResponse() { }
        public PoResponse(Pr pr)
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
                foreach (var detail in pr.PrDetails)
                {
                    Details.Add(new PrDetailResponse(detail));
                }
            }
            IsReadCancel = pr.IsReadCancel.HasValue ? pr.IsReadCancel.Value : false;
        }
    }
    public class PoDetailResponse
    {
        public long? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public DateTime? UseDate { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public long? ProjectCodeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Remark { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public PoDetailResponse() { }
        public PoDetailResponse(PrDetail detail)
        {
            ProductId = detail.ProductId;
            if (detail.Product != null)
            {
                Name = detail.Product.Name;
                Unit = detail.Product.Unit;
            }
            Quantity = detail.Quantity;
            UseDate = detail.UseDate;
            if (detail.PoDetail != null && detail.PoDetail.Po != null)
                PlanTransferDate = detail.PoDetail.Po.PlanTransferDate;
            ProjectCodeId = detail.ProjectCodeId;
            if (detail.ProjectCode != null)
                Code = detail.ProjectCode.Code;
            Remark = detail.Remark;
            Status = detail.Status;
        }
    }
}

