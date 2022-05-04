using ImmoGlobal.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace ImmoGlobal.ViewModels
{
  internal class MenuBarViewModel : BaseViewModel
  {
    internal MenuBarViewModel()
    {
      BtnProperty = new RelayCommand<object>(BtnPropertyClick);
      BtnRenter = new RelayCommand<object>(BtnRenterClick);
      BtnCreditor = new RelayCommand<object>(BtnCreditorsClick);
      BtnRentalContract = new RelayCommand<object>(BtnRentalContractClick);
      BtnInvoice = new RelayCommand<object>(BtnInvoiceClick);
      BtnAccount = new RelayCommand<object>(BtnAccountClick);
      BtnLogout = new RelayCommand<object>(BtnLogoutClick);
      IsEnabled = false;
    }

    private void BtnPropertyClick(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
    }
    private void BtnRenterClick(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new RenterOverviewViewModel();
      }
    }
    private void BtnRentalContractClick(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new RentalContractsOverviewViewModel();
      }
    }
    private void BtnCreditorsClick(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new CreditorOverviewViewModel();
      }
    }
    private void BtnInvoiceClick(object obj)

    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new InvoicesOverviewViewModel();
      }
    }
    private void BtnAccountClick(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new AccountsOverviewViewModel();
      }
    }
    private void BtnLogoutClick(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        instance.SelectedViewModel = new LoginViewModel();
        instance.SelectedProperty = null;
        instance.SelectedPropertyObject = null;
        instance.SelectedPersona = null;
        instance.SelectedInvoice = null;
        instance.SelectedRentalContract = null;
        instance.SelectedPaymentRecord = null;
        instance.SelectedBillReminder = null;
        instance.MenuBarViewModel.LogedInUserFullName = null;
        instance.NavigationStore.Clear();
        IsEnabled = false;

      }
    }

    private bool _isEnabled;
    private string? _logedInUserFullName;
    public bool IsEnabled
    {
      get => _isEnabled;
      set
      {
        _isEnabled = value;
        OnPropertyChanged();
      }
    }

    public string? LogedInUserFullName
    {
      get => _logedInUserFullName;
      set
      {
        _logedInUserFullName = value;
        OnPropertyChanged();
      }
    }
    public ICommand BtnProperty
    {
      get;
      private set;
    }
    public ICommand BtnRenter
    {
      get;
      private set;
    }
    public ICommand BtnCreditor
    {
      get;
      private set;
    }
    public ICommand BtnRentalContract
    {
      get;
      private set;
    }
    public ICommand BtnInvoice
    {
      get;
      private set;
    }
    public ICommand BtnAccount
    {
      get;
      private set;
    }
    public ICommand BtnLogout
    {
      get;
      private set;
    }


    private Brush? _btnPropertyColor;
    private Brush? _btnRenterColor;
    private Brush? _btnCreditorColor;
    private Brush? _btnRentalContractColor;
    private Brush? _btnInvoiceColor;
    private Brush? _btnAccountColor;

    public Brush? BtnPropertyColor
    {
      get => _btnPropertyColor;
      set
      {
        _btnPropertyColor = value;
        OnPropertyChanged();
      }
    }
    public Brush? BtnRenterColor
    {
      get => _btnRenterColor;
      set
      {
        _btnRenterColor = value;
        OnPropertyChanged();
      }
    }
    public Brush? BtnCreditorColor
    {
      get => _btnCreditorColor;
      set
      {
        _btnCreditorColor = value;
        OnPropertyChanged();
      }
    }
    public Brush? BtnRentalContractColor
    {
      get => _btnRentalContractColor;
      set
      {
        _btnRentalContractColor = value;
        OnPropertyChanged();
      }
    }
    public Brush? BtnInvoiceColor
    {
      get => _btnInvoiceColor;
      set
      {
        _btnInvoiceColor = value;
        OnPropertyChanged();
      }
    }
    public Brush? BtnAccountColor
    {
      get => _btnAccountColor;
      set
      {
        _btnAccountColor = value;
        OnPropertyChanged();
      }
    }
  }
}