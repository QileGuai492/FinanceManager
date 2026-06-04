using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryEntity>> GetCategoriesAsync(int userId);
        Task<IEnumerable<CategoryEntity>> GetCategoriesByTypeAsync(int userId, int type);
        Task<CategoryEntity> GetCategoryByIdAsync(int id);
        Task<int> AddCategoryAsync(CategoryEntity category);
        Task UpdateCategoryAsync(CategoryEntity category);
        Task DeleteCategoryAsync(int id);
        Task<int> GetCustomCategoryCountAsync(int userId);
    }
}
