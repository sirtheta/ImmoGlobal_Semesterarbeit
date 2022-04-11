using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ImmoGlobal.Database;
using ImmoGlobal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ImmoGlobal
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      CheckDatabase();
      
      SetLanguageDictionary();
      PropertyViewModel propertyViewModel = PropertyViewModel.GetInstance;
      MenuBarViewModel menuBarViewModel = new();
      SideMenuViewModel sideMenuViewModel = new();

      MainWindow = new MainWindow()
      {
        DataContext = new MainViewModel(propertyViewModel)
      };
      MainWindow.Show();

      base.OnStartup(e);
    }

    private static void CheckDatabase()
    {
      string? _delete = ConfigurationManager.AppSettings["DropDatabase"];
      string? _seed = ConfigurationManager.AppSettings["SeedDatabase"];
      using var context = new ImmoGlobalContext();
      if (_delete != null && bool.Parse(_delete))
      {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (_seed != null && bool.Parse(_seed))
        {
          DatabaseSeeder.CreateTestEntries();
        }
      }
      else
      {
        context.Database.EnsureCreated();
      }
    }
    private void SetLanguageDictionary()
    {
      ResourceDictionary dict = new();
      switch (Thread.CurrentThread.CurrentCulture.ToString())
      {
        case "de-CH":
          dict.Source = new Uri("..\\Resources\\StringResources.de-DE.xaml", UriKind.Relative);
          break;
        case "de-DE":
          dict.Source = new Uri("..\\Resources\\StringResources.de-DE.xaml", UriKind.Relative);
          break;
        default:
          dict.Source = new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative);
          break;
      }
      Resources.MergedDictionaries.Add(dict);
    }
  }
}
