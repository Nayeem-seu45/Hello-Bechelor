using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.Models
{
    public class UserWiseBillResponse
    {
        public int UserId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public decimal Amount { get; set; }
    }
}
