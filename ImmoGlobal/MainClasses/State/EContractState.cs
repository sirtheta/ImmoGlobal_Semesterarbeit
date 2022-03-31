using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses.State
{
  /// <summary>
  /// represents the rentalContract state
  /// </summary>
  internal enum EContractState
  {
    NotValid,
    Singend,
    Valid,
    Canceled
  }
}
