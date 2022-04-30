using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class RentalContractsOverviewViewModel : BaseViewModel
  {
    internal RentalContractsOverviewViewModel()
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
        if (_selectedContract != value && MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedRentalContract = _selectedContract;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
        }
        OnPropertyChanged();
      }
    }
  }
}
