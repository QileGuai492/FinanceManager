using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    public class AiSuggestions
    {
        public List<string> Suggestions { get; set; } = new List<string>();
        public decimal PotentialSavings { get; set; }
        public ConsumptionAnalysis ConsumptionAnalysis { get; set; }
        public List<AnomalyAlert> AnomalyAlerts { get; set; } = new List<AnomalyAlert>();
        public TrendPrediction TrendPrediction { get; set; }
        public BudgetSuggestion BudgetSuggestion { get; set; }
    }
}
