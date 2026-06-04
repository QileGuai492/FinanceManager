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
    /// <summary>分类服务实现 —— 处理收支分类的加载、新增、删除和自定义分类计数</summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesAsync(int userId)
        {
            return await _repo.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesByTypeAsync(int userId, int type)
        {
            return await _repo.GetByTypeAsync(userId, type);
        }

        public async Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> AddCategoryAsync(CategoryEntity category)
        {
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = category.CreatedAt;
            category.IsDefault = false;
            return await _repo.InsertAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            category.UpdatedAt = DateTime.Now;
            await _repo.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<int> GetCustomCategoryCountAsync(int userId)
        {
            return await _repo.GetCustomCountByUserIdAsync(userId);
        }
    }
}
