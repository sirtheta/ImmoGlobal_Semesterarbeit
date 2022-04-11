using System.Windows.Input;
using System.Windows.Media;
using ImmoGlobal.Commands;

namespace ImmoGlobal.ViewModels
{
  internal class MainViewModel : BaseViewModel
  {
    private BaseViewModel _selectedViewModel;
    private MenuBarViewModel _menuBarViewModel;
    private SideMenuViewModel _sideMenuViewModel;


    private static MainViewModel? instance = null;

    /// <summary>
    /// returns instance of class MainViewModel
    /// </summary>
    public static MainViewModel? GetInstance
    {
      get
      {
        return instance;
      }
    }

    public MainViewModel(BaseViewModel viewModel)
    {
      _selectedViewModel = viewModel;
      _menuBarViewModel = new MenuBarViewModel();
      _sideMenuViewModel = new SideMenuViewModel();
      instance = this;
    }

    public MenuBarViewModel MenuBarViewModel
    {
      get { return _menuBarViewModel; }
      set
      {
        _menuBarViewModel = value;
      }
    }

    public SideMenuViewModel SideMenuViewModel
    {
      get { return _sideMenuViewModel; }
      set
      {
        _sideMenuViewModel = value;
      }
    }

    public BaseViewModel SelectedViewModel
    {
      get
      {
        SetMenuBarIconColor();
        return _selectedViewModel;
      }
      set
      {
        _selectedViewModel = value;
        SetMenuBarIconColor();
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }

    
    private void SetMenuBarIconColor()
    {
      switch (_selectedViewModel)
      {
        case PropertyViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Red;    
          MenuBarViewModel.BtnPropertyObjectColor = Brushes.Black;
          MenuBarViewModel.BtnRenterColor         = Brushes.Black;
          MenuBarViewModel.BtnCreditorColor       = Brushes.Black;
          MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
          MenuBarViewModel.BtnInvoiceColor        = Brushes.Black;
          MenuBarViewModel.BtnAccountColor        = Brushes.Black;
          break;
        default:
          break;
      }
    }
  }
}
