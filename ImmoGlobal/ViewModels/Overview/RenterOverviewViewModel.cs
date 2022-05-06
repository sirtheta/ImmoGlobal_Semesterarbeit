using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class RenterOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      RenterCollection = new(DbController.GetAllRentersDB());
      OnPropertyChanged(nameof(RenterCollection));
    }

    internal RenterOverviewViewModel()
    {
      _selectedRenterDetailsViewModel = null;
    }

    private ObservableCollection<Invoice>? _invoiceCollection;
    private ObservableCollection<RentalContract>? _rentalContractCollection;
    private Persona? _selectedRenter;
    private RenterDetailsViewModel? _selectedRenterDetailsViewModel;


    public ObservableCollection<Persona> RenterCollection { get; set; }
    public ObservableCollection<RentalContract>? RentalContractCollection
    {
      get => _rentalContractCollection;
      set
      {
        _rentalContractCollection = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Invoice>? InvoiceCollection
    {
      get => _invoiceCollection;
      set
      {
        _invoiceCollection = value;
        OnPropertyChanged();
      }
    }

    public Persona? SelectedRenter
    {
      get => _selectedRenter;
      set
      {
        if (_selectedRenter != value && MainWindowViewModelInstance != null)
        {
          _selectedRenter = value;
          SelectedRenterDetailsViewModel = new RenterDetailsViewModel(_selectedRenter);
          InvoiceCollection = new ObservableCollection<Invoice>(DbController.GetInvoiceToPersonaDB(_selectedRenter));
          RentalContractCollection = new ObservableCollection<RentalContract>(DbController.GetRentalContractsToPersonDB(_selectedRenter));
          MainWindowViewModelInstance.SelectedPersona = _selectedRenter;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
        }
        else
        {
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
        }
        OnPropertyChanged();
      }
    }
    public RenterDetailsViewModel? SelectedRenterDetailsViewModel
    {
      get => _selectedRenterDetailsViewModel;
      set
      {
        _selectedRenterDetailsViewModel = value;
        OnPropertyChanged();
      }
    }
  }
}
