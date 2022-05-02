using ImmoGlobal.Helpers;
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

      MainWindow = new MainWindowView()
      {
        DataContext = MainWindowViewModel.GetInstance
      };
      
      MainWindowViewModel.GetInstance.SelectedViewModel = new LoginViewModel();
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
      dict.Source = Thread.CurrentThread.CurrentCulture.ToString() switch
      {
        "de-CH" => new Uri("..\\Resources\\StringResources.de-DE.xaml", UriKind.Relative),
        "de-DE" => new Uri("..\\Resources\\StringResources.de-DE.xaml", UriKind.Relative),
        _ => new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative),
      };
      Resources.MergedDictionaries.Add(dict);
    }
  }
}
