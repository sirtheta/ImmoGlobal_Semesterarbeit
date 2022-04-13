using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectViewModel : BaseViewModel
  {

    public PropertyObjectViewModel(PropertyObject propertyObject)
    {
      PropertyObject = propertyObject;
    }

    public PropertyObject PropertyObject { get; set; }

    public ObservableCollection<Invoice>? InvoicesOfPropertyObject { get; set; }


    public ObservableCollection<RentalContract>? RentalContracsOfPropertyObject
    {
      get { return new ObservableCollection<RentalContract>(PropertyObject.GetRentalContractToObject()); }
    }


    /// <summary>
    /// return renter from activ Contract
    /// </summary>
    public Persona? Renter
    {
      get
      {
        return RentalContracsOfPropertyObject?.Where(x => x.ContractState == EContractState.Active).FirstOrDefault()?.GetRenter();
      }
    }
  }
}

