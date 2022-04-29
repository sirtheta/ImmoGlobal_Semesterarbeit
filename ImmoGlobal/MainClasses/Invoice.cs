using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
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
    public int? BillReminders { get; set; }


    public string PersonaFullName
    {
      get => GetPersonaToInvoice().FullName;
    }

    public Persona GetPersonaToInvoice()
    {
      return DbController.GetPersonaToInvoiceDB(this);
    }

    public double TotalValue
    {
      get
      {
        var value = 0.0;
        foreach (var invoicePosition in DbController.GetInvoicePositionsToInvoiceDB(this))
        {
          value += invoicePosition.Value;
        }
        return value;
      }
    }

    public string InvoiceCategoryString
    {
      get
      {
        return InvoiceCategory switch
        {
          EInvoiceCategory.Property => Application.Current.FindResource("property") as string ?? "Property",
          EInvoiceCategory.Object => Application.Current.FindResource("propertyObject") as string ?? "Property Object",
          EInvoiceCategory.Rent => Application.Current.FindResource("rent") as string ?? "Rent",
          EInvoiceCategory.AdditionalCosts => Application.Current.FindResource("additionalCosts") as string ?? "Additional Costs",
          EInvoiceCategory.BillReminder => Application.Current.FindResource("billReminder") as string ?? "Bill Reminder",
          EInvoiceCategory.None => Application.Current.FindResource("none") as string ?? "none",
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
          EInvoiceState.NotReleased => Application.Current.FindResource("notReleased") as string ?? "not Released",
          EInvoiceState.Released => Application.Current.FindResource("released") as string ?? "Released",
          EInvoiceState.OverDue => Application.Current.FindResource("overDue") as string ?? "Over Due",
          EInvoiceState.Paid => Application.Current.FindResource("paid") as string ?? "Paid",
          EInvoiceState.Canceled => Application.Current.FindResource("canceled") as string ?? "Canceled",
          _ => "",
        };
      }
    }

    public string BillRemindersString
    {
      get => BillReminders?.ToString() ?? Application.Current.FindResource("none") as string ?? "none";
    }
  }
}
