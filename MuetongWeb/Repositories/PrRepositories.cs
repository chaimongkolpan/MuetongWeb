using MuetongWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuetongWeb.Repositories
{
    public class PrRepositories : IPrRepositories
    {
        private readonly MuetongContext _dbContext;
        public PrRepositories(MuetongContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pr>> SearchAsync(PrIndexSearchRequest request)
        {
            return await _dbContext.Prs.Where(pr => pr.Project != null && request.User != null
                            && (pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || pr.ProjectId == request.ProjectId.Value)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || pr.PrNo == request.PrNo)
                            && (!request.RequesterId.HasValue || request.RequesterId.Value == RequestConstants.AllValue || pr.UserId == request.RequesterId.Value)
                            && (string.IsNullOrWhiteSpace(request.Status) || request.Status == RequestConstants.AllString || pr.Status == request.Status)
                        )
                        .OrderBy(pr => pr.CreateDate)
                        .Include(pr => pr.Project)
                        .Include(pr => pr.Contractor)
                        .Include(pr => pr.User).ThenInclude(user => user.Role)
                        .Include(pr => pr.User).ThenInclude(user => user.SubDepartment)
                        .ThenInclude(sub => sub.Department)
                        .ThenInclude(de => de.Line)
                        .Include(pr => pr.Approver)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.Product)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PoDetail).ThenInclude(prDetail => prDetail.Po)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.ProjectCode)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> SearchAsync(PrReceiveSearchRequest request)
        {
            return await _dbContext.Prs.Where(pr => pr.Project != null && request.User != null
                            && (pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || pr.ProjectId == request.ProjectId.Value)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || pr.PrNo == request.PrNo)
                            && (!request.RequesterId.HasValue || request.RequesterId.Value == RequestConstants.AllValue || pr.UserId == request.RequesterId.Value)
                            //&& (pr.Status == StatusConstants.PrComplete || pr.Status == StatusConstants.PrWaitingTransfer)
                            && (pr.PrDetails.Any(detail => detail.Status == StatusConstants.PrDetailComplete || detail.Status == StatusConstants.PrDetailWaitingTransfer))
                        )
                        .OrderBy(pr => pr.CreateDate)
                        .Include(pr => pr.Project)
                        .Include(pr => pr.Contractor)
                        .Include(pr => pr.User)
                        .Include(pr => pr.Approver)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.Product)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PoDetail).ThenInclude(prDetail => prDetail.Po)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.ProjectCode)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PrReceives).ThenInclude(receive => receive.User)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> SearchAsync(PoIndexSearchRequest request)
        {
            return await _dbContext.Prs.Where(pr => pr.Project != null && request.User != null
                            && (pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || pr.ProjectId == request.ProjectId.Value)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || pr.PrNo == request.PrNo)
                            && (!request.RequesterId.HasValue || request.RequesterId.Value == RequestConstants.AllValue || pr.UserId == request.RequesterId.Value)
                            && pr.PrDetails.Any(detail => detail.Status == StatusConstants.PrDetailRequested || detail.Status == StatusConstants.PrDetailWaitingOrder)
                        )
                        .OrderBy(pr => pr.CreateDate)
                        .Include(pr => pr.Project)
                        .Include(pr => pr.Contractor)
                        .Include(pr => pr.User)
                        .Include(pr => pr.Approver)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.Product)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PoDetail).ThenInclude(prDetail => prDetail.Po)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.ProjectCode)
                        .ToListAsync();
        }
        public async Task<List<long>> SearchBillAsync(BillingIndexSearch request)
        {
            return await _dbContext.PrDetails.Where(detail => detail.Pr.Project != null && request.User != null
                            && (detail.Pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (request.ProjectId == RequestConstants.AllValue || detail.Pr.ProjectId == request.ProjectId)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || detail.Pr.PrNo == request.PrNo)
                            && (request.RequesterId == RequestConstants.AllValue || detail.Pr.UserId == request.RequesterId)
                            && detail.PoDetail != null 
                            && (detail.Status == StatusConstants.PrDetailComplete || detail.Status == StatusConstants.PrDetailWaitingTransfer)
                            && (string.IsNullOrWhiteSpace(request.PoNo) || request.PoNo == RequestConstants.AllString || detail.PoDetail.Po.PoNo == request.PoNo)
                        ).Include(detail => detail.PoDetail)
                        .Select(detail => detail.PoDetail.PoId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<PrDetail>> SearchDetailAsync(PoIndexSearchRequest request)
        {
            return await _dbContext.PrDetails.Where(detail => detail.Pr.Project != null && request.User != null
                            && (detail.Pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || detail.Pr.ProjectId == request.ProjectId.Value)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || detail.Pr.PrNo == request.PrNo)
                            && (!request.RequesterId.HasValue || request.RequesterId.Value == RequestConstants.AllValue || detail.Pr.UserId == request.RequesterId.Value)
                        )
                        .ToListAsync();
        }
        public async Task<Pr?> GetAsync(long id)
        {
            return await _dbContext.Prs.Where(pr => pr.Id == id)
                                       .Include(pr => pr.Project)
                                       .Include(pr => pr.Contractor)
                                       .Include(pr => pr.User)
                                       .Include(pr => pr.Approver)
                                       .Include(pr => pr.PrDetails).ThenInclude(detail => detail.Product)
                                       .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PoDetail).ThenInclude(prDetail => prDetail.Po)
                                       .Include(pr => pr.PrDetails).ThenInclude(detail => detail.ProjectCode)
                                       .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Pr>> GetAsync()
        {
            return await _dbContext.Prs.OrderBy(pr => pr.CreateDate)
                                       .Include(pr => pr.PrDetails)
                                       .Include(pr => pr.User).ThenInclude(user => user.Role)
                                       .Include(pr => pr.User).ThenInclude(user => user.SubDepartment)
                                       .ThenInclude(sub => sub.Department)
                                       .ThenInclude(de => de.Line)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Prs.Where(pr => pr.ProjectId == projectId)
                                       .OrderBy(pr => pr.CreateDate)
                                       .Include(pr => pr.PrDetails)
                                       .Include(pr => pr.User).ThenInclude(user => user.Role)
                                       .Include(pr => pr.User).ThenInclude(user => user.SubDepartment)
                                       .ThenInclude(sub => sub.Department)
                                       .ThenInclude(de => de.Line)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<PrDetail>> GetByPrAsync(long prId)
        {
            return await _dbContext.PrDetails.Where(detail => detail.PrId == prId)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<PrDetail>> GetByIdsAsync(List<long> detailId)
        {
            return await _dbContext.PrDetails.Where(detail => detailId.Contains(detail.Id))
                                       .ToListAsync();
        }
        public async Task<bool> AddAsync(Pr pr)
        {
            await _dbContext.Prs.AddAsync(pr);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddDetailAsync(PrDetail detail)
        {
            await _dbContext.PrDetails.AddAsync(detail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddDetailRangeAsync(List<PrDetail> details)
        {
            await _dbContext.PrDetails.AddRangeAsync(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Pr pr)
        {
            _dbContext.Prs.Update(pr);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDetailAsync(PrDetail detail)
        {
            _dbContext.PrDetails.Update(detail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDetailAsync(List<PrDetail> details)
        {
            _dbContext.PrDetails.UpdateRange(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDetailAsync(List<PrDetail> details)
        {
            _dbContext.PrDetails.RemoveRange(details);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Read(long id)
        {
            var pr = await _dbContext.Prs.FindAsync(id);
            if (pr == null)
                return false;
            pr.IsReadCancel = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Cancel(long id)
        {
            var pr = await _dbContext.Prs.FindAsync(id);
            if (pr == null)
                return false;
            pr.Status = StatusConstants.PrCancel;
            pr.ModifyDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Approve(long id, long userId)
        {
            var pr = await _dbContext.Prs.FindAsync(id);
            if (pr == null)
                return false;
            pr.Status = StatusConstants.PrRequested;
            pr.ApproverId = userId;
            pr.ApproveDate = DateTime.Now;
            pr.ModifyDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Disapprove(long id)
        {
            var pr = await _dbContext.Prs.FindAsync(id);
            if (pr == null)
                return false;
            pr.Status = StatusConstants.PrWaitingApprove;
            pr.ApproverId = null;
            pr.ApproveDate = null;
            pr.ModifyDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAllDetailStatus(long prId, string status)
        {
            var details = await _dbContext.PrDetails.Where(detail => detail.PrId == prId).ToListAsync();
            if (details.Any())
            {
                details.ForEach(detail => {
                    detail.Status = status;
                });
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAllDetailStatus(List<long> ids, string status)
        {
            var details = await _dbContext.PrDetails.Where(detail => ids.Contains(detail.Id)).ToListAsync();
            if (details.Any())
            {
                details.ForEach(detail => {
                    detail.Status = status;
                });
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAllDetailStatusByPoDetail(List<long> ids, string status)
        {
            var details = await _dbContext.PrDetails.Where(detail => detail.PoDetailId.HasValue && ids.Contains(detail.PoDetailId.Value)).ToListAsync();
            if (details.Any())
            {
                details.ForEach(detail => {
                    detail.Status = status;
                });
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDetailStatus(long id, string status)
        {
            var detail = await _dbContext.PrDetails.FindAsync(id);
            if (detail == null)
                return false;
            detail.Status = status;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<PrDetail>> SearchAsync(PoIndexPrSearch request)
        {
            var details = await _dbContext.PrDetails.Where(detail => detail.Pr.Project != null && request.User != null
                            && (detail.Status == StatusConstants.PrDetailRequested || detail.Status == StatusConstants.PrDetailWaitingOrder)
                            && (detail.Pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || detail.Pr.ProjectId == request.ProjectId)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || detail.Pr.PrNo == request.PrNo)
                            && (!request.ProductId.HasValue || request.ProductId.Value == RequestConstants.AllValue || detail.ProductId == request.ProductId)
                                                    )
                                                    .OrderBy(detail => detail.Pr.CreateDate)
                                                    .Include(detail => detail.Pr)
                                                    .ThenInclude(pr => pr.Project)
                                                    .Include(detail => detail.Pr)
                                                    .ThenInclude(pr => pr.User)
                                                    .Include(detail => detail.Product)
                                                    .Include(detail => detail.ProjectCode)
                                                    .ToListAsync();
            return details;
        }
        public async Task<bool> AddReceiveAsync(PrReceive receive)
        {
            await _dbContext.PrReceives.AddAsync(receive);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddReceiveRangeAsync(List<PrReceive> receives)
        {
            await _dbContext.PrReceives.AddRangeAsync(receives);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckReceive(List<long> detailId)
        {
            var details = await _dbContext.PrDetails.Where(detail => detailId.Contains(detail.Id))
                                                    .Include(detail => detail.PrReceives)
                                                    .ToListAsync();
            details.ForEach(detail =>
            {
                if (detail.PrReceives != null && detail.PrReceives.Any())
                {
                    var sum = detail.PrReceives.Sum(receive => receive.Quantity);
                    if (sum >= detail.Quantity)
                    {
                        detail.Status = StatusConstants.PrDetailComplete;
                    }
                }
            });
            await _dbContext.SaveChangesAsync();
            var detailIds = details.Where(detail => detail.PoDetailId.HasValue).Select(detail => detail.PoDetailId.Value).ToList();
            var pos = await _dbContext.Pos.Where(po => po.PoDetails.Any(detail => detailIds.Contains(detail.Id)))
                                        .Include(po => po.PoDetails).ThenInclude(detail => detail.PrDetails)
                                        .ToListAsync();
            pos.ForEach(po =>
            {
                if (po.PoDetails.All(detail => detail.PrDetails.All(detail => detail.Status == StatusConstants.PrDetailComplete)))
                {
                    po.Status = StatusConstants.PoComplete;
                    po.ModifyDate = DateTime.Now;
                }
            });
            await _dbContext.SaveChangesAsync();
            var prIds = details.Select(detail => detail.PrId).Distinct().ToList();
            var prs = await _dbContext.Prs.Where(pr => prIds.Contains(pr.Id)).Include(pr => pr.PrDetails).ToListAsync();
            prs.ForEach(pr =>
            {
                if (pr.PrDetails.All(detail => detail.Status == StatusConstants.PrDetailComplete))
                {
                    pr.Status = StatusConstants.PrComplete;
                    pr.ModifyDate = DateTime.Now;
                }
            });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DisapproveReceive(List<long> detailId) 
        {
            var details = await _dbContext.PrDetails.Where(detail => detailId.Contains(detail.Id))
                                       .ToListAsync();
            details.ForEach(detail =>
            {
                detail.Status = StatusConstants.PrDetailWaitingTransfer;
            });
            await _dbContext.SaveChangesAsync();
            var detailIds = details.Where(detail => detail.PoDetailId.HasValue).Select(detail => detail.PoDetailId.Value).ToList();
            var pos = await _dbContext.Pos.Where(po => po.PoDetails.Any(detail => detailIds.Contains(detail.Id)))
                                        .Include(po => po.PoDetails).ThenInclude(detail => detail.PrDetails)
                                        .ToListAsync();
            pos.ForEach(po =>
            {
                if (po.PoDetails.Any(detail => detail.PrDetails.Any(detail => detail.Status != StatusConstants.PrDetailComplete)))
                {
                    po.Status = StatusConstants.PoRequested;
                    po.ModifyDate = DateTime.Now;
                }
            });
            await _dbContext.SaveChangesAsync();
            var prIds = details.Select(detail => detail.PrId).Distinct().ToList();
            var prs = await _dbContext.Prs.Where(pr => prIds.Contains(pr.Id) && pr.Status == StatusConstants.PrComplete).ToListAsync();
            prs.ForEach(pr =>
            {
                if (pr.PrDetails.Any(detail => detail.Status != StatusConstants.PrDetailComplete))
                {
                    pr.Status = StatusConstants.PrWaitingTransfer;
                    pr.ModifyDate = DateTime.Now;
                }
            });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<PrReceive?> GetReceiveAsync(long id)
        {
            return await _dbContext.PrReceives.FindAsync(id);
        }
        public async Task<bool> UpdateReceiveAsync(PrReceive receive)
        {
            _dbContext.PrReceives.Update(receive);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
