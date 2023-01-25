using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Helpers;
using MuetongWeb.Constants;

namespace MuetongWeb.Repositories
{
    public class PoRepositories : IPoRepositories
    {
        private readonly MuetongContext _dbContext;
        public PoRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Po>> GetAsync()
        {
            return await _dbContext.Pos.OrderBy(po => po.CreateDate)
                                       .Include(po => po.PoDetails)
                                       .Include(po => po.User)
                                       .ToListAsync();
        }
        public async Task<Po?> GetAsync(long id)
        {
            return await _dbContext.Pos.Where(po => po.Id == id)
                                       .Include(po => po.PoDetails)
                                       .Include(po => po.User)
                                       .FirstOrDefaultAsync();
        }
        public async Task<List<PoDetail>> GetDetailAsync(long id)
        {
            return await _dbContext.PoDetails.Where(detail => detail.PoId == id)
                                       .ToListAsync();
        }
        public async Task<Po?> FindAsync(long id)
        {
            return await _dbContext.Pos.FindAsync(id);
        }
        public async Task<IEnumerable<Po>> GetByBilling(List<long> Ids)
        {
            return await _dbContext.Pos.Where(po => Ids.Contains(po.Id))
                        .OrderBy(po => po.CreateDate)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.Product)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.ProjectCode)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Project)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Contractor)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.User)
                        .Include(po => po.Store)
                        .Include(po => po.CreditTypeNavigation)
                        .Include(po => po.PaymentTypeNavigation)
                        .Include(po => po.PaymentAccount)
                        .Include(po => po.BillingReceiveTypeNavigation)
                        .Include(po => po.ReceiptReceiveTypeNavigation)
                        .Include(po => po.User)
                        .Include(po => po.Approver)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Po>> SearchAsync(PoIndexSearchRequest request, List<long> detailIds)
        {
            return await _dbContext.Pos.Where(po =>
                        (string.IsNullOrWhiteSpace(request.Status) || request.Status == RequestConstants.AllString || po.Status == request.Status)
                        && (po.PoDetails.Any(pd => detailIds.Contains(pd.Id)))
                        )
                        .OrderBy(po => po.CreateDate)
                        .Include(po => po.PoDetails)
                        .ThenInclude(poDetail => poDetail.Product)
                        .Include(po => po.PoDetails)
                        .ThenInclude(poDetail => poDetail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Project)
                        .Include(po => po.PoDetails)
                        .ThenInclude(poDetail => poDetail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.User)
                        .Include(po => po.PoDetails)
                        .ThenInclude(poDetail => poDetail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Approver)
                        .Include(po => po.PoDetails)
                        .ThenInclude(poDetail => poDetail.PrDetails)
                        .ThenInclude(prDetail => prDetail.ProjectCode)
                        .Include(po => po.Store)
                        .Include(po => po.CreditTypeNavigation)
                        .Include(po => po.PaymentTypeNavigation)
                        .Include(po => po.PaymentAccount)
                        .Include(po => po.BillingReceiveTypeNavigation)
                        .Include(po => po.ReceiptReceiveTypeNavigation)
                        .Include(po => po.User)
                        .Include(po => po.Approver)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Po>> SearchBillingAsync(BillingIndexPoSearch request)
        {
            return await _dbContext.Pos.Where(po =>  (po.Status == StatusConstants.PoRequested || po.Status == StatusConstants.PoComplete)
                        && (po.PoBillings == null || !po.PoBillings.Any() || po.PoBillings.All(pb => pb.Billing != null && pb.Billing.Status == StatusConstants.BillingCancel))
                        && (request.StoreId == RequestConstants.AllValue || po.StoreId == request.StoreId)
                        && (string.IsNullOrWhiteSpace(request.PoNo) || po.PoNo == request.PoNo)
                        )
                        .OrderBy(po => po.CreateDate)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.Product)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.ProjectCode)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Project)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Contractor)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.User)
                        .Include(po => po.Store)
                        .Include(po => po.CreditTypeNavigation)
                        .Include(po => po.PaymentTypeNavigation)
                        .Include(po => po.PaymentAccount)
                        .Include(po => po.BillingReceiveTypeNavigation)
                        .Include(po => po.ReceiptReceiveTypeNavigation)
                        .Include(po => po.User)
                        .Include(po => po.Approver)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Po>> SearchBillingAsync(BillingIndexSearch request)
        {
            return await _dbContext.Pos.Where(po => (po.Status == StatusConstants.PoRequested || po.Status == StatusConstants.PoComplete)
                        && (po.PoBillings == null || !po.PoBillings.Any() || po.PoBillings.All(pb => pb.Billing != null && pb.Billing.Status == StatusConstants.BillingCancel))
                        && (string.IsNullOrWhiteSpace(request.PoNo) || request.PoNo == RequestConstants.AllString || po.PoNo == request.PoNo)
                        )
                        .OrderBy(po => po.CreateDate)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.Product)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.ProjectCode)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Project)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.Contractor)
                        .Include(po => po.PoDetails)
                        .ThenInclude(detail => detail.PrDetails)
                        .ThenInclude(prDetail => prDetail.Pr)
                        .ThenInclude(pr => pr.User)
                        .Include(po => po.Store)
                        .Include(po => po.CreditTypeNavigation)
                        .Include(po => po.PaymentTypeNavigation)
                        .Include(po => po.PaymentAccount)
                        .Include(po => po.BillingReceiveTypeNavigation)
                        .Include(po => po.ReceiptReceiveTypeNavigation)
                        .Include(po => po.User)
                        .Include(po => po.Approver)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Po>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Pos//.Where(po => po.ProjectId == projectId)
                                       .OrderBy(po => po.CreateDate)
                                       .Include(po => po.PoDetails)
                                       .Include(po => po.User)
                                       .ToListAsync();
        }
        public async Task<bool> AddAsync(Po po)
        {
            await _dbContext.Pos.AddAsync(po);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Po po)
        {
            _dbContext.Pos.Update(po);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddDetailAsync(PoDetail detail)
        {
            await _dbContext.PoDetails.AddAsync(detail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddDetailRangeAsync(List<PoDetail> details)
        {
            await _dbContext.PoDetails.AddRangeAsync(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDetailAsync(PoDetail detail)
        {
            _dbContext.PoDetails.Update(detail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDetailRangeAsync(List<PoDetail> details)
        {
            _dbContext.PoDetails.UpdateRange(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDetailAsync(long id)
        {
            var detail = _dbContext.PoDetails.FirstOrDefault(x => x.Id == id);
            if (detail == null)
                return false;
            _dbContext.PoDetails.Remove(detail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDetailRangeAsync(List<long> ids)
        {
            var details = _dbContext.PoDetails.Where(x => ids.Contains(x.Id));
            if (!details.Any())
                return false;
            _dbContext.PoDetails.RemoveRange(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Read(long id)
        {
            var po = await _dbContext.Pos.FindAsync(id);
            if (po == null)
                return false;
            po.IsReadCancel = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Cancel(long id, string? remark)
        {
            var po = await _dbContext.Pos.FindAsync(id);
            if (po == null)
                return false;
            po.Status = StatusConstants.PoCancel;
            po.Remark = remark;
            po.ModifyDate = DateTime.Now;
            var poDetailIds = await _dbContext.PoDetails.Where(detail => detail.PoId == id)
                                                        .Select(detail => detail.Id)
                                                        .ToListAsync();
            await _dbContext.PrDetails.Where(detail => detail.PoDetailId.HasValue
                                    && poDetailIds.Contains(detail.PoDetailId.Value))
                                    .ForEachAsync(detail => {
                                        detail.Status = StatusConstants.PrDetailWaitingOrder;
                                    });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Approve(long id, long userId)
        {
            var po = await _dbContext.Pos.FindAsync(id);
            if (po == null)
                return false;
            po.Status = StatusConstants.PoRequested;
            po.ApproverId = userId;
            po.ApproveDate = DateTime.Now;
            po.ModifyDate = DateTime.Now;
            var poDetailIds = await _dbContext.PoDetails.Where(detail => detail.PoId == id)
                                                        .Select(detail => detail.Id)
                                                        .ToListAsync();
            await _dbContext.PrDetails.Where(detail => detail.PoDetailId.HasValue 
                                    && poDetailIds.Contains(detail.PoDetailId.Value))
                                    .ForEachAsync(detail => {
                                        detail.Status = StatusConstants.PrDetailWaitingTransfer;
                                    });
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
