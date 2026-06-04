using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    /// <summary>二期实现：多时间段对比分析结果</summary>
    public class PeriodComparisonResult
    {
        public DateTime Period1Start { get; set; }
        public DateTime Period1End { get; set; }
        public DateTime Period2Start { get; set; }
        public DateTime Period2End { get; set; }
        public decimal Period1TotalExpense { get; set; }
        public decimal Period2TotalExpense { get; set; }
        public decimal ExpenseChange => Period2TotalExpense - Period1TotalExpense;
        public decimal ExpenseChangePercentage { get; set; }
        public List<CategoryStatistics> Period1Breakdown { get; set; } = new List<CategoryStatistics>();
        public List<CategoryStatistics> Period2Breakdown { get; set; } = new List<CategoryStatistics>();
    }
}
