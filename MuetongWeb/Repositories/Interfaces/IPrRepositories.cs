using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;

namespace MuetongWeb.Repositories.Interfaces
{
    public interface IPrRepositories
    {
        Task<IEnumerable<Pr>> SearchAsync(PrIndexSearchRequest request);
        Task<IEnumerable<Pr>> SearchAsync(PrReceiveSearchRequest request);
        Task<IEnumerable<Pr>> SearchAsync(PoIndexSearchRequest request);
        Task<List<long>> SearchBillAsync(BillingIndexSearch request);
        Task<IEnumerable<PrDetail>> SearchDetailAsync(PoIndexSearchRequest request);
        Task<Pr?> GetAsync(long id);
        Task<IEnumerable<Pr>> GetAsync();
        Task<IEnumerable<Pr>> GetByProjectAsync(long projectId);
        Task<IEnumerable<PrDetail>> GetByPrAsync(long prId);
        Task<IEnumerable<PrDetail>> GetByIdsAsync(List<long> detailId);
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
        Task<bool> Disapprove(long id);

        Task<bool> UpdateAllDetailStatus(long prId, string status);
        Task<bool> UpdateAllDetailStatus(List<long> ids, string status);
        Task<bool> UpdateAllDetailStatusByPoDetail(List<long> ids, string status);
        Task<bool> UpdateDetailStatus(long id, string status);
        Task<IEnumerable<PrDetail>> SearchAsync(PoIndexPrSearch request);

        Task<bool> AddReceiveAsync(PrReceive receive);
        Task<bool> AddReceiveRangeAsync(List<PrReceive> receives);
        Task<bool> CheckReceive(List<long> detailId);
        Task<bool> DisapproveReceive(List<long> detailId);
        Task<PrReceive?> GetReceiveAsync(long id);
        Task<bool> UpdateReceiveAsync(PrReceive receive);
    }
}
