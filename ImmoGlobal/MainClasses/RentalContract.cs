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
    public int RentalContractId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime? RentEndDate { get; set; }
    public double RentPrice { get; set; }
    public double Deposit { get; set; }
    public int PersonaId { get; set; }
    public Persona? Persona { get; set; }
    public int ObjectId { get; set; }
    public Object? Object { get; set; }
    public EAdditionalCosts EAdditionalCosts { get; set; }
    public EContractState EContractState { get; set; }
  }
}
