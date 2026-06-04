using FinanceManager.Common.Constants;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Services
{
    /// <summary>模板服务实现 —— 处理记账模板的增删改查、收藏筛选和使用计数</summary>
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _repo;

        public TemplateService(ITemplateRepository repo) => _repo = repo;

        public async Task<IEnumerable<TemplateEntity>> GetTemplatesAsync(int userId)
        {
            return await _repo.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<TemplateEntity>> GetFavoriteTemplatesAsync(int userId)
        {
            return await _repo.GetFavoriteByUserIdAsync(userId);
        }

        public async Task<TemplateEntity> GetTemplateByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> AddTemplateAsync(TemplateEntity template)
        {
            var count = await _repo.GetCountByUserIdAsync(template.UserId);
            if (count >= AppConstants.MaxTemplates)
                throw new InvalidOperationException($"模板数量已达上限（{AppConstants.MaxTemplates}个）");

            template.UseCount = 0;
            template.CreatedAt = DateTime.Now;
            template.UpdatedAt = template.CreatedAt;
            return await _repo.InsertAsync(template);
        }

        public async Task UpdateTemplateAsync(TemplateEntity template)
        {
            template.UpdatedAt = DateTime.Now;
            await _repo.UpdateAsync(template);
        }

        public async Task DeleteTemplateAsync(int id) => await _repo.DeleteAsync(id);

        public async Task IncrementUseCountAsync(int id) =>
            await _repo.IncrementUseCountAsync(id);

        public async Task<int> GetTemplateCountAsync(int userId) =>
            await _repo.GetCountByUserIdAsync(userId);
    }
}
