using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public string ExpenseDateString { get; set; }
        public double TotalAmount { get; set; }
        public string Comment { get; set; }
        public string ExpenseBy { get; set; }
        public string IsEdit { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BazarDetailViewModel> BazarItemDetails { get; set; } = new();
        //public IEnumerable<ExpenseCategoryItemModel> expenseCategoryItemModels { get; set; }
    }

    public class BazarDetailViewModel 
    {
        public int Id { get; set; }
        public string BazarItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
    }
}
