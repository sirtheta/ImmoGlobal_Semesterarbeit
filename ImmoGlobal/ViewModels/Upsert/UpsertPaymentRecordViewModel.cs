using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertPaymentRecordViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new payment record
    /// </summary>
    /// <param name="selectedAccount"></param>
    internal UpsertPaymentRecordViewModel(Account selectedAccount)
    {
      SelectedAccount = selectedAccount;

      Date = DateTime.Now;

      //set the title of the form
      FormTitel = (Application.Current.TryFindResource("addNewPaymentRecord") as string ?? "create new payment record") + " " +
                  (Application.Current.TryFindResource("for") as string ?? "for") + " " + SelectedAccount.Description + " " +
                  (Application.Current.TryFindResource("add") as string ?? "add");
    }

    /// <summary>
    /// c'tor to edit an existing payment record
    /// </summary>
    /// <param name="selectedAccount"></param>
    /// <param name="selectedPaymentRecord"></param>
    internal UpsertPaymentRecordViewModel(Account selectedAccount, PaymentRecord selectedPaymentRecord)
    {
      PaymentRecord = selectedPaymentRecord;
      Id = selectedPaymentRecord.PaymentRecordId;
      SelectedAccount = selectedAccount;

      ReceiptNumber = selectedPaymentRecord.ReceiptNumber.ToString();
      Description = selectedPaymentRecord.Description;
      IncomeAmount = selectedPaymentRecord.IncomeAmount.ToString();
      ExpenseAmount = selectedPaymentRecord.ExpenseAmount.ToString();
      Date = selectedPaymentRecord.Date;

      //set the title of the form
      FormTitel = (Application.Current.TryFindResource("paymentRecord") as string ?? "payment record") + " " +
                  (Application.Current.TryFindResource("edit") as string ?? "edit");
    }

    public Account SelectedAccount { get; private set; }

    private string _receiptNumber;
    private string _description;
    private string _incomeAmount;
    private string _expenseAmount;
    private bool _incomeEnabled = true;
    private bool _expenseEnabled = true;
    private DateTime _date;

    private PaymentRecord PaymentRecord { get; set; }
    public string ReceiptNumber
    {
      get => _receiptNumber;
      set
      {
        _receiptNumber = value;
        OnPropertyChanged();
      }
    }

    public string Description
    {
      get => _description;
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }

    public string IncomeAmount
    {
      get => _incomeAmount;
      set
      {
        _incomeAmount = value;
        if (_incomeAmount != string.Empty)
        {
          ExpenseEnabled = false;
        }
        else
        {
          ExpenseEnabled = true;
        }
        OnPropertyChanged();
      }
    }

    public string ExpenseAmount
    {
      get => _expenseAmount;
      set
      {
        _expenseAmount = value;
        if (_expenseAmount != string.Empty)
        {
          IncomeEnabled = false;
        }
        else
        {
          IncomeEnabled = true;
        }
        OnPropertyChanged();
      }
    }

    public bool IncomeEnabled
    {
      get => _incomeEnabled;
      set
      {
        _incomeEnabled = value;
        OnPropertyChanged();
      }
    }
    public bool ExpenseEnabled
    {
      get => _expenseEnabled;
      set
      {
        _expenseEnabled = value;
        OnPropertyChanged();
      }
    }
    public DateTime Date
    {
      get => _date;
      set
      {
        _date = value;
        OnPropertyChanged();
      }
    }

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      bool income = true;
      bool expense = true;
      if (!double.TryParse(IncomeAmount, out double incomeAmount))
      {
        income = false;
      }

      if (!double.TryParse(ExpenseAmount, out double expenseAmount))
      {
        expense = false;
      }

      if (!income && !expense)
      {
        ShowMessageBox(Application.Current.TryFindResource("errorIncomeExpense") as string ?? "Please enter income or expense value", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(ReceiptNumber, out int receiptNumber))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorReceiptNumber") as string ?? "Please enter a valid receipt number", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create payment record
      if (Id == null && CreatePaymentRecord(receiptNumber, incomeAmount, expenseAmount))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddPaymentRecord") as string ?? "Payment record added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update payment record
      else if (Id != null && UpdatePaymentRecord(receiptNumber, (int)Id, incomeAmount, expenseAmount))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdatePaymentRecord") as string ?? "Payment record updated successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorAddPaymentRecord") as string ?? "Error adding payment record", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (!string.IsNullOrEmpty(Description) &&
          !string.IsNullOrEmpty(ReceiptNumber) &&
          (!string.IsNullOrEmpty(IncomeAmount) ||
          !string.IsNullOrEmpty(ExpenseAmount)))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Create new account
    /// </summary>
    /// <returns></returns>
    private bool CreatePaymentRecord(int receiptNumber, double? incomeAmount, double? expenseAmount = null)
    {
      if (incomeAmount == 0)
      {
        incomeAmount = null;
      }
      if (expenseAmount == 0)
      {
        expenseAmount = null;
      }
      if (DbController.UpsertPaymentRecordToDB(new PaymentRecord
      {
        Account = SelectedAccount,
        Description = Description,
        ReceiptNumber = receiptNumber,
        IncomeAmount = incomeAmount,
        ExpenseAmount = expenseAmount,
        Date = Date
      }))
      {
        return true;
      }
      return false;
    }

    ///// <summary>
    ///// update account
    ///// </summary>
    ///// <param name="accountId"></param>
    ///// <returns></returns>
    private bool UpdatePaymentRecord(int receiptNumber, int paymentRecordId, double? incomeAmount, double? expenseAmount)
    {
      if (incomeAmount == 0)
        incomeAmount = null;

      if (expenseAmount == 0)
        expenseAmount = null;

      PaymentRecord.PaymentRecordId = paymentRecordId;
      PaymentRecord.Account = SelectedAccount;
      PaymentRecord.Description = Description;
      PaymentRecord.ReceiptNumber = receiptNumber;
      PaymentRecord.IncomeAmount = incomeAmount;
      PaymentRecord.ExpenseAmount = expenseAmount;
      PaymentRecord.Date = Date;

      if (DbController.UpsertPaymentRecordToDB(PaymentRecord))
      {
        return true;
      }
      return false;
    }
  }
}
