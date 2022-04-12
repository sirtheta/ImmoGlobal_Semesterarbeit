using ImmoGlobal.Database;
using ImmoGlobal.ViewModels;
using System;
using System.Configuration;
using System.Threading;
using System.Windows;

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
      PropertyViewModel propertyViewModel = new();

      MainWindow = new MainWindowView()
      {
        DataContext = new MainWindowViewModel(propertyViewModel)
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
