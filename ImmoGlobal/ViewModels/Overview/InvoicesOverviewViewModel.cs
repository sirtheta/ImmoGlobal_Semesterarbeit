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

      IsOverDueInvoiceSelected = Visibility.Collapsed;
    }

    private Visibility _isOverDueInvoiceSelected;
    private Invoice _selectedInvoice;
    private InvoicePosition? _selectedInvoicePosition;
    private BillReminder? _selectedBillReminder;
    private ObservableCollection<InvoicePosition> _invoicePositionCollection;
    private ObservableCollection<BillReminder> _billReminderCollection;

    public ObservableCollection<Invoice> InvoiceCollection { get; set; }

    public Invoice SelectedInvoice
    {
      get => _selectedInvoice;
      set
      {
        _selectedInvoice = value;
        InvoicePositionCollection = new(DbController.GetInvoicePositionsToInvoiceDB(_selectedInvoice));
        BillReminderCollection = new();
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedInvoice = _selectedInvoice;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Collapsed;
          if (_selectedInvoice.DueDate < System.DateTime.Now && _selectedInvoice.InvoiceState == EInvoiceState.Released)
          {
            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
            BillReminderCollection = new(DbController.GetBillRemindersToInvoiceDB(_selectedInvoice));
            IsOverDueInvoiceSelected = Visibility.Visible;
          }
          else
          {
            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
            BillReminderCollection.Clear();
            IsOverDueInvoiceSelected = Visibility.Collapsed;
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

    public ObservableCollection<BillReminder> BillReminderCollection
    {
      get => _billReminderCollection;
      set
      {
        _billReminderCollection = value;
        OnPropertyChanged();
      }
    }
    public BillReminder? SelectedBillReminder
    {
      get => _selectedBillReminder;
      set
      {
        _selectedBillReminder = value;
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedBillReminder = _selectedBillReminder;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoWidth = 195;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTextTwo =
            (Application.Current.FindResource("editBillReminder") as string ?? "edit bill reminder");
        }
        OnPropertyChanged();
      }
    }

    public Visibility IsOverDueInvoiceSelected
    {
      get => _isOverDueInvoiceSelected;
      set
      {
        _isOverDueInvoiceSelected = value;
        OnPropertyChanged();
      }
    }
  }
}
