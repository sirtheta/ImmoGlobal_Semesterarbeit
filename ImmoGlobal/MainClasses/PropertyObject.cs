using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class PropertyObject
  {
    public int PropertyObjectId { get; set; }
    public Property? Property { get; set; }
    public EPropertyObjectType ObjectType { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public double NumberOfRooms { get; set; }
    public double Area { get; set; }
    public int? NumberOfKeys { get; set; }
    public bool Fridge { get; set; }
    public bool Dishwasher { get; set; }
    public bool Stove { get; set; }
    public bool Oven { get; set; }
    public bool WashingMachine { get; set; }
    public bool Tumbler { get; set; }
    public ICollection<RentalContract>? RentalContracts { get; set; }
  }
}
