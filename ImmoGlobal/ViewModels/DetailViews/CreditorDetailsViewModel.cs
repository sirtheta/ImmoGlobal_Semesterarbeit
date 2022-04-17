using ImmoGlobal.MainClasses;

namespace ImmoGlobal.ViewModels
{
  internal class CreditorDetailsViewModel : BaseViewModel
  {
    public CreditorDetailsViewModel(Persona creditor)
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
