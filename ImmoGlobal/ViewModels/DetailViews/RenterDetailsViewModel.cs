using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.ViewModels
{
  internal class RenterDetailsViewModel : BaseViewModel
  {
    public RenterDetailsViewModel(Persona renter)
    {
      Renter = renter;
    }

    private Persona? _renter;
    public Persona? Renter
    {
      get => _renter;
      set
      {
        _renter = value;
        OnPropertyChanged();
      }
    }
  }
}
