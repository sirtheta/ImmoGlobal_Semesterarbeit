using ImmoGlobal.MainClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectViewModel : BaseViewModel
  {
    private ObservableCollection<PropertyObject>? _propertyObjectCollection;
    private string _housekeeper;
    private string _description;

    public PropertyObjectViewModel(List<PropertyObject> property, string housekeeper, string description)
    {
      _propertyObjectCollection = new ObservableCollection<PropertyObject>();
      property.ForEach(x => _propertyObjectCollection.Add(x));
      _housekeeper = housekeeper;
      _description = description;
    }

    public ObservableCollection<PropertyObject>? PropertyObjectCollection
    {
      get
      {
        return _propertyObjectCollection;
      }
      set
      {
        _propertyObjectCollection = value;
        OnPropertyChanged();
      }
    }
    public string Housekeeper
    {
      get
      {
        return _housekeeper;
      }
      set
      {
        _housekeeper = value;
        OnPropertyChanged();
      }
    }
    public string Description
    {
      get
      {
        return _description;
      }
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }
  }
}
