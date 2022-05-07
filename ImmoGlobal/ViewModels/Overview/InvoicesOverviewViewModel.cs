using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class InvoicesOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      InvoiceCollection = new(DbController.GetAllInvoicesDB());
      OnPropertyChanged(nameof(InvoiceCollection));
      IsOverDueInvoiceSelected = Visibility.Collapsed;
    }

    private Invoice _selectedInvoice;
    private InvoicePosition? _selectedInvoicePosition;
    private BillReminder? _selectedBillReminder;

    public ObservableCollection<Invoice> InvoiceCollection { get; set; }

    public new Invoice SelectedInvoice
    {
      get => _selectedInvoice;
      set
      {
        if (_selectedInvoice != value && value != null && MainWindowViewModelInstance != null)
        {
          _selectedInvoice = value;
          InvoicePositionCollection = new(DbController.GetInvoicePositionsToInvoiceDB(_selectedInvoice));

          BillReminderCollection = new();
          MainWindowViewModelInstance.SelectedInvoice = _selectedInvoice;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Collapsed;
          if (_selectedInvoice.DueDate < System.DateTime.Now && _selectedInvoice.InvoiceState == EInvoiceState.Released)
          {
            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
            BillReminderCollection = new(DbController.GetBillRemindersToInvoiceDB(_selectedInvoice));
            IsOverDueInvoiceSelected = Visibility.Visible;
          }
        }
        else
        {
          InvoicePositionCollection.Clear();
          SelectedBillReminder = null;
          BillReminderCollection.Clear();
          IsOverDueInvoiceSelected = Visibility.Collapsed;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Collapsed;
        }
        OnPropertyChanged();
        OnPropertyChanged(nameof(InvoicePositionCollection));
        OnPropertyChanged(nameof(IsOverDueInvoiceSelected));
        OnPropertyChanged(nameof(BillReminderCollection));
      }
    }

    public ObservableCollection<InvoicePosition> InvoicePositionCollection { get; set; }

    public InvoicePosition? SelectedInvoicePosition
    {
      get => _selectedInvoicePosition;
      set
      {
        _selectedInvoicePosition = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<BillReminder> BillReminderCollection { get; set; }

    public BillReminder? SelectedBillReminder
    {
      get => _selectedBillReminder;
      set
      {
        if (_selectedBillReminder != value && MainWindowViewModelInstance != null)
        {
          _selectedBillReminder = value;
          MainWindowViewModelInstance.SelectedBillReminder = _selectedBillReminder;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoWidth = 195;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTextTwo =
            (Application.Current.TryFindResource("editBillReminder") as string ?? "edit bill reminder");
        }
        OnPropertyChanged();
      }
    }

    public Visibility IsOverDueInvoiceSelected { get; set; }
  }
}
