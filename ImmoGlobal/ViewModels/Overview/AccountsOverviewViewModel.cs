using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class AccountsOverviewViewModel : BaseViewModel
  {
    public AccountsOverviewViewModel()
    {
      AccountsCollection = new ObservableCollection<Account>(DbController.GetAllAccountsDB());
    }
    
    public ObservableCollection<Account> AccountsCollection { get; set; }

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
        OnPropertyChanged();
      }
    }
  }
}
