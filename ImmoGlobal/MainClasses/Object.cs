using ImmoGlobal.MainClasses.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Object
  {
    public int ObjectId { get; set; }
    public int PropertyId { get; set; }
    public EObjectType ObjectType { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public double NumberOfRooms { get; set; }
    public double Surface { get; set; }
    public int? NumberOfKeys { get; set; }
    public bool Fridge { get; set; }
    public bool Dishwasher { get; set; }
    public bool Stove { get; set; }
    public bool Oven { get; set; }
    public bool WashingMachine { get; set; }
    public bool Tumbler { get; set; }

    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<RentalContract>? RentalContracts { get; set; }
  }
}
