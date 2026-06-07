using FinanceManager.Common.Helpers;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.models;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Services
{
    /// <summary>统计服务实现 —— 提供月度/日度/年度/分类/趋势多维度的收支统计查询</summary>
    public class StatisticsService : IStatisticsService
    {
        private readonly IRecordRepository _recordRepo;
        private readonly ICategoryRepository _categoryRepo;

        public StatisticsService(IRecordRepository recordRepo, ICategoryRepository categoryRepo)
        {
            _recordRepo = recordRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<MonthlyStatistics> GetMonthlyStatisticsAsync(int userId, int year, int month, string currency = null)
        {
            var start = DateHelper.StartOfMonth(year, month);
            var end = DateHelper.EndOfMonth(year, month);
            var totalIncome = await _recordRepo.GetSumByTypeAndDateRangeAsync(
                userId, (int)RecordType.Income, start, end, currency);
            var totalExpense = await _recordRepo.GetSumByTypeAndDateRangeAsync(
                userId, (int)RecordType.Expense, start, end, currency);
            return new MonthlyStatistics
            {
                Year = year,
                Month = month,
                TotalIncome = totalIncome,
                TotalExpense = Math.Abs(totalExpense)
            };
        }

        public async Task<IEnumerable<DailyStatistics>> GetDailyStatisticsAsync(
            int userId, int year, int month, string currency = null)
        {
            var start = DateHelper.StartOfMonth(year, month);
            var end = DateHelper.EndOfMonth(year, month);
            var records = await _recordRepo.GetByDateRangeAsync(userId, start, end, currency);
            return records
                .GroupBy(r => r.Date.Date)
                .Select(g => new DailyStatistics
                {
                    Date = g.Key,
                    Income = g.Where(r => r.Type == RecordType.Income).Sum(r => r.Amount),
                    Expense = Math.Abs(g.Where(r => r.Type == RecordType.Expense).Sum(r => r.Amount))
                })
                .OrderBy(d => d.Date);
        }

        public async Task<IEnumerable<MonthlyStatistics>> GetYearlyStatisticsAsync(
            int userId, int year, string currency = null)
        {
            var results = new List<MonthlyStatistics>();
            for (int month = 1; month <= 12; month++)
            {
                results.Add(await GetMonthlyStatisticsAsync(userId, year, month, currency));
            }
            return results;
        }

        public async Task<IEnumerable<CategoryStatistics>> GetCategoryStatisticsAsync(
            int userId, int type, DateTime startDate, DateTime endDate, string currency = null)
        {
            var categories = await _categoryRepo.GetByTypeAsync(userId, type);
            var totalAmount = await _recordRepo.GetSumByTypeAndDateRangeAsync(
                userId, type, startDate, endDate, currency);
            var absTotal = Math.Abs(totalAmount);

            var results = new List<CategoryStatistics>();
            foreach (var cat in categories)
            {
                var amount = await _recordRepo.GetSumByCategoryAndTypeAndDateRangeAsync(
                    userId, cat.Id, type, startDate, endDate, currency);
                results.Add(new CategoryStatistics
                {
                    CategoryId = cat.Id,
                    CategoryName = cat.Name,
                    CategoryIcon = cat.Icon,
                    CategoryColor = cat.Color,
                    Amount = Math.Abs(amount),
                    Percentage = absTotal > 0 ? Math.Abs(amount) / absTotal * 100 : 0
                });
            }
            return results.OrderByDescending(r => r.Amount);
        }

        public async Task<IEnumerable<TrendData>> GetTrendDataAsync(
            int userId, DateTime startDate, DateTime endDate, string currency = null)
        {
            var records = await _recordRepo.GetByDateRangeAsync(userId, startDate, endDate, currency);
            return records
                .GroupBy(r => r.Date.Date)
                .Select(g => new TrendData
                {
                    Date = g.Key,
                    Label = g.Key.ToString("MM/dd"),
                    Income = g.Where(r => r.Type == RecordType.Income).Sum(r => r.Amount),
                    Expense = Math.Abs(g.Where(r => r.Type == RecordType.Expense).Sum(r => r.Amount))
                })
                .OrderBy(d => d.Date);
        }
    }
}
