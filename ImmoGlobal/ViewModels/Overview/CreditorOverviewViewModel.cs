using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class CreditorOverviewViewModel : BaseViewModel
  {

    internal override void OnLoadedEvent(object obj)
    {
      CreditorCollection = new ObservableCollection<Persona>(DbController.GetAllCreditorsDB());
      SelectedCreditorDetailsViewModel = null;
      OnPropertyChanged(nameof(CreditorCollection));
    }

    public ObservableCollection<Persona> CreditorCollection { get; set; }
    public ObservableCollection<Invoice>? InvoiceCollection { get; set; }

    private Persona? _selectedCreditor;
    public Persona? SelectedCreditor
    {
      get => _selectedCreditor;
      set
      {
        if (_selectedCreditor != value && value != null && MainWindowViewModelInstance != null)
        {
          _selectedCreditor = value;
          SelectedCreditorDetailsViewModel = new CreditorDetailsViewModel(_selectedCreditor);
          InvoiceCollection = new(DbController.GetInvoiceToPersonaDB(_selectedCreditor));
          //notify view for changes
          OnPropertyChanged(nameof(InvoiceCollection));
          OnPropertyChanged(nameof(SelectedCreditorDetailsViewModel));

          MainWindowViewModelInstance.SelectedPersona = _selectedCreditor;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Visible;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Visible;
        }
        else
        {
          MainWindowViewModelInstance.SelectedPersona = _selectedCreditor;
          MainWindowViewModelInstance.SideMenuViewModel.BtnEditVisibility = Visibility.Collapsed;
          MainWindowViewModelInstance.SideMenuViewModel.BtnNewInvoiceVisibility = Visibility.Collapsed;
        }
        OnPropertyChanged();
      }
    }

    public CreditorDetailsViewModel? SelectedCreditorDetailsViewModel { get; set; }
  }
}
