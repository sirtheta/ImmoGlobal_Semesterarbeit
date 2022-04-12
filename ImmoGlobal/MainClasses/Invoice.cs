using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;

namespace ImmoGlobal.MainClasses
{
  internal class Invoice
  {
    public int InvoiceId { get; set; }
    public Persona? Persona { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? InvoicePurpose { get; set; }
    public EInvoiceCategory InvoiceCategory { get; set; }
    public EInvoiceState InvoiceState { get; set; }
    public double TotalValue { get; set; }
    public ICollection<InvoicePosition>? InvoicePositions { get; set; }
    public int BillReminders { get; set; }
  }
}
