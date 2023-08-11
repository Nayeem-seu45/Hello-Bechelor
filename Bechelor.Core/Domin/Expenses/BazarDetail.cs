using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Expenses
{
    public class BazarDetail : BaseEntity
    {
        public int ExpenseId { get; set; }
        public int BazarItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
    }
}
