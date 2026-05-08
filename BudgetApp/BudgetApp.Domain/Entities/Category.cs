using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AllocatedAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Budget? Budget { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
