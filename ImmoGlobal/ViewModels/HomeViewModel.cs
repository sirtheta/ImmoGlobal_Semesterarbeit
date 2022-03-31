using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ImmoGlobal.Commands;


namespace ImmoGlobal.ViewModels
{
  internal class HomeViewModel : BaseViewModel
  {
    #region Singleton
    private static HomeViewModel? instance = null;
    private static readonly object padlock = new();

    protected HomeViewModel()
    {
    }

    /// <summary>
    /// returns instance of class HomeViewModel
    /// </summary>
    public static HomeViewModel GetInstance {
      get {
        lock (padlock) {
          if (instance == null) {
            instance = new HomeViewModel();
          }
          return instance;
        }
      }
    }
    #endregion

  }
}
