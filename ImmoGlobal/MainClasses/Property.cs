using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Property
  {
    public int PropertyId { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? PropertyInsurance { get; set; }
    public string? PersonInsurance { get; set; }
    public string? LiabilityInsurance { get; set; }
    public Persona? Housekeeper { get; set; }
    public ICollection<Object>? Objects { get; set; }
  }
}
