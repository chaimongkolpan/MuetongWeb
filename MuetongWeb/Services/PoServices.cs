using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PoServices : IPoServices
    {
        private readonly ILogger<PoServices> _logger;
        private readonly IFileServices _fileServices;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPoRepositories _poRepositories;
        private readonly IPrRepositories _prRepositories;
        private readonly IProductRepositories _productRepositories;
        private readonly IStoreRepositories _storeRepositories;
        private readonly ISettingConstantRepositories _settingConstantRepositories;
        public PoServices
        (
            ILogger<PoServices> logger,
            IFileServices fileServices,
            IProjectRepositories projectRepositories,
            IPoRepositories poRepositories,
            IPrRepositories prRepositories,
            IProductRepositories productRepositories,
            IStoreRepositories storeRepositories,
            ISettingConstantRepositories settingConstantRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;
            _projectRepositories = projectRepositories;
            _poRepositories = poRepositories;
            _prRepositories = prRepositories;
            _productRepositories = productRepositories;
            _storeRepositories = storeRepositories;
            _settingConstantRepositories = settingConstantRepositories;
        }
        #region Page Service
        public async Task<PoModel> IndexAsync(bool canEdit, UserInfoModel user)
        {
            try
            {
                IEnumerable<Project> projects;
                if (RoleHelpers.CanSeeAllProject(user.Role))
                    projects = await _projectRepositories.GetAsync();
                else
                    projects = await _projectRepositories.GetByUserIdAsync(user.Id);
                var projectResponses = new List<ProjectResponse>();
                foreach (var project in projects)
                {
                    projectResponses.Add(new ProjectResponse(project));
                }
                var response = new PoModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexAsync: " + ex.Message);
                return new PoModel();
            }
        }
        public async Task<PoModel> ApproverAsync(bool canEdit, UserInfoModel user)
        {
            try
            {
                IEnumerable<Project> projects;
                if (RoleHelpers.CanSeeAllProject(user.Role))
                    projects = await _projectRepositories.GetAsync();
                else
                    projects = await _projectRepositories.GetByUserIdAsync(user.Id);
                var projectResponses = new List<ProjectResponse>();
                foreach (var project in projects)
                {
                    projectResponses.Add(new ProjectResponse(project));
                }
                var response = new PoModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => ApproverAsync: " + ex.Message);
                return new PoModel();
            }
        }
        #endregion
        #region Api Service
        public async Task<bool> IndexAddAsync(PoIndexAddRequest request)
        {
            try
            {
                string poNo = await GetPoNo();
                var po = new Po()
                {
                    PoNo = poNo,
                    StoreId = request.StoreId,
                    PlanTransferDate = request.PlanTransferDate,
                    CreditType = request.CreditType.HasValue ? request.CreditType.Value : 0,
                    DateValue = request.DateValue,
                    DateSpecific = request.DateSpecific,
                    BgContractNo = request.BgContractNo,
                    ChequeNo = request.ChequeNo,
                    PaymentType = request.PaymentType.HasValue ? request.PaymentType.Value : 0,
                    PaymentAccountId = request.PaymentAccountId.HasValue ? request.PaymentAccountId.Value : 0,
                    PaymentDate = request.PayDate,
                    BillingReceiveType = request.ReceiveBillingTypeId.HasValue ? request.ReceiveBillingTypeId.Value : 0,
                    ReceiptReceiveType = request.ReceiveReceiptTypeId.HasValue ? request.ReceiveReceiptTypeId.Value : 0,
                    Status = StatusConstants.PoWaitingApprove,
                    UserId = request.User?.Id ?? 0,
                    CreateDate = DateTime.Now,
                    WhtRate = request.WhtRate,
                    VatRate = request.VatRate,
                };
                decimal? subtotal = 0;
                decimal? discount = 0;
                decimal? total = 0;
                decimal? vat = 0;
                decimal? wht = 0;
                decimal? grandtotal = 0;
                if (await _poRepositories.AddAsync(po))
                {
                    if (request.Details != null && request.Details.Any())
                    {
                        var prDetailIds = request.Details.Select(detail => detail.PrDetailId.Value).ToList();
                        var prDetails = await _prRepositories.GetByIdsAsync(prDetailIds);
                        List<PoDetail> details = new List<PoDetail>();
                        foreach (var detail in request.Details)
                        {
                            var prDetail = prDetails.FirstOrDefault(d => d.Id == detail.PrDetailId);
                            if (prDetail == null)
                                continue;
                            var tmp = new PoDetail()
                            {
                                PoId = po.Id,
                                ProductId = prDetail.ProductId,
                                Quantity = prDetail.Quantity,
                                PricePerUnit = detail.PricePerUnit,
                                Discount = detail.Discount,
                                WhtRate = request.WhtRate,
                                VatRate = request.VatRate,
                                SubTotal = detail.PricePerUnit * prDetail.Quantity
                            };
                            tmp.Total = tmp.SubTotal - tmp.Discount;
                            tmp.Vat = detail.IsVat.HasValue && detail.IsVat.Value ? tmp.Total * tmp.VatRate : 0;
                            tmp.Wht = detail.IsWht.HasValue && detail.IsWht.Value ? tmp.Total * tmp.WhtRate : 0;
                            tmp.GrandTotal = tmp.Total + tmp.Vat - tmp.Wht;
                            subtotal += tmp.SubTotal;
                            discount += tmp.Discount;
                            total += tmp.Total;
                            vat += tmp.Vat;
                            wht += tmp.Wht;
                            grandtotal += tmp.GrandTotal;
                            await _poRepositories.AddDetailAsync(tmp);
                            prDetail.Status = StatusConstants.PrDetailWaitingOrder;
                            prDetail.PoDetailId = tmp.Id;
                            await _prRepositories.UpdateDetailAsync(prDetail);
                        }
                        if (request.HasAdditional.HasValue && request.HasAdditional.Value)
                        {
                            long otherProductId = 0;
                            var tmp = new PoDetail()
                            {
                                Code = request.AdditionalType,
                                ProductCode = request.AdditionalOther,
                                PoId = po.Id,
                                ProductId = otherProductId,
                                Quantity = 1,
                                PricePerUnit = request.Other?.PricePerUnit,
                                Discount = request.Other?.Discount,
                                WhtRate = request.WhtRate,
                                VatRate = request.VatRate,
                                SubTotal = request.Other?.PricePerUnit
                            };
                            tmp.Total = tmp.SubTotal - tmp.Discount;
                            tmp.Vat = (request.Other?.IsVat.HasValue ?? false) && (request.Other?.IsVat.Value ?? false) ? tmp.Total * tmp.VatRate : 0;
                            tmp.Wht = (request.Other?.IsWht.HasValue ?? false) && (request.Other?.IsWht.Value ?? false) ? tmp.Total * tmp.WhtRate : 0;
                            tmp.GrandTotal = tmp.Total + tmp.Vat - tmp.Wht;
                            subtotal += tmp.SubTotal;
                            discount += tmp.Discount;
                            total += tmp.Total;
                            vat += tmp.Vat;
                            wht += tmp.Wht;
                            grandtotal += tmp.GrandTotal;
                            await _poRepositories.AddDetailAsync(tmp);
                        }
                    }
                    po.SubTotal = subtotal;
                    po.Discount = discount;
                    po.Total = total;
                    po.Vat = vat;
                    po.Wht = wht;
                    po.GrandTotal = grandtotal;
                    if (request.Files != null && request.Files.Any())
                    {
                        await _fileServices.ImportFileList(po.Id, FileConstants.PoPathType, request.Files);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexAddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<PoIndexResponse> IndexSearchAsync(PoIndexSearchRequest request)
        {
            try
            {
                var prs = await _prRepositories.SearchAsync(request);
                var details = await _prRepositories.SearchDetailAsync(request);
                var detailIds = details.Where(d => d.PoDetailId.HasValue).Select(d => d.PoDetailId.Value).ToList();
                var pos = await _poRepositories.SearchAsync(request, detailIds);
                var response = new PoIndexResponse(pos, prs);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexSearchAsync: " + ex.Message);
                return new PoIndexResponse();
            }
        }
        public async Task<bool> UpdateIndexPo(long id, long userId, PoIndexUpdateRequest request)
        {
            try
            {
                if (request == null)
                    return false;
                decimal? subtotal = 0;
                decimal? discount = 0;
                decimal? total = 0;
                decimal? vat = 0;
                decimal? wht = 0;
                decimal? grandtotal = 0;
                var po = await _poRepositories.FindAsync(id);
                if (po == null)
                    return false;
                po.StoreId = request.StoreId;
                po.CreditType = request.CreditType.Value;
                po.DateSpecific = request.DateSpecific;
                po.DateValue = request.DateValue;
                po.BgContractNo = request.BgContractNo;
                po.ChequeNo = request.ChequeNo;
                po.PaymentType = request.PaymentType.Value;
                po.PaymentAccountId = request.PaymentAccountId;
                po.PaymentDate = request.PayDate;
                po.BillingReceiveType = request.ReceiveBillingTypeId;
                po.ReceiptReceiveType = request.ReceiveReceiptTypeId;
                po.PlanTransferDate = request.PlanTransferDate;
                po.Status = StatusConstants.PoWaitingApprove;
                po.WhtRate = request.WhtRate;
                po.VatRate = request.VatRate;
                po.UserId = userId;
                po.ModifyDate = DateTime.Now;
                var details = await _poRepositories.GetDetailAsync(id);
                var ids = new List<long>();
                if(request.Details != null && request.Details.Any())
                {
                    var prDetailIds = request.Details.Select(detail => detail.PrDetailId.Value).ToList();
                    var prDetails = await _prRepositories.GetByIdsAsync(prDetailIds);
                    List<PoDetail> addDetails = new List<PoDetail>();
                    List<long> addPrDetails = new List<long>();
                    foreach (var detail in request.Details)
                    {
                        if (!detail.Id.HasValue || (detail.Id.HasValue && detail.Id.Value == 0))
                        {
                            var prDetail = prDetails.FirstOrDefault(d => d.Id == detail.PrDetailId);
                            if (prDetail == null)
                                continue;
                            var addDetail = new PoDetail() 
                            { 
                                PoId = id,
                                ProductId = prDetail.ProductId,
                                Quantity = prDetail.Quantity,
                                PricePerUnit = detail.PricePerUnit,
                                Discount = detail.Discount,
                                WhtRate = request.WhtRate,
                                VatRate = request.VatRate,
                                SubTotal = detail.PricePerUnit * prDetail.Quantity
                            };
                            addDetail.Total = addDetail.SubTotal - addDetail.Discount;
                            addDetail.Vat = detail.IsVat.HasValue && detail.IsVat.Value ? addDetail.Total * addDetail.VatRate : 0;
                            addDetail.Wht = detail.IsWht.HasValue && detail.IsWht.Value ? addDetail.Total * addDetail.WhtRate : 0;
                            addDetail.GrandTotal = addDetail.Total + addDetail.Vat - addDetail.Wht;
                            subtotal += addDetail.SubTotal;
                            discount += addDetail.Discount;
                            total += addDetail.Total;
                            vat += addDetail.Vat;
                            wht += addDetail.Wht;
                            grandtotal += addDetail.GrandTotal;
                            addDetails.Add(addDetail);
                            addPrDetails.Add(detail.PrDetailId.Value);
                        }
                        else 
                        {
                            var tmp = details.FirstOrDefault(x => x.Id == detail.Id);
                            tmp.PricePerUnit = detail.PricePerUnit;
                            tmp.Discount = detail.Discount;
                            tmp.WhtRate = request.WhtRate;
                            tmp.VatRate = request.VatRate;
                            tmp.SubTotal = detail.PricePerUnit * tmp.Quantity;
                            tmp.Total = tmp.SubTotal - tmp.Discount;
                            tmp.Vat = detail.IsVat.HasValue && detail.IsVat.Value ? tmp.Total * tmp.VatRate : 0;
                            tmp.Wht = detail.IsWht.HasValue && detail.IsWht.Value ? tmp.Total * tmp.WhtRate : 0;
                            tmp.GrandTotal = tmp.Total + tmp.Vat - tmp.Wht;
                            subtotal += tmp.SubTotal;
                            discount += tmp.Discount;
                            total += tmp.Total;
                            vat += tmp.Vat;
                            wht += tmp.Wht;
                            grandtotal += tmp.GrandTotal;
                        }
                    }
                    if (addDetails.Any())
                    {
                        await _poRepositories.AddDetailRangeAsync(addDetails);
                        await _prRepositories.UpdateAllDetailStatus(addPrDetails, StatusConstants.PrDetailWaitingOrder);
                    }
                    await _poRepositories.UpdateDetailRangeAsync(details);
                    ids = request.Details.Where(x => x.Id.HasValue && x.Id.Value != 0)
                        .Select(x => x.Id.Value).ToList();
                }

                if (ids.Any())
                {
                    var deleteIds = details.Where(x => ids.Contains(x.Id) && x.ProductId != 0).Select(x => x.Id).ToList();
                    await _prRepositories.UpdateAllDetailStatusByPoDetail(deleteIds, StatusConstants.PrDetailWaitingOrder);
                    await _poRepositories.DeleteDetailRangeAsync(deleteIds);
                }
                if (request.Other != null)
                {
                    if (!details.Any(x => x.ProductId == 0))
                    {
                        var addDetail = new PoDetail()
                        {
                            Code = request.AdditionalType,
                            ProductCode = request.AdditionalOther,
                            PoId = po.Id,
                            ProductId = 0,
                            Quantity = 1,
                            PricePerUnit = request.Other?.PricePerUnit,
                            Discount = request.Other?.Discount,
                            WhtRate = request.WhtRate,
                            VatRate = request.VatRate,
                            SubTotal = request.Other?.PricePerUnit
                        };

                        addDetail.Total = addDetail.SubTotal - addDetail.Discount;
                        addDetail.Vat = (request.Other?.IsVat.HasValue ?? false) && (request.Other?.IsVat.Value ?? false) ? addDetail.Total * addDetail.VatRate : 0;
                        addDetail.Wht = (request.Other?.IsWht.HasValue ?? false) && (request.Other?.IsWht.Value ?? false) ? addDetail.Total * addDetail.WhtRate : 0;
                        addDetail.GrandTotal = addDetail.Total + addDetail.Vat - addDetail.Wht;
                        subtotal += addDetail.SubTotal;
                        discount += addDetail.Discount;
                        total += addDetail.Total;
                        vat += addDetail.Vat;
                        wht += addDetail.Wht;
                        grandtotal += addDetail.GrandTotal;
                        await _poRepositories.AddDetailAsync(addDetail);
                    }
                    else
                    {
                        var updateDetail = details.FirstOrDefault(x => x.ProductId == 0);
                        if (updateDetail != null)
                        {
                            updateDetail.PricePerUnit = request.Other?.PricePerUnit;
                            updateDetail.Discount = request.Other?.Discount;
                            updateDetail.WhtRate = request.WhtRate;
                            updateDetail.VatRate = request.VatRate;
                            updateDetail.SubTotal = request.Other?.PricePerUnit;
                            updateDetail.Total = updateDetail.SubTotal - updateDetail.Discount;
                            updateDetail.Vat = (request.Other?.IsVat.HasValue ?? false) && (request.Other?.IsVat.Value ?? false) ? updateDetail.Total * updateDetail.VatRate : 0;
                            updateDetail.Wht = (request.Other?.IsWht.HasValue ?? false) && (request.Other?.IsWht.Value ?? false) ? updateDetail.Total * updateDetail.WhtRate : 0;
                            updateDetail.GrandTotal = updateDetail.Total + updateDetail.Vat - updateDetail.Wht;
                            subtotal += updateDetail.SubTotal;
                            discount += updateDetail.Discount;
                            total += updateDetail.Total;
                            vat += updateDetail.Vat;
                            wht += updateDetail.Wht;
                            grandtotal += updateDetail.GrandTotal;
                            await _poRepositories.UpdateDetailAsync(updateDetail);
                        }
                    }
                }
                else if(details.Any(x => x.ProductId == 0))
                {
                    var delDetail = details.FirstOrDefault(x => x.ProductId == 0);
                    if (delDetail != null)
                        await _poRepositories.DeleteDetailAsync(delDetail.Id);
                }
                po.SubTotal = subtotal;
                po.Discount = discount;
                po.Total = total;
                po.Vat = vat;
                po.Wht = wht;
                po.GrandTotal = grandtotal;
                await _poRepositories.UpdateAsync(po);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => UpdateIndexPo: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> Read(long id)
        {
            try
            {
                return await _poRepositories.Read(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => Read: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> Cancel(long id, string? remark)
        {
            try
            {
                return await _poRepositories.Cancel(id, remark);
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => Cancel: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> Approve(long id, long userId)
        {
            try
            {
                return await _poRepositories.Approve(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => Approve: " + ex.Message);
                return false;
            }
        }
        public async Task<PoIndexPrResponse> IndexSearchPrAsync(PoIndexPrSearch request)
        {
            try
            {
                var details = await _prRepositories.SearchAsync(request);
                var response = new PoIndexPrResponse(details);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PoServices => IndexSearchPrAsync: " + ex.Message);
                return new PoIndexPrResponse();
            }
        }
        public async Task<List<UserResponse>> GetRequesterByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var users = prs.Select(pr => pr.User).DistinctBy(user => user.Id).ToList();
            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                response.Add(new UserResponse(user));
            }
            return response;
        }
        public async Task<List<string>> GetPrNoByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var response = prs.Select(pr => pr.PrNo).Distinct().ToList();
            return response;
        }
        public async Task<List<string>> GetPoNoByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var response = prs.Select(pr => pr.PrNo).Distinct().ToList();
            return response;
        }
        public async Task<StoreCollectionResponse> GetStore()
        {
            var stores = await _storeRepositories.GetAsync();
            var response = new StoreCollectionResponse(stores);
            return response;
        }
        public async Task<ReceiveSettingConstantResponse> GetReceive()
        {
            var billing = await _settingConstantRepositories.GetAsync(SettingConstants.ReceiveBillingType);
            var receipt = await _settingConstantRepositories.GetAsync(SettingConstants.ReceiveReceiptType);
            var response = new ReceiveSettingConstantResponse(billing, receipt);
            return response;
        }
        public async Task<TypeSettingConstantResponse> GetTypeAsync()
        {
            var credits = await _settingConstantRepositories.GetAsync(SettingConstants.CreditType);
            var payments = await _settingConstantRepositories.GetAsync(SettingConstants.PaymentType);
            var response = new TypeSettingConstantResponse(credits, payments);
            return response;
        }
        #endregion
        #region Private function
        public async Task<string> GetPoNo()
        {
            var today = DateTime.Today;
            var thisYear = today.Year;
            var thisMonth = today.Month;
            var result = await _poRepositories.GetPoNo(SequenceConstants.GetPoNoByMonth(thisMonth));
            await _poRepositories.ExecuteSql(SequenceConstants.ResetSequence[thisMonth - 1]);
            return SequenceConstants.PoNoBuilder(thisYear, thisMonth, result);
        }
        #endregion
    }
}
