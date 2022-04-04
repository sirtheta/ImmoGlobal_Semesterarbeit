using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses.Enum
{
  /// <summary>
  /// represents the state of the Invoice
  /// </summary>
  internal enum EInvoiceState
  {
    NotReleased,
    Released,
    OverDue,
    Paid,
    Canceled
  }
}
