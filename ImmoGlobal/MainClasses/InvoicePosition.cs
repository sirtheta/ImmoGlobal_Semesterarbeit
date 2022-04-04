using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class InvoicePosition
  {
    public int InvoicePositionId { get; set; }
    public Property? Property { get; set; }
    public PropertyObject? PropertyObject { get; set; }
    public double Value { get; set; }
    public EAdditionalCosts? AdditionalCostsCategory { get; set; }
    public Account? Account { get; set; }
  }
}
