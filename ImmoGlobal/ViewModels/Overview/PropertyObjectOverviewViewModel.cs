using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      PropertyObjectCollection = new();
      var propObj = _selectedProperty.GetPropertyObjectsToProperty();
      propObj.ToList().ForEach(po => PropertyObjectCollection.Add(po));
      PropertyId = _selectedProperty.PropertyId;
      Housekeeper = _selectedProperty.GetHouskeeper().FullName;
      Description = _selectedProperty.Description;
      OnPropertyChanged(nameof(PropertyObjectCollection));
      OnPropertyChanged(nameof(Description));
      OnPropertyChanged(nameof(Housekeeper));
    }

    internal PropertyObjectOverviewViewModel()
    {
      _selectedProperty = MainWindowViewModelInstance.SelectedProperty;
    }

    private readonly Property _selectedProperty;
    private int _propertyId;
    public ObservableCollection<PropertyObject>? PropertyObjectCollection { get; set; }
    public string Housekeeper { get; set; }

    public string Description { get; set; }

    public int PropertyId
    {
      get => _propertyId;
      set
      {
        _propertyId = value;
        OnPropertyChanged();
      }
    }
  }
}
