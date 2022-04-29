using System;

namespace ImmoGlobal.MainClasses
{
  internal class BillReminder
  {
    public int BillReminderId { get; set; }
    public Invoice Invoice { get; set; }
    public DateTime ReminderDate { get; set; }
    public double ReminderAmount { get; set; }
    public string ReminderText { get; set; }
  }
}
