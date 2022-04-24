using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class AccountsOverviewViewModel : BaseViewModel
  {
    public AccountsOverviewViewModel()
    {
      AccountsCollection = new ObservableCollection<Account>(DbController.GetAllAccountsDB());
      AccountTitel = Application.Current.FindResource("incomeExpenseToAccount") as string ?? "income and expenses for";

      MainWindowViewModelInstance = MainWindowViewModel.GetInstance;
    }

    private string _accountTitel;
    public string AccountTitel
    {
      get => _accountTitel;
      set
      {
        _accountTitel = value;
        OnPropertyChanged();
      }
    }

    private MainWindowViewModel? MainWindowViewModelInstance { get; set; }
    public ObservableCollection<Account> AccountsCollection { get; set; }

    private ObservableCollection<PaymentRecord> _icomeExpenseCollection;
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
      get => _selectedAccount;
      set
      {
        _selectedAccount = value;
        if (MainWindowViewModelInstance != null)
        {
          MainWindowViewModelInstance.SelectedAccount = _selectedAccount;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditText =
            (Application.Current.FindResource("account") as string ?? "account") + " " +
            (Application.Current.FindResource("edit") as string ?? "edit");
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
        }
        IncomeExpenseCollection = new(DbController.GetIncomeToAccountDB(_selectedAccount).
        Concat(DbController.GetExpenseToAccountDB(_selectedAccount)).ToList());

        AccountTitel = (Application.Current.FindResource("incomeExpenseToAccount") as string ?? "income and expenses for") 
                        + " " + _selectedAccount?.AccountDescription;

        OnPropertyChanged();
      }
    }
    
    public ObservableCollection<PaymentRecord> IncomeExpenseCollection
    {
      get => _icomeExpenseCollection;
      set
      {
        _icomeExpenseCollection = value;
        OnPropertyChanged();
      }
    }

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
            (Application.Current.FindResource("paymentRecord") as string ?? "payment record") + " " +
            (Application.Current.FindResource("edit") as string ?? "edit");
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
        }

        OnPropertyChanged();
      }
    }
  }
}
