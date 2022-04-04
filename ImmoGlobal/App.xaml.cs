﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
      HomeViewModel homeViewModel = HomeViewModel.GetInstance;

      MainWindow = new MainWindow()
      {
        DataContext = new MainViewModel(homeViewModel)
      };
      MainWindow.Show();

      base.OnStartup(e);

      var _delete = false;
      
      using var context = new ImmoGlobalContext();
      if (_delete)
      {
        context.Database.EnsureDeleted();        
        context.Database.EnsureCreated();
        DatabaseSeeder.CreateTestEntries();
      }
    }
  }
}
