﻿using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPrRepositories
    {
        Task<IEnumerable<Pr>> SearchAsync(PrIndexSearchRequest request);
        Task<IEnumerable<Pr>> SearchAsync(PoIndexSearchRequest request);
        Task<Pr?> GetAsync(long id);
        Task<IEnumerable<Pr>> GetAsync();
        Task<IEnumerable<Pr>> GetByProjectAsync(long projectId);
        Task<IEnumerable<PrDetail>> GetByPrAsync(long prId);
        Task<bool> AddAsync(Pr pr);
        Task<bool> AddDetailAsync(PrDetail detail);
        Task<bool> AddDetailRangeAsync(List<PrDetail> details);
        Task<bool> UpdateAsync(Pr pr);
        Task<bool> UpdateDetailAsync(PrDetail detail);
        Task<bool> UpdateDetailAsync(List<PrDetail> details);
        Task<bool> DeleteDetailAsync(List<PrDetail> details);
        Task<bool> Read(long id);
        Task<bool> Cancel(long id);
        Task<bool> Approve(long id, long userId);

        Task<bool> UpdateAllDetailStatus(long prId, string status);
        Task<bool> UpdateDetailStatus(long id, string status);
    }
}
