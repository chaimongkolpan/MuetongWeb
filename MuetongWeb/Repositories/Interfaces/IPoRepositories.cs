﻿using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPoRepositories
    {
        Task<IEnumerable<Po>> GetAsync();
        Task<Po?> GetAsync(long id);
        Task<Po?> FindAsync(long id);
        Task<IEnumerable<Po>> SearchAsync(PoIndexSearchRequest request, List<long> detailIds);
        Task<IEnumerable<Po>> SearchBillingAsync(BillingIndexPoSearch request);
        Task<IEnumerable<Po>> GetByProjectAsync(long projectId);
        Task<bool> AddAsync(Po po);
        Task<bool> UpdateAsync(Po po);
        Task<bool> AddDetailAsync(PoDetail detail);
        Task<bool> AddDetailRangeAsync(List<PoDetail> details);
        Task<bool> Read(long id);
        Task<bool> Cancel(long id, string? remark);
        Task<bool> Approve(long id, long userId);
    }
}
