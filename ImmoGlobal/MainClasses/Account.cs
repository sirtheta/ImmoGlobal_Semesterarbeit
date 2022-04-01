using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Account
  {
    public int AccountId { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccountDescription { get; set; }
    public double Balance { get; set; }
  }
}
