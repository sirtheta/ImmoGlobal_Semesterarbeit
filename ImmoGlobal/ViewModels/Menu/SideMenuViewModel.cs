using ImmoGlobal.Commands;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class SideMenuViewModel : BaseViewModel
  {

    internal SideMenuViewModel()
    {
      BtnNewProperty = new RelayCommand<object>(BtnNewPropertyClicked);
      BtnNewPropertyObject = new RelayCommand<object>(BtnNewPropertyObjectClicked);
      BtnNewRenter = new RelayCommand<object>(BtnNewRenterClicked);
      BtnNewInvoice = new RelayCommand<object>(BtnNewInvoiceClicked);
      BtnNewCreditor = new RelayCommand<object>(BtnNewCreditorClicked);
      BtnNewBillReminder = new RelayCommand<object>(BtnNewBillReminderClicked);
      BtnNewRentalContract = new RelayCommand<object>(BtnNewRentalContractClicked);
      BtnNewAccount = new RelayCommand<object>(BtnNewAccountClicked);
      BtnNewPaymentRecord = new RelayCommand<object>(BtnNewPaymentRecordClicked);
      BtnEdit = new RelayCommand<object>(BtnEditClicked);
      BtnEditTwo = new RelayCommand<object>(BtnEditTwoClicked);
      BtnHousekeeper = new RelayCommand<object>(BtnHousekeeperClicked);
      BtnNewHousekeeper = new RelayCommand<object>(BtnNewHousekeeperClicked);
      BtnEditText = Application.Current.FindResource("btnEdit") as string ?? "edit";
      BtnEditTextTwo = Application.Current.FindResource("btnEdit") as string ?? "edit";
    }

    private string _btnEditText;
    private string _btnEditTextTwo;
    private string _btnHousekeeperText;
    public string BtnEditText
    {
      get => _btnEditText;
      set
      {
        _btnEditText = value;
        OnPropertyChanged();
      }
    }
    public string BtnEditTextTwo
    {
      get => _btnEditTextTwo;
      set
      {
        _btnEditTextTwo = value;
        OnPropertyChanged();
      }
    }

    public string BtnHousekeeperText
    {
      get => _btnHousekeeperText;
      set
      {
        _btnHousekeeperText = value;
        OnPropertyChanged();
      }
    }

    #region Commands
    public ICommand BtnNewProperty
    {
      get;
      private set;
    }
    public ICommand BtnNewPropertyObject
    {
      get;
      private set;
    }
    public ICommand BtnNewRenter
    {
      get;
      private set;
    }
    public ICommand BtnNewInvoice
    {
      get;
      private set;
    }
    public ICommand BtnNewCreditor
    {
      get;
      private set;
    }
    public ICommand BtnNewBillReminder
    {
      get;
      private set;
    }
    public ICommand BtnNewRentalContract
    {
      get;
      private set;
    }
    public ICommand BtnNewAccount
    {
      get;
      private set;
    }
    public ICommand BtnNewPaymentRecord
    {
      get;
      private set;
    }
    public ICommand BtnEdit
    {
      get;
      private set;
    }
    public ICommand BtnEditTwo
    {
      get;
      private set;
    }
    public ICommand BtnHousekeeper
    {
      get;
      private set;
    }
    public ICommand BtnNewHousekeeper
    {
      get;
      private set;
    }
    #endregion

    #region MethodsToCommands
    private void BtnNewPropertyClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertPropertyViewModel();
      };
    }
    private void BtnNewPropertyObjectClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertPropertyObjectViewModel(MainWindowViewModelInstance.SelectedProperty);
      };
    }
    private void BtnNewPaymentRecordClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null && instance.SelectedAccount != null)
      {
        instance.SelectedViewModel = new UpsertPaymentRecordViewModel(instance.SelectedAccount);
      };
    }
    private void BtnNewAccountClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertAccountViewModel();
      }
    }
    private void BtnNewRentalContractClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertRentalContractViewModel();
      }
    }
    private void BtnNewBillReminderClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null && instance.SelectedInvoice != null)
      {
        instance.SelectedViewModel = new UpsertBillReminderViewModel(instance.SelectedInvoice);
      }
    }
    private void BtnNewCreditorClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertCreditorViewModel();
      };
    }
    private void BtnNewInvoiceClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        switch (instance.SelectedViewModel)
        {
          case RenterOverviewViewModel:
          case CreditorOverviewViewModel:
            if (instance.SelectedPersona != null)
            {
              instance.SelectedViewModel = new UpsertInvoiceViewModel(instance.SelectedPersona);
            }
            break;
          default:
            instance.SelectedViewModel = new UpsertInvoiceViewModel();
            break;

        }

      }
    }
    private void BtnNewRenterClicked(object obj)
    {
      if (MainWindowViewModelInstance != null)
      {
        MainWindowViewModelInstance.SelectedViewModel = new UpsertRenterViewModel();
      }
    }
    private void BtnEditClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        switch (instance.SelectedViewModel)
        {
          case PropertyObjectOverviewViewModel:
            instance.SelectedViewModel = new UpsertPropertyViewModel(instance.SelectedProperty);
            break;
          case PropertyObjectViewModel:
            instance.SelectedViewModel = new UpsertPropertyObjectViewModel(instance.SelectedProperty, instance.SelectedPropertyObject);
            break;
          case RenterOverviewViewModel:
            if (instance.SelectedPersona != null)
            {
              instance.SelectedViewModel = new UpsertRenterViewModel(instance.SelectedPersona);
            }
            break;
          case CreditorOverviewViewModel:
            if (instance.SelectedPersona != null)
            {
              instance.SelectedViewModel = new UpsertCreditorViewModel(instance.SelectedPersona);
            }
            break;
          case InvoicesOverviewViewModel:
            if (instance.SelectedInvoice != null)
            {
              instance.SelectedViewModel = new UpsertInvoiceViewModel(instance.SelectedInvoice);
            }
            break;
          case AccountsOverviewViewModel:
            if (instance.SelectedAccount != null)
            {
              instance.SelectedViewModel = new UpsertAccountViewModel(instance.SelectedAccount);
            }
            break;
          case RentalContractsOverviewViewModel:
            if (instance.SelectedRentalContract != null)
            {
              instance.SelectedViewModel = new UpsertRentalContractViewModel(instance.SelectedRentalContract);
            }
            break;
        }
      };
    }
    private void BtnEditTwoClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        switch (instance.SelectedViewModel)
        {
          case AccountsOverviewViewModel:
            if (instance.SelectedPaymentRecord != null && instance.SelectedAccount != null)
            {
              instance.SelectedViewModel = new UpsertPaymentRecordViewModel(instance.SelectedAccount, instance.SelectedPaymentRecord);
            }
            break;
          case InvoicesOverviewViewModel:
            if (instance.SelectedBillReminder != null)
            {
              instance.SelectedViewModel = new UpsertBillReminderViewModel(instance.SelectedBillReminder);
            }
            break;
          case CreditorOverviewViewModel:
          case PropertyObjectViewModel:
          case RenterOverviewViewModel:
            if (instance.SelectedInvoice != null)
            {
              instance.SelectedViewModel = new UpsertInvoiceViewModel(instance.SelectedInvoice);
            }
            break;
        }
      }
    }
    private void BtnHousekeeperClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null && instance.SelectedPersona != null)
      {
        instance.SelectedViewModel = new UpsertHousekeeperViewModel(instance.SelectedPersona);
      }
    }
    private void BtnNewHousekeeperClicked(object obj)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        instance.SelectedViewModel = new UpsertHousekeeperViewModel();
      }
    }
    #endregion

    /// <summary>
    /// Visibilty Properties for all Buttons
    /// </summary>
    #region ButtonVisibility
    private Visibility _btnNewPropertyVisibility;
    private Visibility _btnNewPropertyObjectVisibility;
    private Visibility _btnNewRenterVisibility;
    private Visibility _btnNewInvoiceVisibility;
    private Visibility _btnNewCreditorVisibility;
    private Visibility _btnNewBillReminderVisibility;
    private Visibility _btnNewRentalContractVisibility;
    private Visibility _btnNewAccountVisibility;
    private Visibility _btnNewPaymentRecordVisibility;
    private Visibility _btnEditVisibility;
    private Visibility _btnEditTwoVisibility;
    private Visibility _btnHousekeeperVisibility;
    private Visibility _btnNewHousekeeperVisibility;

    public Visibility BtnNewPropertyVisibility
    {
      get => _btnNewPropertyVisibility;
      set
      {
        _btnNewPropertyVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewPropertyObjectVisibility
    {
      get => _btnNewPropertyObjectVisibility;
      set
      {
        _btnNewPropertyObjectVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewRenterVisibility
    {
      get => _btnNewRenterVisibility;
      set
      {
        _btnNewRenterVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewInvoiceVisibility
    {
      get => _btnNewInvoiceVisibility;
      set
      {
        _btnNewInvoiceVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewCreditorVisibility
    {
      get => _btnNewCreditorVisibility;
      set
      {
        _btnNewCreditorVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewBillReminderVisibility
    {
      get => _btnNewBillReminderVisibility;
      set
      {
        _btnNewBillReminderVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewRentalContractVisibility
    {
      get => _btnNewRentalContractVisibility;
      set
      {
        _btnNewRentalContractVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewAccountVisibility
    {
      get => _btnNewAccountVisibility;
      set
      {
        _btnNewAccountVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewPaymentRecordVisibility
    {
      get => _btnNewPaymentRecordVisibility;
      set
      {
        _btnNewPaymentRecordVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnEditVisibility
    {
      get => _btnEditVisibility;
      set
      {
        var instance = MainWindowViewModelInstance;
        if (instance != null && CanEdit)
        {
          _btnEditVisibility = value;
        }
        OnPropertyChanged();
      }
    }
    public Visibility BtnEditTwoVisibility
    {
      get => _btnEditTwoVisibility;
      set
      {
        var instance = MainWindowViewModelInstance;
        if (instance != null && CanEdit)
        {
          _btnEditTwoVisibility = value;
        }
        OnPropertyChanged();
      }
    }
    public Visibility BtnHousekeeperVisibility
    {
      get => _btnHousekeeperVisibility;
      set
      {
        _btnHousekeeperVisibility = value;
        OnPropertyChanged();
      }
    }
    public Visibility BtnNewHousekeeperVisibility
    {
      get => _btnNewHousekeeperVisibility;
      set
      {
        _btnNewHousekeeperVisibility = value;
        OnPropertyChanged();
      }
    }
    #endregion

    private int _btnEditTwoWidth;
    public int BtnEditTwoWidth
    {
      get => _btnEditTwoWidth;
      set
      {
        _btnEditTwoWidth = value;
        OnPropertyChanged();
      }
    }
  }
}
