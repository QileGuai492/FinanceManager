using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface IBudgetRepository
    {
        Task<BudgetEntity> GetByYearMonthAsync(int userId, int year, int month);
        Task<IEnumerable<BudgetEntity>> GetByUserIdAsync(int userId);
        Task<int> InsertAsync(BudgetEntity entity);
        Task UpdateAsync(BudgetEntity entity);
        Task DeleteAsync(int id);
    }
}
