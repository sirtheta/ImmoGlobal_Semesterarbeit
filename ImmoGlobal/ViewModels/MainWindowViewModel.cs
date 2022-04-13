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
    /// <summary>
    /// sets the color of the menu bar icon
    /// </summary>
    private void SetMenuBarIconColor()
    {
      switch (_selectedViewModel)
      {
        case PropertyViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Red;
          MenuBarViewModel.BtnRenterColor = Brushes.Black;
          MenuBarViewModel.BtnCreditorColor = Brushes.Black;
          MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
          MenuBarViewModel.BtnInvoiceColor = Brushes.Black;
          MenuBarViewModel.BtnAccountColor = Brushes.Black;
          break;
        default:
          MenuBarViewModel.BtnPropertyColor = Brushes.Black;
          MenuBarViewModel.BtnRenterColor = Brushes.Black;
          MenuBarViewModel.BtnCreditorColor = Brushes.Black;
          MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
          MenuBarViewModel.BtnInvoiceColor = Brushes.Black;
          MenuBarViewModel.BtnAccountColor = Brushes.Black;
          break;
      }
    }
    /// <summary>
    /// Sets the side menu buttons.
    /// </summary>
    private void SetSideMenuButtons()
    {
      switch (_selectedViewModel)
      {
        case PropertyViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Visible;
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
          break;
        case PropertyObjectOverviewViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Visible;
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
          break;
        case PropertyObjectViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Visible;
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnObjectsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnRentalContractsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
          break;
        default:
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
          break;
      }
    }
  }
}
