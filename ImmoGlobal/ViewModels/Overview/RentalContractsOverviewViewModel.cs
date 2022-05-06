using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class RentalContractsOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      RentalContractCollection = new(DbController.GetAllRentalContractsDB());
      OnPropertyChanged(nameof(RentalContractCollection));
    }
    public ObservableCollection<RentalContract> RentalContractCollection { get; set; }

    private RentalContract? _selectedContract;
    public RentalContract? SelectedContract
    {
      get => _selectedContract;
      set
      {
        if (_selectedContract != value && MainWindowViewModelInstance != null)
        {
          _selectedContract = value;
          MainWindowViewModelInstance.SelectedRentalContract = _selectedContract;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
        }
        OnPropertyChanged();
      }
    }

  }
}
