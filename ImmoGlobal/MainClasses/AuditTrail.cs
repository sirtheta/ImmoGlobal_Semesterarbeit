using System;

namespace ImmoGlobal.MainClasses
{
  internal class AuditTrail
  {
    public long AuditTrailId { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public DateTime Date { get; set; }
    public string Table { get; set; }
    public string Column { get; set; }
    public string PrimaryKey { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
  }
}
