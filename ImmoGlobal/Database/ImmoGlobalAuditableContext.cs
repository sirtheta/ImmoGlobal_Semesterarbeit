using ImmoGlobal.MainClasses;
using ImmoGlobal.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace ImmoGlobal.Database
{
  internal class ImmoGlobalAuditableContext : ImmoGlobalContext
  {
    public override int SaveChanges()
    {
      ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
      {
        Audit(entry);
      });

      return base.SaveChanges();
    }

    /// <summary>
    /// writes the audit information to the database if the 
    /// property of the entity has changed
    /// </summary>
    /// <param name="entry"></param>
    /// <exception cref="Exception"></exception>
    private void Audit(EntityEntry entry)
    {
      var instance = MainWindowViewModel.GetInstance;
      foreach (var property in entry.Properties)
      {
        if (property.OriginalValue == null)
          continue;
        if (property.OriginalValue.ToString() == property.CurrentValue.ToString())
          continue;

        var auditEntry = new AuditTrail
        {
          Table = entry.Entity.GetType().Name,
          Column = property.Metadata.Name,
          PrimaryKey = entry.Metadata.FindPrimaryKey().Properties.Select(p => entry.Property(p.Name).CurrentValue).First().ToString(),
          OldValue = property.OriginalValue.ToString(),
          NewValue = property.CurrentValue.ToString(),
          User = instance.LogedInUser.FullName,
          Date = DateTime.Now
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
  }
}
