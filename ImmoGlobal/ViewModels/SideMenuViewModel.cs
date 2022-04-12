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
      BtnNewRenter = new RelayCommand<object>(BtnBtnNewRenterClicked);
      BtnInvoice = new RelayCommand<object>(BtnBtnInvoiceClicked);
      BtnNewInvoice = new RelayCommand<object>(BtnBtnNewInvoiceClicked);
      BtnObjects = new RelayCommand<object>(BtnBtnObjectsClicked);
      BtnRentalContracts = new RelayCommand<object>(BtnBtnRentalContractsClicked);
      BtnNewCreditor = new RelayCommand<object>(BtnBtnNewCreditorClicked);
      BtnNewBillReminder = new RelayCommand<object>(BtnBtnNewBillReminderClicked);
      BtnNewRentalContract = new RelayCommand<object>(BtnBtnNewRentalContractClicked);
      BtnNewAccount = new RelayCommand<object>(BtnBtnNewAccountClicked);
      BtnNewPaymentRecord = new RelayCommand<object>(BtnBtnNewPaymentRecordClicked);
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
    #endregion

    #region MethodsToCommands
    private void BtnNewPropertyClicked(object obj)
    {
      throw new NotImplementedException();
    }
    private void BtnNewPropertyObjectClicked(object obj)
    {
      throw new NotImplementedException();
    }
    private void BtnBtnNewPaymentRecordClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewAccountClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewRentalContractClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewBillReminderClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewCreditorClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnRentalContractsClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnObjectsClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewInvoiceClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnInvoiceClicked(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnBtnNewRenterClicked(object obj)
    {
      throw new NotImplementedException();
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
    #endregion
  }
}
