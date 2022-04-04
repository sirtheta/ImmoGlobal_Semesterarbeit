using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoGlobal.MainClasses.Enum;

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
  }
}
