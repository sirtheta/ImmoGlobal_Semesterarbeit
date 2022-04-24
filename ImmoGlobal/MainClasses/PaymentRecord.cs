using System;

namespace ImmoGlobal.MainClasses
{
  internal class PaymentRecord
  {

    public int PaymentRecordId { get; set; }
    public Account Account { get; set; }
    public string Description { get; set; }
    public int ReceiptNumber { get; set; }
    public double? IncomeAmount { get; set; }
    public double? ExpenseAmount { get; set; }
    public DateTime Date { get; set; }
  }
}
