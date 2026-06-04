using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    public class ConsumptionAnalysis
    {
        public decimal TotalExpense { get; set; }
        public List<CategoryStatistics> CategoryBreakdown { get; set; } = new List<CategoryStatistics>();
        public string TopCategoryName { get; set; } = string.Empty;
        public decimal TopCategoryPercentage { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
