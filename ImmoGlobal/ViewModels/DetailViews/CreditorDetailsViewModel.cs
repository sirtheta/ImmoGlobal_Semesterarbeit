using ImmoGlobal.MainClasses;

namespace ImmoGlobal.ViewModels
{
  internal class CreditorDetailsViewModel : BaseViewModel
  {
    internal CreditorDetailsViewModel(Persona creditor)
    {
      Creditor = creditor;
    }

    private Persona? _creditor;
    public Persona? Creditor
    {
      get => _creditor;
      set
      {
        _creditor = value;
        OnPropertyChanged();
      }
    }
  }
}
