using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  internal class RentalContract
  {
    /// <summary>
    /// model for the rental contract
    /// </summary>
    public int RentalContractId { get; set; }
    public Persona Renter { get; set; }
    public PropertyObject? PropertyObject { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime? RentEndDate { get; set; }
    public double Rent { get; set; }
    public bool Deposit { get; set; }
    public EContractState ContractState { get; set; }

    public string RenterFullName
    {
      get => GetRenter()?.FullName ?? "";
    }

    internal Persona GetRenter()
    {
      return DbController.GetRenterToRentalContractDB(this);
    }

    public string DepositString
    {
      get => Deposit ? Application.Current.FindResource("yes") as string ?? "yes" :
          Application.Current.FindResource("no") as string ?? "no";
    }

    public string ActivContract
    {
      get
      {
        return ContractState switch
        {
          EContractState.NotActive => Application.Current.FindResource("notActiveContract") as string ?? "not active contract",
          EContractState.Singend => Application.Current.FindResource("singnedContract") as string ?? "singned contract",
          EContractState.Active => Application.Current.FindResource("activeContract") as string ?? "active contract",
          EContractState.Canceled => Application.Current.FindResource("canceledContract") as string ?? "canceled contract",
          _ => Application.Current.FindResource("unknown") as string ?? "unknown",
        };
      }
    }

    public PropertyObject? GetPropertyObjectToRentalContract()
    {
      return DbController.GetPropertyObjectToRentalContractDB(this);
    }

    public string PropertyObjectName
    {
      get => GetPropertyObjectToRentalContract()?.Description ?? "";
    }
  }
}
