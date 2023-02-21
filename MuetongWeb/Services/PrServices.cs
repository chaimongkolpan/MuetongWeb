using System.Collections.Generic;
using System.Net.NetworkInformation;
using MuetongWeb.Constants;
using MuetongWeb.Helpers;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Pages;
using MuetongWeb.Models.Requests;
using MuetongWeb.Models.Responses;
using MuetongWeb.Repositories.Interfaces;
using MuetongWeb.Services.Interfaces;
namespace MuetongWeb.Services
{
    public class PrServices : IPrServices
    {
        private readonly ILogger<PrServices> _logger;
        private readonly IFileServices _fileServices;
        private readonly IProjectRepositories _projectRepositories;
        private readonly IPrRepositories _prRepositories;
        private readonly IProductRepositories _productRepositories; 
        public PrServices
        (
            ILogger<PrServices> logger,
            IFileServices fileServices,
            IProjectRepositories projectRepositories,
            IPrRepositories prRepositories,
            IProductRepositories productRepositories
        )
        {
            _logger = logger;
            _fileServices = fileServices;
            _projectRepositories = projectRepositories;
            _prRepositories = prRepositories;
            _productRepositories = productRepositories;
        }
        #region Page Service
        public async Task<PrModel> IndexAsync(bool canEdit, UserInfoModel user)
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
                var response = new PrModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => IndexAsync: " + ex.Message);
                return new PrModel();
            }
        }
        public async Task<PrModel> ApproverAsync(bool canEdit, UserInfoModel user)
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
                var response = new PrModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ApproverAsync: " + ex.Message);
                return new PrModel();
            }
        }
        public async Task<PrModel> ReceiveAsync(bool canEdit, UserInfoModel user)
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
                var response = new PrModel(projectResponses, canEdit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ApproverAsync: " + ex.Message);
                return new PrModel();
            }
        }
        #endregion
        #region Api Service
        public async Task<PrIndexResponse> IndexSearchAsync(PrIndexSearchRequest request)
        {
            try
            {
                var prs = await _prRepositories.SearchAsync(request);
                var response = new PrIndexResponse(prs);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("PrServices => IndexSearchAsync: " + ex.Message);
                return new PrIndexResponse();
            }
        }
        public async Task<PrResponse?> Get(long id)
        {
            try
            {
                var pr = await _prRepositories.GetAsync(id);
                if (pr == null)
                    return null;
                var response = new PrResponse(pr);
                var files = await _fileServices.GetFilesAsync(pr.Id, FileConstants.PrPathType);
                if (files.Any()) response.SetFiles(files);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => Get: " + ex.Message);
                return null;
            }
        }
        public async Task<bool> Read(long id)
        {
            try
            {
                return await _prRepositories.Read(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => Read: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> Cancel(long id)
        {
            try
            {
                // add remark
                if (await _prRepositories.Cancel(id))
                {
                    await _prRepositories.UpdateAllDetailStatus(id, StatusConstants.PrCancel);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => Cancel: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> Approve(long id, long userId, PrApproveRequest request)
        {
            try
            {
                if(await _prRepositories.Approve(id, userId))
                {
                    await _prRepositories.UpdateAllDetailStatus(id, StatusConstants.PrDetailRequested);
                    if (request.Files != null && request.Files.Any())
                    {
                        await _fileServices.ImportFileList(id, FileConstants.PrApprovePathType, request.Files);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => Approve: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> IndexAddAsync(long userId, PrIndexAddRequest request)
        {
            try
            {
                Pr pr = new Pr()
                {
                    PrNo = string.IsNullOrWhiteSpace(request.PrNo) ? string.Empty : request.PrNo,
                    ProjectId = request.ProjectId,
                    IsAdvancePay = request.AdvancePay,
                    ContractorId = request.AdvancePay ? request.ContractorId : null,
                    Status = StatusConstants.PrWaitingApprove,
                    UserId = userId,
                    CreateDate = DateTime.Now
                };
                if (await _prRepositories.AddAsync(pr))
                {
                    if (request.Details != null && request.Details.Any())
                    {
                        List<PrDetail> details = new List<PrDetail>();
                        foreach (var detail in request.Details)
                        {
                            var tmp = new PrDetail()
                            {
                                PrId = pr.Id,
                                ProductId = detail.ProductId,
                                ProjectCodeId = detail.ProjectCodeId,
                                Quantity = detail.Quantity.HasValue ? detail.Quantity.Value : 0,
                                UseDate = detail.UseDate,
                                Status = StatusConstants.PrDetailWaitingApprove,
                                Remark = detail.Remark
                            };
                            details.Add(tmp);
                        }
                        await _prRepositories.AddDetailRangeAsync(details);
                    }
                    if (request.Files != null && request.Files.Any())
                    {
                        await _fileServices.ImportFileList(pr.Id, FileConstants.PrPathType, request.Files);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => IndexAddAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> IndexUpdateAsync(long id, long userId, PrIndexUpdateRequest request)
        {
            try
            {
                var pr = await _prRepositories.GetAsync(id);
                if (pr == null)
                    return false;
                pr.PrNo = string.IsNullOrWhiteSpace(request.PrNo) ? string.Empty : request.PrNo;
                pr.ProjectId = request.ProjectId;
                pr.IsAdvancePay = request.AdvancePay;
                pr.ContractorId = request.AdvancePay ? request.ContractorId : null;
                pr.Status = StatusConstants.PrWaitingApprove;
                pr.UserId = userId;
                pr.ModifyDate = DateTime.Now;
                if (await _prRepositories.UpdateAsync(pr))
                {
                    var details = await _prRepositories.GetByPrAsync(id);
                    var addDetails = new List<PrDetail>();
                    var deleteDetails = new List<PrDetail>();
                    if (request.Details != null && request.Details.Any())
                    {
                        foreach (var detail in request.Details)
                        {
                            if(detail.Id == 0)
                            {
                                var tmp = new PrDetail()
                                {
                                    PrId = pr.Id,
                                    ProductId = detail.ProductId,
                                    ProjectCodeId = detail.ProjectCodeId,
                                    Quantity = detail.Quantity.HasValue ? detail.Quantity.Value : 0,
                                    UseDate = detail.UseDate,
                                    Status = StatusConstants.PrDetailWaitingApprove,
                                    Remark = detail.Remark
                                };
                                addDetails.Add(tmp);
                            }
                            else
                            {
                                var updateDetail = details.FirstOrDefault(d => d.Id == detail.Id);
                                if(updateDetail != null)
                                {
                                    updateDetail.ProductId = detail.ProductId;
                                    updateDetail.ProjectCodeId = detail.ProjectCodeId;
                                    updateDetail.Quantity = detail.Quantity.HasValue ? detail.Quantity.Value : 0;
                                    updateDetail.UseDate = detail.UseDate;
                                    updateDetail.Status = StatusConstants.PrDetailWaitingApprove;
                                    updateDetail.Remark = detail.Remark;
                                }
                            }
                        }
                        if (addDetails.Any())
                            await _prRepositories.AddDetailRangeAsync(addDetails);
                        if (details.Any())
                            await _prRepositories.UpdateDetailAsync(details.ToList());
                        foreach (var detail in details)
                        {
                            if (!request.Details.Any(d => d.Id == detail.Id))
                                deleteDetails.Add(detail);
                        }
                        if (deleteDetails.Any())
                            await _prRepositories.DeleteDetailAsync(deleteDetails);
                    }
                    if (request.Files != null && request.Files.Any())
                    {
                        await _fileServices.ImportFileList(pr.Id, FileConstants.PrPathType, request.Files);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => IndexUpdateAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<List<UserResponse>> GetRequesterByProject(long projectId)
        {
            IEnumerable<Pr> prs;
            if (projectId == 0)
                prs = await _prRepositories.GetAsync();
            else
                prs = await _prRepositories.GetByProjectAsync(projectId);
            var users = prs.Select(pr => pr.User).DistinctBy(user => user.Id).OrderBy(user => string.Format("{0} {1}", user.Firstname, user.Lastname)).ToList();
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
            var response = prs.Select(pr => pr.PrNo).Distinct().OrderBy(prno => prno).ToList();
            return response;
        }
        public async Task<List<ContractorResponse>> GetContractorByProject(long projectId)
        {
            IEnumerable<ProjectContractor> projectContractors = await _projectRepositories.GetContractorAsync(projectId);
            var contractors = projectContractors.Select(project => project.Contractor).DistinctBy(contractor => contractor.Id).ToList();
            var response = new List<ContractorResponse>();
            foreach (var contractor in contractors)
            {
                response.Add(new ContractorResponse(contractor));
            }
            return response;
        }
        public async Task<List<ProjectCodeResponse>> GetProjectCode(long projectId)
        {
            IEnumerable<ProjectCode> codes = await _projectRepositories.GetCodeAsync(projectId);
            var response = new List<ProjectCodeResponse>();
            foreach (var code in codes)
            {
                response.Add(new ProjectCodeResponse(code));
            }
            return response;
        }
        public async Task<List<ProductResponse>> GetProduct()
        {
            IEnumerable<Product> products = await _productRepositories.GetAsync();
            var response = new List<ProductResponse>();
            foreach (var product in products)
            {
                if(product.Id != 0)
                    response.Add(new ProductResponse(product));
            }
            return response;
        }
        public async Task<FileModalResponse> GetFiles(long id, string type)
        {
            try
            {
                var files = await _fileServices.GetFilesAsync(id, type);
                if(files.Any())
                {
                    return new FileModalResponse(files);
                }
                return new FileModalResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => GetFiles: " + ex.Message);
                return new FileModalResponse();
            }
        }
        #endregion
        #region Receive
        public async Task<PrReceiveResponse> ReceiveSearchAsync(PrReceiveSearchRequest request)
        {
            try
            {
                var prs = await _prRepositories.SearchAsync(request);
                var ids = prs.SelectMany(pr => pr.PrDetails.Select(detail => detail.Id)).ToList();
                var files = await _fileServices.GetFilesListAsync(ids, FileConstants.PrReceivePathType);
                var response = new PrReceiveResponse(prs, files);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ReceiveSearchAsync: " + ex.Message);
                return new PrReceiveResponse();
            }
        }
        public async Task<bool> ReceiveListAsync(PrReceiveAddRequest request)
        {
            try
            {
                if (request == null)
                    return false;
                if (request.Details.Any())
                {
                    List<long> ids = new List<long>();
                    List<PrReceive> prReceives = new List<PrReceive>();
                    var now = DateTime.Now;
                    foreach(var detail in request.Details)
                    {
                        var tmp = new PrReceive()
                        {
                            PrDetailId = detail.DetailId,
                            Quantity = detail.Quantity,
                            Remark = detail.Remark,
                            UserId = request.User.Id,
                            CreateDate = detail.CreateDate.HasValue ? detail.CreateDate.Value : now
                        };
                        ids.Add(detail.DetailId);
                        prReceives.Add(tmp);
                    }
                    await _prRepositories.AddReceiveRangeAsync(prReceives);
                    // save file receive coll
                    await _prRepositories.CheckReceive(ids);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ReceiveListAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> ReceiveAsync(PrReceiveDetailRequest request)
        {
            try
            {
                if (request == null)
                    return false;
                var tmp = new PrReceive()
                {
                    PrDetailId = request.DetailId,
                    Quantity = request.Quantity,
                    Remark = request.Remark,
                    UserId = request.User.Id,
                    CreateDate = request.CreateDate.HasValue ? request.CreateDate.Value : DateTime.Now
                };
                await _prRepositories.AddReceiveAsync(tmp);
                if (request.Files != null && request.Files.Any())
                {
                    await _fileServices.ImportFileList(request.DetailId, FileConstants.PrReceivePathType, request.Files);
                }
                //await _prRepositories.CheckReceive(new List<long>() { request.DetailId });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ReceiveAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateReceiveAsync(long receiveId, PrReceiveDetailRequest request)
        {
            try
            {
                if (request == null)
                    return false;
                var tmp = await _prRepositories.GetReceiveAsync(receiveId);
                if (tmp == null)
                {
                    await _prRepositories.AddReceiveAsync(new PrReceive()
                    {
                        PrDetailId = request.DetailId,
                        Quantity = request.Quantity,
                        Remark = request.Remark,
                        UserId = request.User.Id,
                        CreateDate = request.CreateDate.HasValue ? request.CreateDate.Value : DateTime.Now
                    });
                }
                else
                {
                    tmp.Quantity= request.Quantity;
                    tmp.Remark= request.Remark;
                    tmp.UserId = request.User.Id;
                    tmp.CreateDate = request.CreateDate.HasValue ? request.CreateDate.Value : DateTime.Now;
                    await _prRepositories.UpdateReceiveAsync(tmp);
                }
                if (request.Files != null && request.Files.Any())
                {
                    await _fileServices.ImportFileList(request.DetailId, FileConstants.PrReceivePathType, request.Files);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => UpdateReceiveAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> ApproveReceiveAsync(long detailId)
        {
            try
            {
                await _prRepositories.CheckReceive(new List<long>() { detailId });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => ApproveReceiveAsync: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> DisapproveReceiveAsync(long detailId)
        {
            try
            {
                await _prRepositories.DisapproveReceive(new List<long>() { detailId });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("PrServices => DisapproveReceiveAsync: " + ex.Message);
                return false;
            }
        }
        #endregion
    }
}
