using System;

namespace ImmoGlobal.MainClasses
{
  /// <summary>
  /// model for bill reminders in an invoice
  /// </summary>
  internal class BillReminder
  {
    public int BillReminderId { get; set; }
    public Invoice Invoice { get; set; }
    public DateTime ReminderDate { get; set; }
    public double ReminderAmount { get; set; }
    public string ReminderText { get; set; }
  }
}
