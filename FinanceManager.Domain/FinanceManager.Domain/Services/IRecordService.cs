using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface IRecordService
    {
        Task<IEnumerable<RecordEntity>> GetRecordsAsync(int userId);
        Task<IEnumerable<RecordEntity>> GetRecordsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<RecordEntity> GetRecordByIdAsync(int id);
        Task<int> AddRecordAsync(RecordEntity record);
        Task UpdateRecordAsync(RecordEntity record);
        Task DeleteRecordAsync(int id);
        Task<decimal> GetTotalAmountByTypeAsync(int userId, int type, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalAmountByCategoryAsync(int userId, int categoryId, int type, DateTime startDate, DateTime endDate);
    }
}
