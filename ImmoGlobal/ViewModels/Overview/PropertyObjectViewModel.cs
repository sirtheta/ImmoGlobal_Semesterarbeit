using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class PropertyObjectViewModel : BaseViewModel
  {
    internal PropertyObjectViewModel(PropertyObject propertyObject)
    {
      PropertyObject = propertyObject;
      RenterDetailsViewModel = new RenterDetailsViewModel(Renter);
    }

    private Invoice? _selectedInvoice;
    public PropertyObject PropertyObject { get; set; }

    public ObservableCollection<Invoice>? InvoicesOfPropertyObject
    {
      get => new(PropertyObject.GetInvoicesOfPropertyObject());
    }

    public ObservableCollection<RentalContract>? RentalContracsOfPropertyObject
    {
      get => new(PropertyObject.GetRentalContractToObject());
    }

    public RenterDetailsViewModel RenterDetailsViewModel { get; set; }


    /// <summary>
    /// return renter from activ Contract
    /// </summary>
    public Persona? Renter
    {
      get => RentalContracsOfPropertyObject?.Where(x => x.ContractState == EContractState.Active).FirstOrDefault()?.GetRenter();
    }

    public Invoice? SelectedInvoice
    {
      get => _selectedInvoice;
      set
      {
        if (_selectedInvoice != value && MainWindowViewModelInstance != null)
        {
          _selectedInvoice = value;
          MainWindowViewModelInstance.SelectedInvoice = _selectedInvoice;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTwoWidth = 200;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditTextTwo =
             Application.Current.FindResource("editInvoice") as string ?? "Edit Invoice"; ;
          if (_selectedInvoice.DueDate < System.DateTime.Now && _selectedInvoice.InvoiceState == EInvoiceState.Released)
          {

            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Visible;
          }
          else
          {
            MainWindowViewModelInstance.SideMenuViewModel.BtnNewBillReminderVisibility = Visibility.Collapsed;
          }

        }
        OnPropertyChanged();
      }
    }
  }
}


