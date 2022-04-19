﻿using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyOverviewViewModel : BaseViewModel
  {
    internal PropertyOverviewViewModel()
    {
      PropertyCollection = new ObservableCollection<Property>(DbController.GetPropertiesDB());
    }

    public ObservableCollection<Property>? PropertyCollection { get; set; }
  }
}
