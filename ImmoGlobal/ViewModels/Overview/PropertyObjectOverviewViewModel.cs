using ImmoGlobal.MainClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectOverviewViewModel : BaseViewModel
  {
    internal PropertyObjectOverviewViewModel(List<PropertyObject> propertyObjects, Persona persona, string description)
    {
      _propertyObjectCollection = new ObservableCollection<PropertyObject>();
      propertyObjects.ForEach(po => _propertyObjectCollection.Add(po));
      _housekeeper = persona.FullName;
      _description = description;
      MainWindowViewModelInstance.SelectedPersona = persona;
    }

    private ObservableCollection<PropertyObject>? _propertyObjectCollection;
    private string _housekeeper;
    private string _description;

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
