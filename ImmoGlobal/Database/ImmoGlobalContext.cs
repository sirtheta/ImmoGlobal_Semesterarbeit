using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;

namespace ImmoGlobal
{
  class ImmoGlobalContext : DbContext
  {
    public ImmoGlobalContext() : base(new DbContextOptionsBuilder<ImmoGlobalContext>()

    .UseSqlServer(ConnectionString).Options)
    {
    }
    private static string? _connectionString;
    public static string ConnectionString
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

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyObject> PropertyObjects { get; set; }
    public DbSet<RentalContract> RentalContracts { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoicePosition> InvoicePositions { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PaymentRecord> PaymentRecords { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AuditTrail> AuditTrail { get; set; }
  }
}
