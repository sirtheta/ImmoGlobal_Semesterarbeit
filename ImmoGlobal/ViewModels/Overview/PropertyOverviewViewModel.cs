using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      PropertyCollection = new(DbController.GetAllPropertiesDB());
      OnPropertyChanged(nameof(PropertyCollection));
    }

    public ObservableCollection<Property>? PropertyCollection { get; set; }
  }
}
