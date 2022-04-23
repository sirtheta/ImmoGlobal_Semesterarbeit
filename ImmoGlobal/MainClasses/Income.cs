using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Income
  {  
    public int IncomeId { get; set; }
    public Account Account { get; set; }
    public double IncomeAmount { get; set; }
  }
}
