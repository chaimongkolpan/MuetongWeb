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
                var status = po.Status;
                var tmp = new PoResponse(po);
                if (status == StatusConstants.PoCancel)
                {
                    if (!tmp.IsReadCancel)
                    {
                        UnreadCancelCount++;
                    }
                    //else
                    //    All.Add(tmp);
                    Cancel.Add(tmp);
                }
                else if (status == StatusConstants.PoWaitingApprove)
                {
                    WaitingCount++;
                    Waiting.Add(tmp);
                }
                else
                {
                    All.Add(tmp);
                }
            }
        }
    }
    public class PoResponse
    {
        public long Id { get; set; }
        public string PoNo { get; set; } = null!;
        public string? StoreName { get; set; }
        public string? StoreAddress { get; set; }
        public string? StorePhoneNo { get; set; }
        public string? StoreTaxNo { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public string CreditType { get; set; }
        public int? DateValue { get; set; }
        public string BeforeDay { get { return IsPayBefore(CreditType) ? DateValue.HasValue ?  DateValue.Value.ToString() : string.Empty : string.Empty; } }
        public string AfterDay { get { return !IsPayBefore(CreditType) ? DateValue.HasValue ? DateValue.Value.ToString() : string.Empty : string.Empty; } }
        public DateTime? DateSpecific { get; set; }
        public string? BgContractNo { get; set; }
        public string? ChequeNo { get; set; }
        public string ContractNo { get { return string.IsNullOrWhiteSpace(BgContractNo) ? string.IsNullOrWhiteSpace(ChequeNo) ? string.Empty : ChequeNo : BgContractNo; } }
        public string? PaymentType { get; set; }
        public string? PaymentAccount { get; set; }
        public string? PaymentAccountType { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? BillingReceiveType { get; set; }
        public string? ReceiptReceiveType { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
        public bool IsReadCancel { get; set; } = false;
        public List<PoDetailResponse> Details { get; set; } = new List<PoDetailResponse>();
        public PoResponse() { }
        public PoResponse(Po po)
        {
            Id = po.Id;
            PoNo = string.IsNullOrWhiteSpace(po.PoNo) ? string.Empty : po.PoNo;
            StoreName = po.Store.Name;
            StoreAddress = po.Store.Address;
            StorePhoneNo = po.Store.PhoneNo;
            StoreTaxNo = po.Store.TaxNo;
            PlanTransferDate = po.PlanTransferDate;
            CreditType = po.CreditTypeNavigation.Name;
            // value
            PaymentType = po.PaymentTypeNavigation.Name;
            BillingReceiveType = po.BillingReceiveTypeNavigation?.Name ?? string.Empty;
            ReceiptReceiveType = po.ReceiptReceiveTypeNavigation?.Name ?? string.Empty;
            Status = po.Status;
            RequesterName = String.Format("{0} {1}", po.User.Firstname, po.User.Lastname);
            ApproverName = po.Approver != null ? string.Format("{0} {1}", po.Approver.Firstname, po.Approver.Lastname) : string.Empty;
            IsReadCancel = po.IsReadCancel.HasValue ? po.IsReadCancel.Value : false;
            if (po.PoDetails.Any())
            {
                foreach(var item in po.PoDetails)
                {
                    Details.Add(new PoDetailResponse(item));
                }
            }
            Remark = string.IsNullOrWhiteSpace(po.Remark) ? string.Empty : po.Remark;
        }
        private bool IsPayBefore(string creditType)
        {
            return false;
        }
    }
    public class PoDetailResponse
    {
        public string ProjectName { get; set; } = string.Empty;
        public string PrNo { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public bool IsAdvancePay { get; set; } = false;
        public string ContractorName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Wht { get; set; }
        public decimal? Total { get; set; }
        public DateTime? UseDate { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public PoDetailResponse() { }
        public PoDetailResponse(PoDetail detail)
        {
            if (detail.PrDetails.Any())
            {
                var prDetail = detail.PrDetails.FirstOrDefault();
                if(prDetail != null)
                {
                    var pr = prDetail.Pr;
                    var project = pr.Project;
                    ProjectName = project.Name;
                    PrNo = pr.PrNo;
                    CreateDate = pr.CreateDate;
                    IsAdvancePay = pr.IsAdvancePay.HasValue ? pr.IsAdvancePay.Value : false;
                    if (IsAdvancePay)
                    {
                        var contractor = pr.Contractor;
                        if(contractor != null)
                            ContractorName = contractor.Name;
                    }
                    var user = pr.User;
                    RequesterName = String.Format("{0} {1}", user.Firstname, user.Lastname);
                    var projectCode = prDetail.ProjectCode;
                    if (projectCode != null)
                        Code = projectCode.Code;
                    Status = prDetail.Status;
                    Quantity = prDetail.Quantity;
                    UseDate = prDetail.UseDate;
                    Remark = prDetail.Remark;
                }
            }
            PricePerUnit = detail.PricePerUnit;
            Discount = detail.Discount;
            Vat = detail.Vat;
            Wht = detail.Wht;
            Total = detail.Total;
            if (detail.Product != null)
            {
                Name = detail.Product.Name;
                Unit = detail.Product.Unit;
            }
        }
    }
}

