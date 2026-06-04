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
    /// <summary>预算服务实现 —— 处理月预算和分类预算的增删改查</summary>
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepo;
        private readonly ICategoryBudgetRepository _catBudgetRepo;

        public BudgetService(IBudgetRepository budgetRepo, ICategoryBudgetRepository catBudgetRepo)
        {
            _budgetRepo = budgetRepo;
            _catBudgetRepo = catBudgetRepo;
        }

        public async Task<BudgetEntity> GetBudgetByYearMonthAsync(int userId, int year, int month)
        {
            return await _budgetRepo.GetByYearMonthAsync(userId, year, month);
        }

        public async Task<IEnumerable<BudgetEntity>> GetBudgetsAsync(int userId)
        {
            return await _budgetRepo.GetByUserIdAsync(userId);
        }

        public async Task<int> AddBudgetAsync(BudgetEntity budget)
        {
            budget.CreatedAt = DateTime.Now;
            budget.UpdatedAt = budget.CreatedAt;
            return await _budgetRepo.InsertAsync(budget);
        }

        public async Task UpdateBudgetAsync(BudgetEntity budget)
        {
            budget.UpdatedAt = DateTime.Now;
            await _budgetRepo.UpdateAsync(budget);
        }

        public async Task DeleteBudgetAsync(int id) => await _budgetRepo.DeleteAsync(id);

        public async Task<CategoryBudgetEntity> GetCategoryBudgetAsync(
            int userId, int categoryId, int year, int month)
        {
            return await _catBudgetRepo.GetByCategoryAsync(userId, categoryId, year, month);
        }

        public async Task<IEnumerable<CategoryBudgetEntity>> GetCategoryBudgetsAsync(
            int userId, int year, int month)
        {
            return await _catBudgetRepo.GetByYearMonthAsync(userId, year, month);
        }

        public async Task<int> AddCategoryBudgetAsync(CategoryBudgetEntity cb)
        {
            cb.CreatedAt = DateTime.Now;
            cb.UpdatedAt = cb.CreatedAt;
            return await _catBudgetRepo.InsertAsync(cb);
        }

        public async Task UpdateCategoryBudgetAsync(CategoryBudgetEntity cb)
        {
            cb.UpdatedAt = DateTime.Now;
            await _catBudgetRepo.UpdateAsync(cb);
        }

        public async Task DeleteCategoryBudgetAsync(int id) =>
            await _catBudgetRepo.DeleteAsync(id);
    }
}
