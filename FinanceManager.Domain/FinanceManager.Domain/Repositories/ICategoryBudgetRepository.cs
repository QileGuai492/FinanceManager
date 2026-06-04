using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface ICategoryBudgetRepository
    {
        Task<CategoryBudgetEntity> GetByCategoryAsync(int userId, int categoryId, int year, int month);
        Task<IEnumerable<CategoryBudgetEntity>> GetByYearMonthAsync(int userId, int year, int month);
        Task<int> InsertAsync(CategoryBudgetEntity entity);
        Task UpdateAsync(CategoryBudgetEntity entity);
        Task DeleteAsync(int id);
    }
}
