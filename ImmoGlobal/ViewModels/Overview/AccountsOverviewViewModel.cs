using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class AccountsOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      AccountsCollection = new ObservableCollection<Account>(DbController.GetAllAccountsDB());
      SelectedAccount = AccountsCollection.FirstOrDefault();
      OnPropertyChanged(nameof(AccountsCollection));
    }

    public string AccountTitel { get; set; }
    public ObservableCollection<Account> AccountsCollection { get; set; }

    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
      get => _selectedAccount;
      set
      {
        //activate the needed buttons to edit the selected account or to add a payment record
        _selectedAccount = value;
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedAccount = _selectedAccount;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditText =
            (Application.Current.TryFindResource("account") as string ?? "account") + " " +
            (Application.Current.TryFindResource("edit") as string ?? "edit");
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Visible;
        }
        IncomeExpenseCollection = new(DbController.GetIncomeToAccountDB(_selectedAccount).
        Concat(DbController.GetExpenseToAccountDB(_selectedAccount)).ToList());

        AccountTitel = (Application.Current.TryFindResource("incomeExpenseToAccount") as string ?? "income and expenses for")
                        + " " + _selectedAccount?.Description;
        OnPropertyChanged(nameof(AccountTitel));
        OnPropertyChanged(nameof(IncomeExpenseCollection));
        OnPropertyChanged();
      }
    }

    public ObservableCollection<PaymentRecord> IncomeExpenseCollection { get; set; }

    private PaymentRecord? _selectedPaymentRecord;
    public PaymentRecord? SelectedPaymentRecord
    {
      get => _selectedPaymentRecord;
      set
      {
        _selectedPaymentRecord = value;
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedPaymentRecord = _selectedPaymentRecord;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTextTwo =
            (Application.Current.TryFindResource("paymentRecord") as string ?? "payment record") + " " +
            (Application.Current.TryFindResource("edit") as string ?? "edit");
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
        }
        OnPropertyChanged();
      }
    }
  }
}
