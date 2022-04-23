﻿using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertAccountViewModel : BaseViewModel
  {
    public UpsertAccountViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      FormTitel = Application.Current.FindResource("addNewAccount") as string ?? "create new account";
    }

    public UpsertAccountViewModel(Account selectedAccount)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      AccountId = selectedAccount.AccountId;
      AccountDescription = selectedAccount.AccountDescription;
      AccountNumber = selectedAccount.AccountNumber;


      FormTitel = (Application.Current.FindResource("account") as string ?? "account") + " " +
       (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string _accountNumber;
    private string _accountDescription;

    private int? AccountId { get; set; }
    public string FormTitel { get; set; }

    public string AccountNumber
    {
      get => _accountNumber;
      set
      {
        _accountNumber = value;
        OnPropertyChanged();
      }
    }

    public string AccountDescription
    {
      get => _accountDescription;
      set
      {
        _accountDescription = value;
        OnPropertyChanged();
      }
    }


    public ICommand BtnSave
    {
      get;
      private set;
    }

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create account
      if (AccountId == null && CreateAccount())
      {
        ShowNotification("Success", Application.Current.FindResource("successAddAccount") as string ?? "Account added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update account
      else if (AccountId != null && UpdateAccount((int) AccountId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateAccount") as string ?? "Account updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddAccount") as string ?? "Error adding account", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (string.IsNullOrEmpty(AccountDescription) ||
          string.IsNullOrEmpty(AccountNumber))
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Create new account
    /// </summary>
    /// <returns></returns>
    private bool CreateAccount()
    {
      if (DbController.UpsertAccountToDB(new Account()
      {
        AccountDescription = AccountDescription,
        AccountNumber = AccountNumber
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// update account
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    private bool UpdateAccount(int accountId)
    {
      if (DbController.UpsertAccountToDB(new Account()
      {
        AccountId = accountId,
        AccountDescription = AccountDescription,
        AccountNumber = AccountNumber
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Sets all properties to null
    /// </summary>
    private void ClearValues()
    {
      AccountNumber = string.Empty;
      AccountDescription = string.Empty;
    }
  }
}
