using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class AuditTrail
  {
    public long AuditTrailId { get; set; }
    public string? Table { get; set; }
    public string? Column { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime Date { get; set; }
    //public User? EditedUser { get; set; }
  }
}
