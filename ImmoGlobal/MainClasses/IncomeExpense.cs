using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class IncomeExpense
  {

    public int IncomeExpenseId { get; set; }
    public Account Account { get; set; }
    public string Description { get; set; }
    public int ReceiptNumber { get; set; }
    public double? IncomeAmount { get; set; }
    public double? ExpenseAmount { get; set; }
    public DateTime Date { get; set; }
  }
}
