using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Entities
{
    public class RecordEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "CNY";
        public RecordType Type { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
