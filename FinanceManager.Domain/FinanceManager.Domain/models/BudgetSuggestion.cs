using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    public class BudgetSuggestion
    {
        public decimal RecommendedTotalBudget { get; set; }
        public List<CategoryBudgetSuggestion> CategoryBudgets { get; set; } = new List<CategoryBudgetSuggestion>();
        public string SuggestionReason { get; set; } = string.Empty;
    }

    public class CategoryBudgetSuggestion
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal SuggestedAmount { get; set; }
        public decimal AverageMonthlyExpense { get; set; }
    }
}
