using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;


namespace ImmoGlobal
{
  class DataContext : DbContext
  {
    private static string? _connectionName;
    ///sets the connection name configured in App.config
    ///if ConnectionName is not set, the standard connection "DiaryApp" will be used
    public static string ConnectionName
    {
      get
      {
        if (_connectionName == null)
        {
          return "ImmoGlobal";
        }
        else
        {
          return _connectionName;
        }
      }

      set
      {
        _connectionName = value;
      }
    }

    public DataContext() : base()
    {
    }
    public virtual DbSet<Account>? Accounts { get; set; }
    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<Invoice>? Invoices { get; set; }
    public virtual DbSet<Object>? Objects { get; set; }
    public virtual DbSet<Persona>? Personas { get; set; }
    public virtual DbSet<Property>? Properties { get; set; }
    public virtual DbSet<RentalContract>? RentalContracts { get; set; }
  }
}
