using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface IBudgetService
    {
        Task<BudgetEntity> GetBudgetByYearMonthAsync(int userId, int year, int month);
        Task<IEnumerable<BudgetEntity>> GetBudgetsAsync(int userId);
        Task<int> AddBudgetAsync(BudgetEntity budget);
        Task UpdateBudgetAsync(BudgetEntity budget);
        Task DeleteBudgetAsync(int id);

        Task<CategoryBudgetEntity> GetCategoryBudgetAsync(int userId, int categoryId, int year, int month);
        Task<IEnumerable<CategoryBudgetEntity>> GetCategoryBudgetsAsync(int userId, int year, int month);
        Task<int> AddCategoryBudgetAsync(CategoryBudgetEntity categoryBudget);
        Task UpdateCategoryBudgetAsync(CategoryBudgetEntity categoryBudget);
        Task DeleteCategoryBudgetAsync(int id);
    }
}
