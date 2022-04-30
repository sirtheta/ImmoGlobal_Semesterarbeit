using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class CreditorOverviewViewModel : BaseViewModel
  {
    internal CreditorOverviewViewModel()
    {
      _creditorCollection = new ObservableCollection<Persona>(DbController.GetAllCreditorsDB());
      _selectedCreditorDetailsViewModel = null;
    }

    private ObservableCollection<Persona> _creditorCollection;
    private ObservableCollection<Invoice>? _invoiceCollection;
    private Persona? _selectedCreditor;
    private CreditorDetailsViewModel? _selectedCreditorDetailsViewModel;
    private Invoice? _selectedInvoice;

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
        if (_selectedCreditor != value && MainWindowViewModelInstance != null)
        {
          _selectedCreditor = value;
          SelectedCreditorDetailsViewModel = new CreditorDetailsViewModel(_selectedCreditor);
          InvoiceCollection = new ObservableCollection<Invoice>(DbController.GetInvoiceToPersonaDB(_selectedCreditor));
          MainWindowViewModelInstance.SelectedPersona = _selectedCreditor;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
        }
        else
        {
          MainWindowViewModelInstance.SelectedPersona = _selectedCreditor;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;

        }
        OnPropertyChanged();
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

    public Invoice? SelectedInvoice
    {
      get => _selectedInvoice;
      set
      {
        if (_selectedInvoice != value && MainWindowViewModelInstance != null)
        {
          _selectedInvoice = value;
          MainWindowViewModelInstance.SelectedInvoice = _selectedInvoice;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoWidth = 200;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTextTwo =
             Application.Current.FindResource("editInvoice") as string ?? "Edit Invoice"; ;
          if (_selectedInvoice.InvoiceState == EInvoiceState.OverDue)
          {

            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
          }
          else
          {
            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          }

        }
        OnPropertyChanged();
      }
    }
  }
}
