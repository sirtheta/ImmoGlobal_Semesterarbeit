using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.ObjectModel;
using System.Linq;


namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      RentalContracsOfPropertyObject = new(PropertyObject.GetRentalContractToPropertyObject());
      InvoicesOfPropertyObject = new(PropertyObject.GetInvoicesOfPropertyObject());
      RenterDetailsViewModel = new RenterDetailsViewModel(Renter);
      OnPropertyChanged(nameof(RenterDetailsViewModel));
      OnPropertyChanged(nameof(InvoicesOfPropertyObject));
      OnPropertyChanged(nameof(RentalContracsOfPropertyObject));
    }

    internal PropertyObjectViewModel(PropertyObject propertyObject)
    {
      PropertyObject = propertyObject;
    }

    public PropertyObject PropertyObject { get; set; }

    public ObservableCollection<Invoice>? InvoicesOfPropertyObject { get; set; }

    public ObservableCollection<RentalContract>? RentalContracsOfPropertyObject { get; set; }

    public RenterDetailsViewModel RenterDetailsViewModel { get; set; }


    /// <summary>
    /// return renter from activ Contract
    /// </summary>
    public Persona? Renter
    {
      get => RentalContracsOfPropertyObject?.Where(x => x.ContractState == EContractState.Active).FirstOrDefault()?.GetRenter();
    }
  }
}


