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
    /// <summary>记账服务实现 —— 处理记账记录的增删改查和按类型/分类汇总</summary>
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<IEnumerable<RecordEntity>> GetRecordsAsync(int userId)
        {
            return await _recordRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<RecordEntity>> GetRecordsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _recordRepository.GetByDateRangeAsync(userId, startDate, endDate);
        }

        public async Task<RecordEntity> GetRecordByIdAsync(int id)
        {
            return await _recordRepository.GetByIdAsync(id);
        }

        public async Task<int> AddRecordAsync(RecordEntity record)
        {
            record.CreatedAt = DateTime.Now;
            record.UpdatedAt = record.CreatedAt;
            return await _recordRepository.InsertAsync(record);
        }

        public async Task UpdateRecordAsync(RecordEntity record)
        {
            record.UpdatedAt = DateTime.Now;
            await _recordRepository.UpdateAsync(record);
        }

        public async Task DeleteRecordAsync(int id)
        {
            await _recordRepository.DeleteAsync(id);
        }

        public async Task<decimal> GetTotalAmountByTypeAsync(int userId, int type, DateTime startDate, DateTime endDate)
        {
            return await _recordRepository.GetSumByTypeAndDateRangeAsync(userId, type, startDate, endDate);
        }

        public async Task<decimal> GetTotalAmountByCategoryAsync(int userId, int categoryId, int type, DateTime startDate, DateTime endDate)
        {
            return await _recordRepository.GetSumByCategoryAndTypeAndDateRangeAsync(userId, categoryId, type, startDate, endDate);
        }
    }
}
