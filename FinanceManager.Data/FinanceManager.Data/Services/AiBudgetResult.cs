using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Services
{
    public class AiBudgetResult
    {
        public decimal TotalBudget { get; set; }
        public List<AiCategoryBudget> Categories { get; set; } = new List<AiCategoryBudget>();
        public string Analysis { get; set; } = string.Empty;     // 自然语言分析
        public string Warning { get; set; } = string.Empty;      // 异常提醒
        public bool Success { get; set; }
        public string Error { get; set; } = string.Empty;
    }

    public class AiCategoryBudget
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
