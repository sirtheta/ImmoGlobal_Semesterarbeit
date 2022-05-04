using ImmoGlobal.MainClasses;
using System.Collections.Generic;
using System.Linq;
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
    private readonly static object padlock = new();

    /// <summary>
    /// returns instance of class MainViewModel
    /// </summary>
    public static MainWindowViewModel GetInstance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new MainWindowViewModel();
          }
          return instance;
        }
      }
    }

    /// <summary>
    /// c'tor for the mainWindowViewModel
    /// </summary>
    private MainWindowViewModel()
    {
      NavigationStore = new();
      MenuBarViewModel = new MenuBarViewModel();
      SideMenuViewModel = new SideMenuViewModel();
      instance = this;
    }

    /// <summary>
    /// the selected menubar viewmodel
    /// </summary>
    public MenuBarViewModel MenuBarViewModel
    {
      get => _menuBarViewModel;
      set
      {
        _menuBarViewModel = value;
      }
    }

    /// <summary>
    /// the selceted side menu view model
    /// </summary>
    public SideMenuViewModel SideMenuViewModel
    {
      get => _sideMenuViewModel;
      set
      {
        _sideMenuViewModel = value;
      }
    }

    #region Navigation
    internal List<BaseViewModel> NavigationStore { get; set; }
    internal void NavigateBack()
    {
      if (NavigationStore.Count > 1)
      {
        // do not add the current viewmodel to the navigation store when naivgating back
        DoNotAddViewModel = true;
        var lastSelectedViewModel = NavigationStore.Last();
        SelectedViewModel = lastSelectedViewModel;
        NavigationStore.Remove(lastSelectedViewModel);
      }
      CheckForNavigation();
    }

    /// <summary>
    /// check if there is a viewmodel in the store to navigate to
    /// </summary>
    private void CheckForNavigation()
    {
      if (NavigationStore.Count < 2)
      {
        SideMenuViewModel.CanNavigate = false;
      }
      else
      {
        SideMenuViewModel.CanNavigate = true;
      }
    }

    /// <summary>
    /// adds the last selected viewmodel to the navigation store
    /// </summary>
    /// <param name="selectedViewModel"></param>
    private void AddPageToNavigation(BaseViewModel selectedViewModel)
    {
      if (_selectedViewModel is not LoginViewModel && !DoNotAddViewModel)
      {
        NavigationStore.Add(selectedViewModel);
      }
      // next view can be added to the navigation store
      DoNotAddViewModel = false;
    }

    internal bool DoNotAddViewModel { get; set; } = false;
    #endregion

    /// <summary>
    /// sets the selected viewmodel
    /// </summary>
    public BaseViewModel SelectedViewModel
    {
      get
      {
        SetMenuBarIconColor();
        SetSideMenuButtons();
        //set side menu edit button to standard text, it can be changed in the viewmodel as needed
        SideMenuViewModel.BtnEditText = Application.Current.FindResource("btnEdit") as string ?? "edit";
        return _selectedViewModel;
      }
      set
      {
        AddPageToNavigation(_selectedViewModel);
        CheckForNavigation();
        _selectedViewModel = value;
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }

    //properties to access in different viewmodels
    internal Property? SelectedProperty { get; set; }
    internal PropertyObject? SelectedPropertyObject { get; set; }
    internal Persona? SelectedPersona { get; set; }
    internal Invoice? SelectedInvoice { get; set; }
    internal Account? SelectedAccount { get; set; }
    internal RentalContract? SelectedRentalContract { get; set; }
    internal PaymentRecord? SelectedPaymentRecord { get; set; }
    internal BillReminder? SelectedBillReminder { get; set; }
    internal User LogedInUser { get; set; }

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
        case PropertyObjectOverviewViewModel:
        case PropertyObjectViewModel:
        case UpsertPropertyObjectViewModel:
        case UpsertPropertyViewModel:
        case UpsertHousekeeperViewModel:
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
        case RentalContractsOverviewViewModel:
        case UpsertRentalContractViewModel:
          MenuBarViewModel.BtnRentalContractColor = Brushes.Red;
          break;
        case InvoicesOverviewViewModel:
        case UpsertInvoiceViewModel:
        case UpsertBillReminderViewModel:
          MenuBarViewModel.BtnInvoiceColor = Brushes.Red;
          break;
        case AccountsOverviewViewModel:
        case UpsertPaymentRecordViewModel:
        case UpsertAccountViewModel:
          MenuBarViewModel.BtnAccountColor = Brushes.Red;
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// Sets the visibility of the side menu buttons.
    /// </summary>
    private void SetSideMenuButtons()
    {
      SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewRenterVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
      SideMenuViewModel.BtnEditTwoVisibility = Visibility.Collapsed;
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
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Visible;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
          break;
        case RenterOverviewViewModel:
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Visible;
          break;
        case CreditorOverviewViewModel:
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Visible;
          break;
        case RentalContractsOverviewViewModel:
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Visible;
          break;
        case InvoicesOverviewViewModel:
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
          break;
        case AccountsOverviewViewModel:
          SideMenuViewModel.BtnNewAccountVisibility = Visibility.Visible;
          break;
        default:
          break;
      }
    }
  }
}
