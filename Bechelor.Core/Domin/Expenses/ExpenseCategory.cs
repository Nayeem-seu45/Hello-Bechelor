using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Expenses
{
    public partial class ExpenseCategory : BaseEntity
    {
        public ExpenseCategory()
        {
            Expenses = new HashSet<Expense>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseType { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
