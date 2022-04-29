using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ImmoGlobal
{
  class ImmoGlobalContext : DbContext
  {
    internal ImmoGlobalContext() : base(new DbContextOptionsBuilder<ImmoGlobalContext>()

    .UseSqlServer(ConnectionString).Options)
    {
    }
    private static string? _connectionString;
    internal static string ConnectionString
    {
      get
      {
        if (_connectionString == null)
        {
          return ConfigurationManager.ConnectionStrings["ImmoGlobal"].ConnectionString;
        }
        else
        {
          return _connectionString;
        }
      }

      set
      {
        _connectionString = value;
      }
    }

    internal DbSet<Persona> Personas { get; set; }
    internal DbSet<Property> Properties { get; set; }
    internal DbSet<PropertyObject> PropertyObjects { get; set; }
    internal DbSet<RentalContract> RentalContracts { get; set; }
    internal DbSet<Invoice> Invoices { get; set; }
    internal DbSet<InvoicePosition> InvoicePositions { get; set; }
    internal DbSet<Account> Accounts { get; set; }
    internal DbSet<PaymentRecord> PaymentRecords { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<AuditTrail> AuditTrail { get; set; }
  }
}
