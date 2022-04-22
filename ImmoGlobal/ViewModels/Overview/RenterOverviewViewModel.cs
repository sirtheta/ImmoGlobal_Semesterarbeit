using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class RenterOverviewViewModel : BaseViewModel
  {
    private ObservableCollection<Persona> _renterCollection;
    private ObservableCollection<Invoice>? _invoiceCollection;
    private ObservableCollection<RentalContract>? _rentalContractCollection;
    private Persona? _selectedRenter;
    private RenterDetailsViewModel? _selectedRenterDetailsViewModel;

    public RenterOverviewViewModel()
    {
      _renterCollection = new ObservableCollection<Persona>(DbController.GetAllRentersDB());
      _selectedRenterDetailsViewModel = null;
    }

    public ObservableCollection<Persona> RenterCollection
    {
      get => _renterCollection;
      set
      {
        _renterCollection = value;
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

    public ObservableCollection<RentalContract>? RentalContractCollection
    {
      get => _rentalContractCollection;
      set
      {
        _rentalContractCollection = value;
        OnPropertyChanged();
      }
    }

    public Persona? SelectedRenter
    {
      get => _selectedRenter;
      set
      {
        if (_selectedRenter != value)
        {
          _selectedRenter = value;
          SelectedRenterDetailsViewModel = new RenterDetailsViewModel(_selectedRenter);
          InvoiceCollection = new ObservableCollection<Invoice>(DbController.GetInvoiceToPersonDB(_selectedRenter));
          RentalContractCollection = new ObservableCollection<RentalContract>(DbController.GetRentalContractsToPersonDB(_selectedRenter));
          if (MainWindowViewModel.GetInstance != null)
          {
            MainWindowViewModel.GetInstance.SelectedPersona = _selectedRenter;
          }
          OnPropertyChanged();
        }
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
