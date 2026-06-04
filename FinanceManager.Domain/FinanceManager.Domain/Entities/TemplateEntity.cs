using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Entities
{
    public class TemplateEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal DefaultAmount { get; set; }
        public string Currency { get; set; } = "CNY";
        public RecordType Type { get; set; }
        public int CategoryId { get; set; }
        public string NoteTemplate { get; set; }
        public bool IsFavorite { get; set; }
        public int UseCount { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
