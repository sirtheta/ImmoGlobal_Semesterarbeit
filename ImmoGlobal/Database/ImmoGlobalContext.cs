using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ImmoGlobal
{
  class ImmoGlobalContext : DbContext
  {
    
    public ImmoGlobalContext(): base(new DbContextOptionsBuilder<ImmoGlobalContext>()
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

    public virtual DbSet<Account>? Accounts { get; set; }
    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<Invoice>? Invoices { get; set; }
    public virtual DbSet<Object>? Objects { get; set; }
    public virtual DbSet<Persona>? Personas { get; set; }
    public virtual DbSet<Property>? Properties { get; set; }
    public virtual DbSet<RentalContract>? RentalContracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Invoice>().HasOne(p => p.Property).WithMany().OnDelete(DeleteBehavior.NoAction); 
      modelBuilder.Entity<Invoice>().HasOne(p => p.Persona).WithMany().OnDelete(DeleteBehavior.NoAction);
      modelBuilder.Entity<RentalContract>().HasOne(p => p.Persona).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
  }
}
