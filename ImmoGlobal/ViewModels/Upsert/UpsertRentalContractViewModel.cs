using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertRentalContractViewModel : BaseViewModel
  {
    public UpsertRentalContractViewModel()
    {
    }

    public UpsertRentalContractViewModel(object selectedRentalContract)
    {
      SelectedRentalContract = selectedRentalContract;
    }

    public object SelectedRentalContract { get; }
  }
}
