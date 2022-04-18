using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertInvoiceViewModel : BaseViewModel
  {
    public UpsertInvoiceViewModel()
    {
    }

    public UpsertInvoiceViewModel(object selectedInvoice)
    {
      SelectedInvoice = selectedInvoice;
    }

    public object SelectedInvoice { get; }
  }
}
