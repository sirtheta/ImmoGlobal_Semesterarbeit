using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class InvoicesOverviewViewModel : BaseViewModel
  {

    internal InvoicesOverviewViewModel()
    {
      InvoiceCollection = new ObservableCollection<Invoice>(DbController.GetAllInvoicesDB());
      InvoicePositionTitel = Application.Current.FindResource("invoicePositionTo") as string ?? "invoice psoition to";

      MainWindowViewModelInstance = MainWindowViewModel.GetInstance;
    }

    private string _invoicePositionTitel;
    private Invoice _selectedInvoice;
    private InvoicePosition? _selectedInvoicePosition;
    private ObservableCollection<InvoicePosition> _invoicePositionCollection;
    private MainWindowViewModel? MainWindowViewModelInstance { get; set; }
    public ObservableCollection<Invoice> InvoiceCollection { get; set; }

    public string InvoicePositionTitel
    {
      get => _invoicePositionTitel;
      set
      {
        _invoicePositionTitel = value;
        OnPropertyChanged();
      }
    }
    public Invoice SelectedInvoice
    {
      get => _selectedInvoice;
      set
      {
        _selectedInvoice = value;
        InvoicePositionCollection = new(DbController.GetInvoicePositionsToInvoiceDB(_selectedInvoice));
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedInvoice = _selectedInvoice;
          MainWindowViewModelInstance.InvoicePositions = InvoicePositionCollection;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
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
    public ObservableCollection<InvoicePosition> InvoicePositionCollection
    {
      get => _invoicePositionCollection;
      set
      {
        _invoicePositionCollection = value;
        OnPropertyChanged();
      }
    }
    public InvoicePosition? SelectedInvoicePosition
    {
      get => _selectedInvoicePosition;
      set
      {
        _selectedInvoicePosition = value;
        if (MainWindowViewModelInstance != null)
        {
        }

        OnPropertyChanged();
      }
    }
  }
}
