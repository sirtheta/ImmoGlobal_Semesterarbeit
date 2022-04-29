using ImmoGlobal.MainClasses;

namespace ImmoGlobal.ViewModels
{
  internal class RenterDetailsViewModel : BaseViewModel
  {
    internal RenterDetailsViewModel(Persona renter)
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
