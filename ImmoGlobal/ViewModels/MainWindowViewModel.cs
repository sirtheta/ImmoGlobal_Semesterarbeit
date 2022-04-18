using ImmoGlobal.MainClasses;
using System.Windows;
using System.Windows.Media;

namespace ImmoGlobal.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    private BaseViewModel _selectedViewModel;
    private MenuBarViewModel _menuBarViewModel;
    private SideMenuViewModel _sideMenuViewModel;


    private static MainWindowViewModel? instance = null;

    /// <summary>
    /// returns instance of class MainViewModel
    /// </summary>
    public static MainWindowViewModel? GetInstance
    {
      get
      {
        return instance;
      }
    }

    public MainWindowViewModel(BaseViewModel viewModel)
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
        SetSideMenuButtons();
        return _selectedViewModel;
      }
      set
      {
        _selectedViewModel = value;
        SetMenuBarIconColor();
        SetSideMenuButtons();
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }

    internal Property? SelectedProperty { get; set; }
    internal PropertyObject? SelectedPropertyObject { get; set; }
    internal Persona? SelectedPersona { get; set; }

    /// <summary>
    /// sets the color of the menu bar icon
    /// </summary>
    private void SetMenuBarIconColor()
    {
      MenuBarViewModel.BtnPropertyColor = Brushes.Black;
      MenuBarViewModel.BtnRenterColor = Brushes.Black;
      MenuBarViewModel.BtnCreditorColor = Brushes.Black;
      MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
      MenuBarViewModel.BtnInvoiceColor = Brushes.Black;
      MenuBarViewModel.BtnAccountColor = Brushes.Black;

      switch (_selectedViewModel)
      {
        case PropertyOverviewViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Red;
          break;
        case RenterOverviewViewModel:
        case UpsertRenterViewModel:
          MenuBarViewModel.BtnRenterColor = Brushes.Red;
          break;
        case CreditorOverviewViewModel:
        case UpsertCreditorViewModel:
          MenuBarViewModel.BtnCreditorColor = Brushes.Red;
          break;
        default:
          break;
      }
    }
    /// <summary>
    /// Sets the side menu buttons.
    /// </summary>
    private void SetSideMenuButtons()
    {
      SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewRenterVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnInvoiceVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnObjectsVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnRentalContractsVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnHousekeeperVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewHousekeeperVisibility = Visibility.Collapsed;

      switch (_selectedViewModel)
      {
        case PropertyOverviewViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Visible;
          break;
        case PropertyObjectOverviewViewModel:
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Visible;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          SideMenuViewModel.BtnHousekeeperVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewHousekeeperVisibility = Visibility.Visible;
          break;
        case PropertyObjectViewModel:
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Visible;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        case RenterOverviewViewModel:
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Visible;
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        case CreditorOverviewViewModel:
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        default:
          break;
      }
    }
  }
}
