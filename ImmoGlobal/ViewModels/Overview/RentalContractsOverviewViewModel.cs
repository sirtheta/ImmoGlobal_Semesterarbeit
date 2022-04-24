using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class RentalContractsOverviewViewModel : BaseViewModel
  {
    public RentalContractsOverviewViewModel()
    {
      RentalContractCollection = new ObservableCollection<RentalContract>(DbController.GetAllRentalContractsDB());
      MainWindowViewModelInstance = MainWindowViewModel.GetInstance;
    }

    private MainWindowViewModel? MainWindowViewModelInstance { get; set; }
    public ObservableCollection<RentalContract> RentalContractCollection { get; set; }

    private RentalContract? _selectedContract;
    public RentalContract? SelectedContract
    {
      get => _selectedContract;
      set
      {
        _selectedContract = value;
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedRentalContract = _selectedContract;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
        }
        OnPropertyChanged();
      }
    }
  }
}
