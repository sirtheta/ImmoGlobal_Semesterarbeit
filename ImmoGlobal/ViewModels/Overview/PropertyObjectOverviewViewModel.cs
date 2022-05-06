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
      _selectedProperty.GetPropertyObjects().ToList().ForEach(po => PropertyObjectCollection.Add(po));
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

    public ObservableCollection<PropertyObject>? PropertyObjectCollection { get; set; }
    public string Housekeeper { get; set; }

    public string Description { get; set; }
  }
}
