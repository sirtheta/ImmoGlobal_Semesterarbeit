using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class CreditorOverviewViewModel : BaseViewModel
  {
    private ObservableCollection<Persona> _creditorCollection;
    private ObservableCollection<Invoice>? _invoiceCollection;
    private Persona? _selectedCreditor;
    private CreditorDetailsViewModel? _selectedCreditorDetailsViewModel;

    public CreditorOverviewViewModel()
    {
      _creditorCollection = new ObservableCollection<Persona>(DbController.GetAllCreditors());
      _selectedCreditorDetailsViewModel = null;
    }

    public ObservableCollection<Persona> CreditorCollection
    {
      get => _creditorCollection;
      set
      {
        _creditorCollection = value;
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

    public Persona? SelectedCreditor
    {
      get => _selectedCreditor;
      set
      {
        if (_selectedCreditor != value)
        {
          _selectedCreditor = value;
          SelectedCreditorDetailsViewModel = new CreditorDetailsViewModel(_selectedCreditor);
          InvoiceCollection = new ObservableCollection<Invoice>(DbController.GetInvoiceToPerson(_selectedCreditor));
          if (MainWindowViewModel.GetInstance != null)
          {
            MainWindowViewModel.GetInstance.SelectedPersona = _selectedCreditor;
          }
          OnPropertyChanged();
        }
      }
    }
    public CreditorDetailsViewModel? SelectedCreditorDetailsViewModel
    {
      get => _selectedCreditorDetailsViewModel;
      set
      {
        _selectedCreditorDetailsViewModel = value;
        OnPropertyChanged();
      }
    }
  }
}
