using FinanceManager.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface IStatisticsService
    {
        Task<MonthlyStatistics> GetMonthlyStatisticsAsync(int userId, int year, int month, string currency = null);
        Task<IEnumerable<DailyStatistics>> GetDailyStatisticsAsync(int userId, int year, int month, string currency = null);
        Task<IEnumerable<MonthlyStatistics>> GetYearlyStatisticsAsync(int userId, int year, string currency = null);
        Task<IEnumerable<CategoryStatistics>> GetCategoryStatisticsAsync(int userId, int type, DateTime startDate, DateTime endDate, string currency = null);
        Task<IEnumerable<TrendData>> GetTrendDataAsync(int userId, DateTime startDate, DateTime endDate, string currency = null);
    }
}
