﻿using ImmoGlobal.MainClasses;
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
        SetSideMenuPropertiesToNull();
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }

    private void SetSideMenuPropertiesToNull()
    {
      SelectedPersona = null;
    }

    internal Property? SelectedProperty { get; set; }
    internal PropertyObject? SelectedPropertyObject { get; set; }
    internal Persona? SelectedPersona { get; set; }

    /// <summary>
    /// sets the color of the menu bar icon
    /// </summary>
    private void SetMenuBarIconColor()
    {
      switch (_selectedViewModel)
      {
        case PropertyOverviewViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Red;
          MenuBarViewModel.BtnRenterColor = Brushes.Black;
          MenuBarViewModel.BtnCreditorColor = Brushes.Black;
          MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
          MenuBarViewModel.BtnInvoiceColor = Brushes.Black;
          MenuBarViewModel.BtnAccountColor = Brushes.Black;
          break;
        case RenterOverviewViewModel:
        case UpsertRenterViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Black;
          MenuBarViewModel.BtnRenterColor = Brushes.Red;
          MenuBarViewModel.BtnCreditorColor = Brushes.Black;
          MenuBarViewModel.BtnRentalContractColor = Brushes.Black;
          MenuBarViewModel.BtnInvoiceColor = Brushes.Black;
          MenuBarViewModel.BtnAccountColor = Brushes.Black;
          break;
        case CreditorOverviewViewModel:
        case UpsertCreditorViewModel:
          MenuBarViewModel.BtnPropertyColor = Brushes.Black;
          MenuBarViewModel.BtnRenterColor = Brushes.Black;
          MenuBarViewModel.BtnCreditorColor = Brushes.Red;
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
        case PropertyOverviewViewModel:
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
          SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
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
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        case PropertyObjectViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnObjectsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnRentalContractsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        case RenterOverviewViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Visible;
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnObjectsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnRentalContractsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          break;
        case CreditorOverviewViewModel:
          SideMenuViewModel.BtnNewPropertyVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPropertyObjectVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewRenterVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnInvoiceVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnObjectsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnRentalContractsVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewCreditorVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
          SideMenuViewModel.BtnNewRentalContractVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewAccountVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnNewPaymentRecordVisibility = Visibility.Collapsed;
          SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
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
          SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
          break;
      }
    }
  }
}
