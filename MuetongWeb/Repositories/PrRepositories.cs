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
                        .Include(pr => pr.User)
                        .Include(pr => pr.Approver)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.Product)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.PoDetail).ThenInclude(prDetail => prDetail.Po)
                        .Include(pr => pr.PrDetails).ThenInclude(detail => detail.ProjectCode)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> SearchAsync(PoIndexSearchRequest request)
        {
            return await _dbContext.Prs.Where(pr => pr.Project != null && request.User != null
                            && (pr.Project.ProjectUsers.Any(pUser => pUser.UserId == request.User.Id) || RoleHelpers.CanSeeAllProject(request.User.Role))
                            && (!request.ProjectId.HasValue || request.ProjectId.Value == RequestConstants.AllValue || pr.ProjectId == request.ProjectId.Value)
                            && (string.IsNullOrWhiteSpace(request.PrNo) || request.PrNo == RequestConstants.AllString || pr.PrNo == request.PrNo)
                            && (!request.RequesterId.HasValue || request.RequesterId.Value == RequestConstants.AllValue || pr.UserId == request.RequesterId.Value)
                            && pr.PrDetails.Any(detail => detail.Status == StatusConstants.PrRequested)
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
                                       .Include(pr => pr.User)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<Pr>> GetByProjectAsync(long projectId)
        {
            return await _dbContext.Prs.Where(pr => pr.ProjectId == projectId)
                                       .OrderBy(pr => pr.CreateDate)
                                       .Include(pr => pr.PrDetails)
                                       .Include(pr => pr.User)
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
                            && detail.Status == StatusConstants.PrDetailRequested
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
    }
}
