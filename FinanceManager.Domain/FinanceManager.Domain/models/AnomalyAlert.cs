using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.models
{
    public class AnomalyAlert
    {
        public int RecordId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal AverageAmount { get; set; }
        public string AlertMessage { get; set; } = string.Empty;
    }
}
