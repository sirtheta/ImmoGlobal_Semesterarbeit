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

    private RentalContract? _selectedContract;
    public RentalContract? SelectedContract
    {
      get => _selectedContract;
      set
      {
        _selectedContract = value;
        if (MainWindowViewModel.GetInstance != null)
        {
          MainWindowViewModel.GetInstance.SelectedRentalContract = _selectedContract;
        }
        OnPropertyChanged();
      }
    }
  }
}
