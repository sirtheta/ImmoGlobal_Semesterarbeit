using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoGlobal.MainClasses.State;

namespace ImmoGlobal.MainClasses
{
  internal class Invoice
  {
    [Key]
    public int InvoiceNumber { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public DateOnly DueDate { get; set; }
    public string? InvoicePurpose { get; set; }
    internal Property? Property { get; set; }
    internal Object? Object { get; set; }
    internal Persona? Persona { get; set; }
    internal Account? Account { get; set; }
    internal EInvoiceCategory EInvoiceCategory { get; set; }
    internal EInvoiceState InvoiceState { get; set; }
  }
}
