﻿using System;
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
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? StoreAddress { get; set; }
        public string? StorePhoneNo { get; set; }
        public string? StoreTaxNo { get; set; }
        public DateTime? PlanTransferDate { get; set; }
        public long? CreditTypeId { get; set; }
        public string CreditType { get; set; }
        public int? DateValue { get; set; }
        public string BeforeDay { get { return IsPayBefore(CreditType) ? DateValue.HasValue ?  DateValue.Value.ToString() : string.Empty : string.Empty; } }
        public string AfterDay { get { return !IsPayBefore(CreditType) ? DateValue.HasValue ? DateValue.Value.ToString() : string.Empty : string.Empty; } }
        public DateTime? DateSpecific { get; set; }
        public string? BgContractNo { get; set; }
        public string? ChequeNo { get; set; }
        public string ContractNo { get { return string.IsNullOrWhiteSpace(BgContractNo) ? string.IsNullOrWhiteSpace(ChequeNo) ? string.Empty : ChequeNo : BgContractNo; } }
        public string? PaymentType { get; set; } = string.Empty;
        public long? PaymentTypeId { get; set; }
        public string? PaymentAccount { get; set; } = string.Empty;
        public long? PaymentAccountId { get; set; }
        public string? PaymentAccountType { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public decimal GrandTotal { get; set; } = 0;
        public long? BillingReceiveTypeId { get; set; }
        public string? BillingReceiveType { get; set; }
        public long? ReceiptReceiveTypeId { get; set; }
        public string? ReceiptReceiveType { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
        public bool IsReadCancel { get; set; } = false;
        public decimal WhtRate { get; set; } = 3;
        public DateTime? CreateDate { get; set; }
        public List<PoDetailResponse> Details { get; set; } = new List<PoDetailResponse>();
        public PoResponse() { }
        public PoResponse(Po po)
        {
            Id = po.Id;
            PoNo = string.IsNullOrWhiteSpace(po.PoNo) ? string.Empty : po.PoNo;
            StoreId = po.StoreId;
            StoreName = po.Store.Name;
            StoreAddress = po.Store.Address;
            StorePhoneNo = po.Store.PhoneNo;
            StoreTaxNo = po.Store.TaxNo;
            PlanTransferDate = po.PlanTransferDate;
            CreditTypeId = po.CreditType;
            CreditType = po.CreditTypeNavigation.Name;
            DateSpecific = po.DateSpecific;
            DateValue = po.DateValue;
            PaymentTypeId = po.PaymentType;
            PaymentType = po.PaymentTypeNavigation.Name;
            if(po.PaymentAccount != null)
            {
                PaymentAccount = String.Format("{0} {1}", po.PaymentAccount.Bank, po.PaymentAccount.AccountNo);
                PaymentAccountId = po.PaymentAccountId;
                PaymentAccountType = po.PaymentAccount.Type;
            }
            BillingReceiveTypeId = po.BillingReceiveType;
            BillingReceiveType = po.BillingReceiveTypeNavigation?.Name ?? string.Empty;
            ReceiptReceiveTypeId = po.ReceiptReceiveType;
            ReceiptReceiveType = po.ReceiptReceiveTypeNavigation?.Name ?? string.Empty;
            Status = po.Status;
            RequesterName = String.Format("{0} {1}", po.User.Firstname, po.User.Lastname);
            ApproverName = po.Approver != null ? string.Format("{0} {1}", po.Approver.Firstname, po.Approver.Lastname) : string.Empty;
            IsReadCancel = po.IsReadCancel.HasValue ? po.IsReadCancel.Value : false;
            decimal sum = 0; 
            if (po.PoDetails.Any())
            {
                foreach(var item in po.PoDetails)
                {
                    Details.Add(new PoDetailResponse(item));
                }
                sum = po.PoDetails.Sum(detail => detail.GrandTotal).Value;
            }
            GrandTotal = sum != 0 ? sum : po.GrandTotal.HasValue ? po.GrandTotal.Value : 0;
            WhtRate = po.WhtRate.HasValue ? po.WhtRate.Value * 100 : 3;
            Remark = string.IsNullOrWhiteSpace(po.Remark) ? string.Empty : po.Remark;
            CreateDate = po.CreateDate;
        }
        private bool IsPayBefore(string creditType)
        {
            if (creditType == CreditTypeConstants.NonCredit) return true;
            return false;
        }
    }
    public class PoDetailResponse
    {
        public long? PoDetailId { get; set; }
        public long? Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string PrNo { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public bool IsAdvancePay { get; set; } = false;
        public string ContractorName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string ApproverName { get; set; } = string.Empty;
        public long? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string AdditionalCode { get; set; } = string.Empty;
        public string AdditionalOtherCode { get; set; } = string.Empty;
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
            PoDetailId = detail.Id;
            if (detail.PrDetails.Any())
            {
                var prDetail = detail.PrDetails.FirstOrDefault();
                if(prDetail != null)
                {
                    Id = prDetail.Id;
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
                    var approver = pr.Approver;
                    if (approver != null)
                        ApproverName = String.Format("{0} {1}", approver.Firstname, approver.Lastname);
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
            ProductId = detail.ProductId;
            if (detail.Product != null)
            {
                Name = detail.Product.Name;
                Unit = detail.Product.Unit;
                AdditionalCode = string.IsNullOrWhiteSpace(detail.Code) ? string.Empty : detail.Code;
                AdditionalOtherCode = string.IsNullOrWhiteSpace(detail.ProductCode) ? string.Empty : detail.ProductCode;
            }
        }
    }
}

