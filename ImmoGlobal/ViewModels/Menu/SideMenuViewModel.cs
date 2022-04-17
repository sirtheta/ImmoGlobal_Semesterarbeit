using ImmoGlobal.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class SideMenuViewModel : BaseViewModel
  {

    public SideMenuViewModel()
    {
      BtnNewProperty = new RelayCommand<object>(BtnNewPropertyClicked);
      BtnNewPropertyObject = new RelayCommand<object>(BtnNewPropertyObjectClicked);
      BtnNewRenter = new RelayCommand<object>(BtnNewRenterClicked);
      BtnInvoice = new RelayCommand<object>(BtnInvoiceClicked);
      BtnNewInvoice = new RelayCommand<object>(BtnNewInvoiceClicked);
      BtnObjects = new RelayCommand<object>(BtnObjectsClicked);
      BtnRentalContracts = new RelayCommand<object>(BtnRentalContractsClicked);
      BtnNewCreditor = new RelayCommand<object>(BtnNewCreditorClicked);
      BtnNewBillReminder = new RelayCommand<object>(BtnNewBillReminderClicked);
      BtnNewRentalContract = new RelayCommand<object>(BtnNewRentalContractClicked);
      BtnNewAccount = new RelayCommand<object>(BtnNewAccountClicked);
      BtnNewPaymentRecord = new RelayCommand<object>(BtnNewPaymentRecordClicked);
      BtnEdit = new RelayCommand<object>(BtnEditClicked);
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

    public ICommand BtnInvoice
    {
      get;
      private set;
    }

    public ICommand BtnNewInvoice
    {
      get;
      private set;
    }

    public ICommand BtnObjects
    {
      get;
      private set;
    }

    public ICommand BtnRentalContracts
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
    #endregion

    #region MethodsToCommands
    private void BtnNewPropertyClicked(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedViewModel = new UpsertPropertyViewModel();
      };
    }
    private void BtnNewPropertyObjectClicked(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedViewModel = new UpsertPropertyObjectViewModel(MainWindowViewModel.GetInstance.SelectedProperty);
      };
    }
    private void BtnNewPaymentRecordClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewAccountClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewRentalContractClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewBillReminderClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewCreditorClicked(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedViewModel = new UpsertCreditorViewModel();
      };
    }

    private void BtnRentalContractsClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnObjectsClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewInvoiceClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnInvoiceClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnNewRenterClicked(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedViewModel = new UpsertRenterViewModel();
      }
    }

    private void BtnEditClicked(object obj)
    {
      var instance = MainWindowViewModel.GetInstance;
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
        }
      };
    }

    #endregion
    /// <summary>
    /// Visibilty Properties for all Buttons
    /// </summary>
    #region ButtonVisibility
    private Visibility _btnNewPropertyVisibility;
    private Visibility _btnNewPropertyObjectVisibility;
    private Visibility _btnNewRenterVisibility;
    private Visibility _btnInvoiceVisibility;
    private Visibility _btnNewInvoiceVisibility;
    private Visibility _btnObjectsVisibility;
    private Visibility _btnRentalContractsVisibility;
    private Visibility _btnNewCreditorVisibility;
    private Visibility _btnNewBillReminderVisibility;
    private Visibility _btnNewRentalContractVisibility;
    private Visibility _btnNewAccountVisibility;
    private Visibility _btnNewPaymentRecordVisibility;
    private Visibility _btnEditVisibility;

    public Visibility BtnNewPropertyVisibility
    {
      get
      {
        return _btnNewPropertyVisibility;
      }
      set
      {
        _btnNewPropertyVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewPropertyObjectVisibility
    {
      get
      {
        return _btnNewPropertyObjectVisibility;
      }
      set
      {
        _btnNewPropertyObjectVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewRenterVisibility
    {
      get
      {
        return _btnNewRenterVisibility;
      }
      set
      {
        _btnNewRenterVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnInvoiceVisibility
    {
      get
      {
        return _btnInvoiceVisibility;
      }
      set
      {
        _btnInvoiceVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewInvoiceVisibility
    {
      get
      {
        return _btnNewInvoiceVisibility;
      }
      set
      {
        _btnNewInvoiceVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnObjectsVisibility
    {
      get
      {
        return _btnObjectsVisibility;
      }
      set
      {
        _btnObjectsVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnRentalContractsVisibility
    {
      get
      {
        return _btnRentalContractsVisibility;
      }
      set
      {
        _btnRentalContractsVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewCreditorVisibility
    {
      get
      {
        return _btnNewCreditorVisibility;
      }
      set
      {
        _btnNewCreditorVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewBillReminderVisibility
    {
      get
      {
        return _btnNewBillReminderVisibility;
      }
      set
      {
        _btnNewBillReminderVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewRentalContractVisibility
    {
      get
      {
        return _btnNewRentalContractVisibility;
      }
      set
      {
        _btnNewRentalContractVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewAccountVisibility
    {
      get
      {
        return _btnNewAccountVisibility;
      }
      set
      {
        _btnNewAccountVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnNewPaymentRecordVisibility
    {
      get
      {
        return _btnNewPaymentRecordVisibility;
      }
      set
      {
        _btnNewPaymentRecordVisibility = value;
        OnPropertyChanged();
      }
    }

    public Visibility BtnEditVisibility
    {
      get
      {
        return _btnEditVisibility;
      }
      set
      {
        _btnEditVisibility = value;
        OnPropertyChanged();
      }
    }
    #endregion
  }
}
