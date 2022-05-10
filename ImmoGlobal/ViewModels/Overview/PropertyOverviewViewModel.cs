using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyOverviewViewModel : BaseViewModel
  {
    internal override void OnLoadedEvent(object obj)
    {
      var loadProps = new Task(LoadProperties);
      loadProps.Start();
      OnPropertyChanged(nameof(PropertyCollection));
    }

    public ObservableCollection<Property>? PropertyCollection { get; set; }

    private void LoadProperties()
    {
      PropertyCollection = new(DbController.GetAllPropertiesDB());
      OnPropertyChanged(nameof(PropertyCollection));
    }
  }
}
