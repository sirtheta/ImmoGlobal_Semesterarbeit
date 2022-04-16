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
      RenterDetailsViewModel = new RenterDetailsViewModel(Renter);
    }

    public PropertyObject PropertyObject { get; set; }

    public ObservableCollection<Invoice>? InvoicesOfPropertyObject
    {
      get
      {
        var invoices = new List<Invoice>();
        foreach (var item in PropertyObject.GetInvoicePositions())
        {
          invoices.Add(item.GetInvoiceToInvoicePosition());
        }
        return new ObservableCollection<Invoice>(invoices.DistinctBy(p => p.InvoiceId));
      }
    }

    public ObservableCollection<RentalContract>? RentalContracsOfPropertyObject
    {
      get => new(PropertyObject.GetRentalContractToObject());
    }

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

