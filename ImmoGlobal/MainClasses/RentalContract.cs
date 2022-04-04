using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}
