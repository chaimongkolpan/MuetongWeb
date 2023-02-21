using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class PrReceiveResponse
    {
        public int WaitingCount { get; set; } = 0;
        public List<ReceivePrResponse> All { get; set; } = new List<ReceivePrResponse>();
        public List<ReceivePrResponse> Waiting { get; set; } = new List<ReceivePrResponse>();
        public PrReceiveResponse() { }
        public PrReceiveResponse(IEnumerable<Pr> prs)
        {
            foreach (var pr in prs)
            {
                var tmp = new ReceivePrResponse(pr);
                if (pr.PrDetails.Any(detail => detail.Status == StatusConstants.PrWaitingTransfer))
                {
                    WaitingCount++;
                    Waiting.Add(tmp);
                }
                All.Add(tmp);
            }
        }
        public PrReceiveResponse(IEnumerable<Pr> prs, List<Models.Entities.File> files)
        {
            foreach (var pr in prs)
            {
                var ids = pr.PrDetails.Select(x => x.Id).ToList();
                var prfiles = files.Where(file => ids.Contains(file.EntityId)).ToList();
                var tmp = new ReceivePrResponse(pr, prfiles);
                if (pr.PrDetails.Any(detail => detail.Status == StatusConstants.PrWaitingTransfer))
                {
                    WaitingCount += pr.PrDetails.Count(detail => detail.Status == StatusConstants.PrWaitingTransfer);
                    Waiting.Add(tmp);
                }
                All.Add(tmp);
            }
        }
    }
    public class ReceivePrResponse 
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
        public List<ReceivePrDetailResponse> Details { get; set; } = new List<ReceivePrDetailResponse>();
        public int ReceiveCount { get; set; } = 0;
        public DateTime CreateDate { get; set; }
        public ReceivePrResponse() { }
        public ReceivePrResponse(Pr pr)
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
                    if (detail.Status == StatusConstants.PrDetailComplete || detail.Status == StatusConstants.PrDetailWaitingTransfer)
                        Details.Add(new ReceivePrDetailResponse(detail));
                }
                ReceiveCount = Details.Count(detail => detail.HasReceive);
            }
            IsReadCancel = pr.IsReadCancel.HasValue ? pr.IsReadCancel.Value : false;
            CreateDate = pr.CreateDate;
        }
        public ReceivePrResponse(Pr pr, List<Models.Entities.File> files)
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
                    if(detail.Status == StatusConstants.PrDetailComplete || detail.Status == StatusConstants.PrDetailWaitingTransfer)
                    {
                        var recfiles = files.Where(file => file.EntityId == detail.Id).ToList();
                        var tmp = new ReceivePrDetailResponse(detail);
                        if (recfiles.Any()) tmp.SetFiles(recfiles);
                        Details.Add(tmp);
                    }
                }
                ReceiveCount = Details.Count(detail => detail.HasReceive);
            }
            IsReadCancel = pr.IsReadCancel.HasValue ? pr.IsReadCancel.Value : false;
            CreateDate = pr.CreateDate;
        }
    }
    public class ReceivePrDetailResponse
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
        public List<ReceiveResponse> Receives { get; set; } = new List<ReceiveResponse>();
        public List<FileResponse> Files { get; set; } = new List<FileResponse>();
        public List<string> FilePreviews { get; set; } = new List<string>();
        public decimal TotalReceive { get; set; } = 0;
        public bool HasReceive { get; set; } = false;
        public ReceivePrDetailResponse() { }
        public ReceivePrDetailResponse(PrDetail detail)
        {
            Id = detail.Id;
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
            Remark = string.IsNullOrWhiteSpace(detail.Remark) ? string.Empty : detail.Remark;
            Status = string.IsNullOrWhiteSpace(detail.Status) ? string.Empty : detail.Status;
            if (detail.PrReceives != null && detail.PrReceives.Any())
            {
                HasReceive = true;
                foreach (var prReceive in detail.PrReceives)
                {
                    Receives.Add(new ReceiveResponse(prReceive));
                }
                TotalReceive = detail.PrReceives.Sum(receive => receive.Quantity);
            }
        }
        public void SetFiles(List<Models.Entities.File> files)
        {
            Files.AddRange(files.Select(file => new FileResponse(file)).ToList());
            FilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention, file.Path)).ToList());
        }
    }
    public class ReceiveResponse
    {
        public long Id { get; set; }
        public decimal Quantity { get; set; } = 0;
        public DateTime CreateDate { get; set; }
        public string ReceiverName { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public ReceiveResponse() { }
        public ReceiveResponse(PrReceive receive) 
        {
            Id = receive.Id;
            Quantity = receive.Quantity;
            CreateDate = receive.CreateDate;
            ReceiverName = string.Format("{0} {1}", receive.User.Firstname, receive.User.Lastname);
            Remark = string.IsNullOrWhiteSpace(receive.Remark) ? string.Empty : receive.Remark;
        }
    }
}
