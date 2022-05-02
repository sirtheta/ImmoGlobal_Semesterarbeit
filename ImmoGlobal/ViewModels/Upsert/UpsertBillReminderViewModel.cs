﻿using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertBillReminderViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new bill reminder
    /// </summary>
    /// <param name="selectedInvoice"></param>
    internal UpsertBillReminderViewModel(Invoice selectedInvoice)
    {
      SelectedInvoice = selectedInvoice;
      ReminderDate = DateTime.Now;

      FormTitel = Application.Current.FindResource("addNewBillReminder") as string ?? "create new bill reminder";
    }

    /// <summary>
    /// c'tor ti update an existing bill reminder
    /// </summary>
    /// <param name="billReminder"></param>
    internal UpsertBillReminderViewModel(BillReminder billReminder)
    {
      Id = billReminder.BillReminderId;

      ReminderDate = billReminder.ReminderDate;
      ReminderAmount = billReminder.ReminderAmount.ToString();
      ReminderText = billReminder.ReminderText;

      //set the title of the form
      FormTitel = (Application.Current.FindResource("account") as string ?? "Bill Reminder") + " " +
       (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string _reminderAmount;
    private DateTime _reminderDate;
    private string _reminderText;

    public string ReminderAmount
    {
      get => _reminderAmount;
      set
      {
        _reminderAmount = value;
        OnPropertyChanged();
      }
    }

    public DateTime ReminderDate
    {
      get => _reminderDate;
      set
      {
        _reminderDate = value;
        OnPropertyChanged();
      }
    }

    public string ReminderText
    {
      get => _reminderText;
      set
      {
        _reminderText = value;
        OnPropertyChanged();
      }
    }

    internal Invoice SelectedInvoice { get; set; }

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(ReminderAmount, out double reminderAmount))
      {
        ShowMessageBox(Application.Current.FindResource("errorReminderAmount") as string ?? "Please enter a valid reminder amount", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create bill reminder
      if (Id == null && CreateBillReminder(reminderAmount))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddBillReminder") as string ?? "BillReminder added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update bill reminder
      else if (Id != null && UpdateBillReminder(reminderAmount, (int)Id))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateBillReminder") as string ?? "BillReminder updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddBillReminder") as string ?? "Error adding Bill Reminder", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (!string.IsNullOrEmpty(ReminderAmount) &&
          !string.IsNullOrEmpty(ReminderText))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Create bill reminder
    /// </summary>
    /// <param name="reminderAmount"></param>
    /// <returns></returns>
    private bool CreateBillReminder(double reminderAmount)
    {
      if (DbController.UpsertBillReminderToDB(new BillReminder
      {
        Invoice = SelectedInvoice,
        ReminderAmount = reminderAmount,
        ReminderDate = ReminderDate,
        ReminderText = ReminderText
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// update bill reminder
    /// </summary>
    /// <param name="reminderAmount"></param>
    /// <param name="reminderId"></param>
    /// <returns></returns>
    private bool UpdateBillReminder(double reminderAmount, int reminderId)
    {
      if (DbController.UpsertBillReminderToDB(new BillReminder
      {
        BillReminderId = reminderId,
        ReminderAmount = reminderAmount,
        ReminderDate = ReminderDate,
        ReminderText = ReminderText
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
      ReminderAmount = string.Empty;
      ReminderDate = DateTime.Now;
      ReminderText = string.Empty;
    }
  }
}
