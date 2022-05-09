using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  /// <summary>
  /// model for the invoice
  /// </summary>
  internal class Invoice
  {
    public int InvoiceId { get; set; }
    public Persona Persona { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string InvoicePurpose { get; set; }
    public EInvoiceCategory InvoiceCategory { get; set; }
    public EInvoiceState InvoiceState { get; set; }
    public ICollection<InvoicePosition>? InvoicePositions { get; set; }
    public ICollection<BillReminder>? BillReminders { get; set; }


    public string PersonaFullName
    {
      get => GetPersonaToInvoice().FullName;
    }

    internal Persona GetPersonaToInvoice() => DbController.GetPersonaToInvoiceDB(this);

    public double TotalValue
    {
      get
      {
        var value = 0.0;
        foreach (var invoicePosition in GetInvoicePositonToInvoice())
        {
          value += invoicePosition.Value;
        }
        return value;
      }
    }

    internal ICollection<InvoicePosition> GetInvoicePositonToInvoice() => DbController.GetInvoicePositionsToInvoiceDB(this);

    public string InvoiceCategoryString
    {
      get
      {
        return InvoiceCategory switch
        {
          EInvoiceCategory.Property => Application.Current.TryFindResource("property") as string ?? "Property",
          EInvoiceCategory.Object => Application.Current.TryFindResource("propertyObject") as string ?? "Property Object",
          EInvoiceCategory.Rent => Application.Current.TryFindResource("rent") as string ?? "Rent",
          EInvoiceCategory.AdditionalCosts => Application.Current.TryFindResource("additionalCosts") as string ?? "Additional Costs",
          EInvoiceCategory.BillReminder => Application.Current.TryFindResource("billReminder") as string ?? "Bill Reminder",
          EInvoiceCategory.None => Application.Current.TryFindResource("none") as string ?? "none",
          _ => "",
        };
      }
    }

    public string InvoiceStateString
    {
      get
      {
        return InvoiceState switch
        {
          EInvoiceState.NotReleased => Application.Current.TryFindResource("notReleased") as string ?? "not Released",
          EInvoiceState.Released => Application.Current.TryFindResource("released") as string ?? "Released",
          EInvoiceState.Paid => Application.Current.TryFindResource("paid") as string ?? "Paid",
          EInvoiceState.Canceled => Application.Current.TryFindResource("canceled") as string ?? "Canceled",
          _ => "",
        };
      }
    }

    public string BillRemindersString
    {
      get
      {
        int reminders = 0;
        var reminderCollection = GetBillReminders();
        if (reminderCollection != null)
        {
          reminders = reminderCollection.Count;
        }

        return reminders == 0 ? Application.Current.TryFindResource("none") as string ?? "none" : reminders.ToString();
      }
    }

    internal ICollection<BillReminder>? GetBillReminders() => DbController.GetBillRemindersToInvoiceDB(this);
  }
}
