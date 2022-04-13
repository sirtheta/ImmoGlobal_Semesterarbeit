using ImmoGlobal.MainClasses;
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
    private PropertyObject _propertyObject;

    public PropertyObjectViewModel(PropertyObject propertyObject)
    {
      _propertyObject = propertyObject;
    }
    
    public PropertyObject PropertyObject 
    { 
      get => _propertyObject; 
      set => _propertyObject = value; 
    }

    public ObservableCollection<Invoice>? InvoicesOfPropertyObject
    {
      get;
      set;
    }

    public ObservableCollection<RentalContract>? RentalContracsOfPropertyObject
    {
      get { return new ObservableCollection<RentalContract>(PropertyObject.GetRentalContractToObject()); }
      set { _ = value; }
    }
  }
}
