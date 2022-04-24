using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImmoGlobal.ViewModels
{
  internal class AccountsOverviewViewModel : BaseViewModel
  {
    public AccountsOverviewViewModel()
    {
      AccountsCollection = new ObservableCollection<Account>(DbController.GetAllAccountsDB());
    }

    public ObservableCollection<Account> AccountsCollection { get; set; }

    private ObservableCollection<IncomeExpense> _icomeExpenseCollection;
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
      get => _selectedAccount;
      set
      {
        _selectedAccount = value;
        if (MainWindowViewModel.GetInstance != null)
        {
          MainWindowViewModel.GetInstance.SelectedAccount = _selectedAccount;
        }
        IncomeExpenseCollection = new(DbController.GetIncomeToAccountDB(_selectedAccount).
        Concat(DbController.GetExpenseToAccountDB(_selectedAccount)).ToList());
        OnPropertyChanged();
      }
    }
    public ObservableCollection<IncomeExpense> IncomeExpenseCollection 
    {
      get => _icomeExpenseCollection;
      set
      {
        _icomeExpenseCollection = value;
        OnPropertyChanged();
      }
    }
  }
}
