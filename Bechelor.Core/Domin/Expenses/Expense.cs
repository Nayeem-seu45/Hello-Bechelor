using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Expenses
{
    public class Expense : BaseEntity
    {
        public string ExpenseName { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public double TotalAmount { get; set; }
        public string Comment { get; set; }
        public string ExpenseBy { get; set; }
        public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public List<BazarDetail> BazarDetails { get; set; } = new();

    }
}
