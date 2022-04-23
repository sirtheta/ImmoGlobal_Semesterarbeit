using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Expense
  {

    public int ExpenseId { get; set; }
    public Account Account { get; set; }
    public double ExpenseAmount { get; set; }
  }
}
