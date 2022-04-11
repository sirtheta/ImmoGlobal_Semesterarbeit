using System.Windows.Input;
using ImmoGlobal.Commands;

namespace ImmoGlobal.ViewModels
{
  internal class MainViewModel : BaseViewModel
  {
    private BaseViewModel _selectedViewModel;
    private BaseViewModel _menuBarViewModel;
    private BaseViewModel _sideMenuViewModel;


    private static MainViewModel? instance = null;

    /// <summary>
    /// returns instance of class MainViewModel
    /// </summary>
    public static MainViewModel? GetInstance {
      get {
        return instance; 
      }
    }

    public MainViewModel(BaseViewModel viewModel, BaseViewModel menuBarViewModel, BaseViewModel sideMenuViewModel)
    {
      _selectedViewModel = viewModel;
      _menuBarViewModel = menuBarViewModel;
      _sideMenuViewModel = sideMenuViewModel;
      instance = this;
    }

    public BaseViewModel MenuBarViewModel
    {
      get { return _menuBarViewModel; }
      set
      {
        _menuBarViewModel = value;
        OnPropertyChanged();
      }
    }

    public BaseViewModel SideMenuViewModel
    {
      get { return _sideMenuViewModel; }
      set
      {
        _sideMenuViewModel = value;
        OnPropertyChanged();
      }
    }

    public BaseViewModel SelectedViewModel {
      get {
        return _selectedViewModel;
      }
      set {
        _selectedViewModel = value;
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }
  }
}
