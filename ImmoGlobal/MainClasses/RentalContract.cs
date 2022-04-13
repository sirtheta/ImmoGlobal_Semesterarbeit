using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  internal class RentalContract
  {
    public int RentalContractId { get; set; }
    public Persona? Renter { get; set; }
    public PropertyObject? PropertyObject { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime? RentEndDate { get; set; }
    public double Rent { get; set; }
    public bool Deposit { get; set; }
    public EContractState ContractState { get; set; }


    public string RenterFullName
    {
      get
      {
        return GetRenter()?.FullName ?? "";
      }
    }

    public Persona? GetRenter()
    {
      return DbController.GetRenter(this);
    }

    public string DepositString
    {
      get
      {
        return Deposit ? Application.Current.FindResource("yes") as string ?? "yes" :
          Application.Current.FindResource("no") as string ?? "no";
      }
    }
    public string ActivContract
    {
      get
      {
        return ContractState == EContractState.Active ? Application.Current.FindResource("yes") as string ?? "yes" :
          Application.Current.FindResource("no") as string ?? "no";
      }
    }
  }
}
