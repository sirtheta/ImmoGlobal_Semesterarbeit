using ImmoGlobal.MainClasses.State;
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
    [Key]
    public int Id { get; set; }
    public DateOnly RentStartDate { get; set; }
    public DateOnly? RentEndDate { get; set; }
    public double RentPrice { get; set; }
    public double Deposit { get; set; }
    internal Persona? Persona { get; set; }
    internal Object? Object { get; set; }
    internal EAdditionalCosts EAdditionalCosts { get; set; }
    internal EContractState EContractState { get; set; }
  }
}
