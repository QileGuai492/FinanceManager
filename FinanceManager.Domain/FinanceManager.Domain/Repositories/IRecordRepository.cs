using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface IRecordRepository
    {
        Task<IEnumerable<RecordEntity>> GetByUserIdAsync(int userId);
        Task<IEnumerable<RecordEntity>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate, string currency = null);
        Task<RecordEntity> GetByIdAsync(int id);
        Task<int> InsertAsync(RecordEntity entity);
        Task UpdateAsync(RecordEntity entity);
        Task DeleteAsync(int id);
        Task<decimal> GetSumByTypeAndDateRangeAsync(int userId, int type, DateTime startDate, DateTime endDate, string currency = null);
        Task<decimal> GetSumByCategoryAndTypeAndDateRangeAsync(int userId, int categoryId, int type, DateTime startDate, DateTime endDate, string currency = null);
    }
}
