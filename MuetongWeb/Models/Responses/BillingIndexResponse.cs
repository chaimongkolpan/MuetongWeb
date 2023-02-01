using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class BillingIndexResponse
    {
        public int WaitingCount { get; set; } = 0;
        public int WaitingApproveCount { get; set; } = 0;
        public int WaitingReceiptCount { get; set; } = 0;
        public int PoCount { get; set; } = 0;
        public int UnreadCancelCount { get; set; } = 0;
        public List<BillingResponse> All { get; set; } = new List<BillingResponse>();
        public List<BillingResponse> Waiting { get; set; } = new List<BillingResponse>();
        public List<BillingResponse> WaitingApprove { get; set; } = new List<BillingResponse>();
        public List<BillingResponse> WaitingReceipt { get; set; } = new List<BillingResponse>();
        public List<PoResponse> Po { get; set; } = new List<PoResponse>();
        public List<BillingResponse> Cancel { get; set; } = new List<BillingResponse>();
        public BillingIndexResponse() { }
        public BillingIndexResponse(IEnumerable<Billing> bills, IEnumerable<Po> pos, IEnumerable<Po> poBills)
        {
            PoCount = pos.Count();
            foreach (var po in pos)
            {
                Po.Add(new PoResponse(po));
            }
            foreach (var bill in bills)
            {
                var status = bill.Status;
                var tmp = new BillingResponse(bill);
                var ids = bill.PoBillings.Select(po => po.PoId).ToList();
                var pbs = poBills.Where(po => ids.Contains(po.Id)).ToList();
                tmp.SetPo(pbs);
                if (status == StatusConstants.BillingCancel)
                {
                    if (!tmp.IsReadCancel)
                    {
                        UnreadCancelCount++;
                    }
                    Cancel.Add(tmp);
                }
                else if (status == StatusConstants.BillingWaitingApprove)
                {
                    WaitingApproveCount++;
                    WaitingApprove.Add(tmp);
                    All.Add(tmp);
                }
                else if (status == StatusConstants.BillingWaitingReceipt || status == StatusConstants.BillingWaitingPayment)
                {
                    WaitingReceiptCount++;
                    WaitingReceipt.Add(tmp);
                    All.Add(tmp);
                }
                else
                {
                    All.Add(tmp);
                }
            }
        }
        public BillingIndexResponse(IEnumerable<Billing> bills, IEnumerable<Po> pos, IEnumerable<Po> poBills, List<Models.Entities.File> files)
        {
            PoCount = pos.Count();
            foreach (var po in pos)
            {
                Po.Add(new PoResponse(po));
            }
            foreach (var bill in bills)
            {
                var status = bill.Status;
                var tmp = new BillingResponse(bill);
                var ids = bill.PoBillings.Select(po => po.PoId).ToList();
                var pbs = poBills.Where(po => ids.Contains(po.Id)).ToList();
                tmp.SetPo(pbs);
                if (files.Any())
                {
                    var billFiles = files.Where(file => file.EntityId == bill.Id).ToList();
                    if (billFiles.Any()) tmp.SetFiles(billFiles);
                }
                if (status == StatusConstants.BillingCancel)
                {
                    if (!tmp.IsReadCancel)
                    {
                        UnreadCancelCount++;
                    }
                    Cancel.Add(tmp);
                }
                else if (status == StatusConstants.BillingWaitingApprove)
                {
                    WaitingApproveCount++;
                    WaitingApprove.Add(tmp);
                    All.Add(tmp);
                }
                else if (status == StatusConstants.BillingWaitingReceipt || status == StatusConstants.BillingWaitingPayment)
                {
                    WaitingReceiptCount++;
                    WaitingReceipt.Add(tmp);
                    All.Add(tmp);
                }
                else
                {
                    All.Add(tmp);
                }
            }
        }
    }
    public class BillingResponse
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string BillingNo { get; set; } = string.Empty;
        public DateTime BillingDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public long PaymentType { get; set; }
        public string PaymentTypeName { get; set; } = string.Empty;
        public long? PaymentAccountId { get; set; }
        public string PaymentAccount { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string ReceiptNo { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? ApproverId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveRemark { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;
        public bool HasExtra { get; set; } = false;
        public decimal ExtraCost { get; set; } = 0;
        public bool IsReadCancel { get; set; } = false;
        public long? ExtraType { get; set; }
        public string ExtraOther { get; set; } = string.Empty;
        public bool HasReceipt { get; set; } = false;
        public bool HasInvoice { get; set; } = false;
        public bool IsPay { get; set; } = false;
        public string RequesterName { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
        public List<PoResponse> Details { get; set; } = new List<PoResponse>();
        public List<FileResponse> Files { get; set; } = new List<FileResponse>();
        public List<string> FilePreviews { get; set; } = new List<string>();
        public int Rowspan { get; set; } = 1;
        public BillingResponse() { }
        public BillingResponse(Billing bill)
        {
            Id = bill.Id;
            StoreId = bill.StoreId;
            BillingNo = string.IsNullOrWhiteSpace(bill.BillingNo) ? string.Empty : bill.BillingNo;
            BillingDate = bill.BillingDate;
            Status = string.IsNullOrWhiteSpace(bill.Status) ? string.Empty : bill.Status;
            PaymentType = bill.PaymentType;
            PaymentTypeName = bill.PaymentTypeNavigation == null ? string.Empty : bill.PaymentTypeNavigation.Name;
            PaymentAccountId = bill.PaymentAccountId;
            PaymentAccount = bill.PaymentAccount == null ? string.Empty : string.Format("{0} {1}", bill.PaymentAccount.Bank, bill.PaymentAccount.AccountNo);
            IsPay = bill.IsPay.HasValue ? bill.IsPay.Value : false;
            PaymentDate = bill.PaymentDate;
            Amount = bill.Amount;
            HasReceipt = bill.HasReceipt.HasValue ? bill.HasReceipt.Value : false;
            ReceiptNo = string.IsNullOrWhiteSpace(bill.ReceiptNo) ? string.Empty : bill.ReceiptNo;
            HasInvoice = bill.HasInvoice.HasValue ? bill.HasInvoice.Value : false;
            InvoiceNo = string.IsNullOrWhiteSpace(bill.InvoiceNo) ? string.Empty : bill.InvoiceNo;
            Remark = string.IsNullOrWhiteSpace(bill.Remark) ? string.Empty : bill.Remark;
            UserId = bill.UserId;
            RequesterName = string.Format("{0} {1}", bill.User.Firstname, bill.User.Lastname);
            CreateDate = bill.CreateDate;
            ModifyDate = bill.ModifyDate;
            ApproverId = bill.ApproverId;
            ApproverName = bill.Approver != null ? string.Format("{0} {1}", bill.Approver.Firstname, bill.Approver.Lastname) : string.Empty;
            ApproveDate = bill.ApproveDate;
            ApproveRemark = string.IsNullOrWhiteSpace(bill.ApproveRemark) ? string.Empty : bill.ApproveRemark;
            HasExtra = bill.HasExtra.HasValue ? bill.HasExtra.Value : false;
            ExtraCost = bill.ExtraCost.HasValue ? bill.ExtraCost.Value : 0;
            ExtraType = bill.ExtraType;
            ExtraOther = string.IsNullOrWhiteSpace(bill.ExtraOther) ? string.Empty : bill.ExtraOther;
            IsReadCancel = bill.IsReadCancel.HasValue ? bill.IsReadCancel.Value : false;
        }
        public void SetFiles(List<Models.Entities.File> files)
        {
            Files.AddRange(files.Select(file => new FileResponse(file)).ToList());
            FilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention, file.Path)).ToList());
        }
        public void SetPo(IEnumerable<Po> pos)
        {
            foreach (var po in pos)
            {
                Details.Add(new PoResponse(po));
            }
            int sumrow = Details.Sum(po => po.Details.Count);
            Rowspan = sumrow == 0 ? 1 : sumrow;
        }
    }
}
