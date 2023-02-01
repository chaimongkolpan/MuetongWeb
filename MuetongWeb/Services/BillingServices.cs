using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Models.Entities;
using MuetongWeb.Constants;
using MuetongWeb.Models.Pages;
using MuetongWeb.Helpers;

namespace MuetongWeb.Services
{
    public class BillingServices : IBillingServices
    {
        private readonly ILogger<BillingServices> _logger;
        private readonly IFileServices _fileServices;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPrRepositories _prRepositories;
        private readonly IPoRepositories _poRepositories;
        private readonly IBillingRepositories _billingRepositories;
        private readonly ISettingConstantRepositories _settingConstantRepositories;
        private readonly IStoreRepositories _storeRepositories;
        public BillingServices
        (
            ILogger<BillingServices> logger,
            IFileServices fileServices,
            IProjectRepositories projectRepositories,
            IPrRepositories prRepositories,
            IPoRepositories poRepositories, 
            IBillingRepositories billingRepositories,
            ISettingConstantRepositories settingConstantRepositories,
            IStoreRepositories storeRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;
            _projectRepositories = projectRepositories;
            _prRepositories = prRepositories;
            _poRepositories = poRepositories;
            _billingRepositories = billingRepositories;
            _settingConstantRepositories = settingConstantRepositories;
            _storeRepositories = storeRepositories;
        }
        #region Page
        public async Task<BillingModel> IndexAsync(bool canEdit, UserInfoModel user)
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
                var response = new BillingModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => IndexAsync: " + ex.Message);
                return new BillingModel();
            }
        }
        public async Task<BillingModel> ApproverAsync(bool canEdit, UserInfoModel user)
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
                var response = new BillingModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => ApproverAsync: " + ex.Message);
                return new BillingModel();
            }
        }
        #endregion
        #region Api Service
        public async Task<bool> AddAsync(BillingIndexAddRequest request)
        {
            try
            {
                if (request == null)
                    return false;
                if (request.Pos == null || (request.Pos != null && !request.Pos.Any()))
                    return false;
                var po = request.Pos.FirstOrDefault();
                if (po == null)
                    return false;
                Billing bill = new Billing()
                {
                    StoreId = po.StoreId.Value,
                    BillingNo = request.BillingNo,
                    BillingDate = request.BillingDate.Value,
                    Status = StatusConstants.BillingWaitingPayment,
                    PaymentType = po.PaymentTypeId.HasValue ? po.PaymentTypeId.Value : 0,
                    PaymentAccountId = po.PaymentAccountId.HasValue ? po.PaymentAccountId.Value : 0,
                    PaymentDate = po.PaymentDate,
                    Amount = request.Pos.Sum(po => po.GrandTotal),
                    UserId = request.User.Id,
                    CreateDate = DateTime.Now
                };
                await _billingRepositories.AddAsync(bill);
                List<PoBilling> poBillings = new List<PoBilling>();
                foreach (var poBill in request.Pos)
                {
                    poBillings.Add(new PoBilling()
                    {
                        PoId = poBill.Id,
                        BillingId = bill.Id
                    });
                }
                await _billingRepositories.AddPoAsync(poBillings);
                if (request.Files != null && request.Files.Any())
                {
                    await _fileServices.ImportFileList(bill.Id, FileConstants.BillingPathType, request.Files);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => AddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateAsync(long id, BillingIndexUpdateRequest request)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return false;
                bill.BillingNo = string.IsNullOrWhiteSpace(request.BillingNo) ? string.Empty : request.BillingNo;
                if(request.BillingDate.HasValue) bill.BillingDate = request.BillingDate.Value;
                bill.PaymentDate = request.PaymentDate;
                if (request.PaymentDate.HasValue)
                {
                    bill.Status = StatusConstants.BillingWaitingReceipt;
                    bill.IsPay = true;
                    bill.PaymentType = request.PaymentType.HasValue ? request.PaymentType.Value : 0;
                    bill.PaymentAccountId = request.PaymentAccount.HasValue ? request.PaymentAccount.Value : 0;
                }
                bill.Amount = request.Amount;
                bill.Remark = string.IsNullOrWhiteSpace(request.Remark) ? string.Empty : request.Remark;
                if(request.HasReceipt.HasValue && request.HasReceipt.Value)
                {
                    bill.HasReceipt = request.HasReceipt.Value;
                    bill.ReceiptNo = request.ReceiptNo;
                }
                if (request.HasInvoice.HasValue && request.HasInvoice.Value)
                {
                    bill.HasInvoice = request.HasInvoice.Value;
                    bill.InvoiceNo = request.InvoiceNo;
                }
                if (request.HasExtra.HasValue && request.HasExtra.Value)
                {
                    bill.HasExtra = request.HasExtra.Value;
                    bill.ExtraType = request.ExtraType;
                    bill.ExtraOther = request.ExtraOther;
                    bill.ExtraCost = request.ExtraAmount;
                }
                bill.ModifyDate = DateTime.Now;
                await _billingRepositories.UpdateAsync(bill);
                if (request.Files != null && request.Files.Any())
                {
                    await _fileServices.ImportFileList(id, FileConstants.BillingPathType, request.Files);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => UpdateAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> SendApproveAsync(long id)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return false;
                if (bill.IsPay.HasValue && bill.IsPay.Value && bill.HasReceipt.HasValue && bill.HasReceipt.Value)
                {
                    bill.Status = StatusConstants.BillingWaitingApprove;
                    bill.ModifyDate = DateTime.Now;
                    await _billingRepositories.UpdateAsync(bill);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => SendApproveAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> ApproveAsync(long id, BillingApproveRequest request)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return false;
                bill.Status = StatusConstants.BillingComplete;
                bill.ModifyDate = DateTime.Now;
                await _billingRepositories.UpdateAsync(bill);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => ApproveAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> ReadAsync(long id)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return false;
                bill.IsReadCancel = true;
                await _billingRepositories.UpdateAsync(bill);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => ReadAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> CancelAsync(long id, BillingCancelRequest request)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return false;
                bill.Status = StatusConstants.BillingCancel;
                bill.Remark = request.Remark;
                bill.IsReadCancel = false;
                bill.ModifyDate = DateTime.Now;
                await _billingRepositories.UpdateAsync(bill);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => CancelAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<BillingIndexResponse> Search(BillingIndexSearch request)
        {
            try
            {
                var poIds = await _prRepositories.SearchBillAsync(request);
                if (poIds == null)
                    return new BillingIndexResponse();
                var bills = await _billingRepositories.Search(request, poIds);
                List<long> ids = bills.Where(bill => bill.PoBillings != null).SelectMany(bill => bill.PoBillings.ToList()).Select(po => po.PoId).ToList();
                var pobills = await _poRepositories.GetByBilling(ids);
                var pos = await _poRepositories.SearchBillingAsync(request);
                List<long> billids = bills.Select(bill => bill.Id).ToList();
                var files = await _fileServices.GetFilesListAsync(billids, FileConstants.BillingPathType);
                var response = new BillingIndexResponse(bills, pos, pobills, files);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => Search: " + ex.Message);
                return new BillingIndexResponse();
            }
        }
        public async Task<BillingIndexPoResponse> SearchPo(BillingIndexPoSearch request)
        {
            try
            {
                var pos = await _poRepositories.SearchBillingAsync(request);
                var response = new BillingIndexPoResponse(pos);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("BillingServices => SearchPo: " + ex.Message);
                return new BillingIndexPoResponse();
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
        public async Task<List<string>> GetBillingNoByProject(long projectId)
        {
            IEnumerable<Billing> billing;
            if (projectId == 0)
                billing = await _billingRepositories.GetAsync();
            else
                billing = await _billingRepositories.GetByProjectAsync(projectId);
            var response = billing.Select(pr => pr.BillingNo).Distinct().ToList();
            return response;
        }
        public async Task<BillPaymentResponse> GetPaymentByBill(long id)
        {
            try
            {
                var bill = await _billingRepositories.GetAsync(id);
                if (bill == null)
                    return new BillPaymentResponse();
                var store = await _storeRepositories.GetAsync(bill.StoreId);
                if (store == null)
                    return new BillPaymentResponse();
                var storeRes = new StoreResponse(store);
                var paymentType = await _settingConstantRepositories.GetAsync(SettingConstants.PaymentType);
                var extraType = await _settingConstantRepositories.GetAsync(SettingConstants.ExtraType);
                var response = new BillPaymentResponse(extraType, storeRes.Payments, paymentType);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("BillingServices => GetPaymentByBill: " + ex.Message);
                return new BillPaymentResponse();
            }
        }
        #endregion
    }
}
