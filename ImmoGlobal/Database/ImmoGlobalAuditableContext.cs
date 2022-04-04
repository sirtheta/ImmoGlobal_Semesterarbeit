using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.Database
{
  internal class ImmoGlobalAuditableContext : ImmoGlobalContext
  {
    public override int SaveChanges()
    {
      ChangeTracker.Entries().ToList().ForEach(entry =>
      {
        Audit(entry);
      });

      return base.SaveChanges();
    }

    private void Audit(EntityEntry entry)
    {
      foreach (var property in entry.Properties)
      {
        if (property.IsModified)
          continue;

        var auditEntry = new AuditTrail
        {
          //Table = entry.Entity.GetType().Name,
          //Column = property.Metadata.Name,
          //OldValue = property.OriginalValue.ToString(),
          //NewValue = property.CurrentValue.ToString(),
          ////EditedUser = GetCurrentUser(),
          //Date = DateTime.Now
        };

        if (AuditTrail != null)
        {
          AuditTrail.Add(auditEntry);
        }
        else
        {
          throw new Exception("AuditTrail is null");
        }
      }
    }

    private User GetCurrentUser()
    {
      throw new NotImplementedException();
    }
  }
}
