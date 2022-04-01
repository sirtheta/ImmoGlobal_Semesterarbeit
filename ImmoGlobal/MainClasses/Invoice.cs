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
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? InvoicePurpose { get; set; }
    public int PropertyId { get; set; }
    public Property? Property { get; set; }
    public int ObjectId { get; set; }
    public Object? Object { get; set; }
    public int PersonaId { get; set; }
    public Persona? Persona { get; set; }
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    public EInvoiceCategory InvoiceCategory { get; set; }
    public EInvoiceState InvoiceState { get; set; }
  }
}
