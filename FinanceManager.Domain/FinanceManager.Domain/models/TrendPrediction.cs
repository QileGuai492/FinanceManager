using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    public class TrendPrediction
    {
        public string TrendDirection { get; set; } = string.Empty;  // "上升" / "下降" / "平稳"
        public decimal TrendSlope { get; set; }
        public List<TrendData> HistoricalData { get; set; } = new List<TrendData>();
        public string PredictionSummary { get; set; } = string.Empty;
    }
}
