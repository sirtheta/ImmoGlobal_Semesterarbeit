using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class RentalContractsOverviewViewModel : BaseViewModel
  {
    public RentalContractsOverviewViewModel()
    {
      RentalContractCollection = new ObservableCollection<RentalContract>(DbController.GetAllRentalContractsDB());
    }

    public ObservableCollection<RentalContract> RentalContractCollection { get; set; }
  }
}
