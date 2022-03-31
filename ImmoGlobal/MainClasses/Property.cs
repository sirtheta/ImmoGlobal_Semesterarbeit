using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Property
  {
    [Key]
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public int NumberOfObjects { get; set; }
    public string? Insurance { get; set; }
    public string? PersonInsurance { get; set; }
    public string? LiabilityInsurance { get; set; }
    internal Persona? Housekeeper { get; set; }
  }
}
