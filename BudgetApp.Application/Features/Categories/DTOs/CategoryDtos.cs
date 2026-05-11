using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Application.Features.Categories.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AllocatedAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
