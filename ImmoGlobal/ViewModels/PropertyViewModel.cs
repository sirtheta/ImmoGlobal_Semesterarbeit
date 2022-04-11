using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyViewModel : BaseViewModel
  {
    private static PropertyViewModel? instance = null;
    private static readonly object padlock = new();

    protected PropertyViewModel()
    {
      PropertyCollection = new ObservableCollection<Property>(DbController.GetPropertiesFromDb());
    }

    /// <summary>
    /// returns instance of class HomeViewModel
    /// </summary>
    public static PropertyViewModel GetInstance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new PropertyViewModel();
          }
          return instance;
        }
      }
    }

    private ObservableCollection<Property>? _propertyCollection;
    public ObservableCollection<Property>? PropertyCollection
    {
      get => _propertyCollection;
      set => _propertyCollection = value;
    }
  }
}
