using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyViewModel : BaseViewModel
  {
    internal PropertyViewModel()
    {
      PropertyCollection = new ObservableCollection<Property>(DbController.GetPropertiesFromDb());
    }

    private ObservableCollection<Property>? _propertyCollection;
    public ObservableCollection<Property>? PropertyCollection
    {
      get => _propertyCollection;
      set => _propertyCollection = value;
    }
  }
}
