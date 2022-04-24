using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertPropertyViewModel : BaseViewModel
  {
    public UpsertPropertyViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      BtnDeleteVisibility = Visibility.Collapsed;
      _personas = new(DbController.GetAllPersonasDB());
      FormTitel = Application.Current.FindResource("createNewProperty") as string ?? "create new property";
    }

    public UpsertPropertyViewModel(Property property)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      BtnDelete = new RelayCommand<object>(DeleteClicked);
      BtnDeleteVisibility = Visibility.Visible;
      _personas = new(DbController.GetAllPersonasDB());

      Housekeeper = property.GetHouskeeper();
      Property = property;
      PropertyId = property.PropertyId;
      Description = property.Description ?? "";
      Address = property.Address;
      ZipCode = property.ZipCode.ToString();
      City = property.City;
      PropertyInsurance = property.PropertyInsurance;
      PersonInsurance = property.PersonInsurance;
      LiabilityInsurance = property.LiabilityInsurance;

      FormTitel = (Application.Current.FindResource("property") as string ?? "property") + " " +
                   (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string _description;
    private string _address;
    private string _zipCode;
    private string _city;
    private string _propertyInsurance;
    private string _personInsurance;
    private string _liabilityInsurance;
    private Persona? _housekeeper;
    private ObservableCollection<Persona> _personas;

    public string FormTitel { get; set; }

    private Property? Property { get; set; }
    private int? PropertyId { get; set; }
    public string Description
    {
      get => _description;
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }
    public string Address
    {
      get => _address;
      set
      {
        _address = value;
        OnPropertyChanged();
      }
    }
    public string ZipCode
    {
      get => _zipCode;
      set
      {
        _zipCode = value;
        OnPropertyChanged();
      }
    }
    public string City
    {
      get => _city;
      set
      {
        _city = value;
        OnPropertyChanged();
      }
    }
    public string PropertyInsurance
    {
      get => _propertyInsurance;
      set
      {
        _propertyInsurance = value;
        OnPropertyChanged();
      }
    }
    public string PersonInsurance
    {
      get => _personInsurance;
      set
      {
        _personInsurance = value;
        OnPropertyChanged();
      }
    }
    public string LiabilityInsurance
    {
      get => _liabilityInsurance;
      set
      {
        _liabilityInsurance = value;
        OnPropertyChanged();
      }
    }
    public Persona? Housekeeper
    {
      get => _housekeeper;
      set
      {
        _housekeeper = value;
        OnPropertyChanged();
      }
    }
    public ObservableCollection<Persona> PersonaCollection
    {
      get => _personas;
      set
      {
        _personas = value;
        OnPropertyChanged();
      }
    }

    public ICommand BtnSave
    {
      get;
      private set;
    }
    public ICommand? BtnDelete
    {
      get;
      private set;
    }

    public Visibility BtnDeleteVisibility { get; set; }

    private void DeleteClicked(object obj)
    {
      if (Property.GetPropertyObjects().Count == 0 && DbController.DeletePropertyDB(PropertyId))
      {
        ShowNotification("Success", Application.Current.FindResource("successDeleteProperty") as string ?? "Property deleted successfully", NotificationType.Success);
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorDeleteProperty") as string ?? "Cannot delete property with objects", MessageType.Error, MessageButtons.Ok);
      }
    }

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(ZipCode, out int zipCode) && zipCode < 1000 && zipCode > 9999)
      {
        ShowMessageBox(Application.Current.FindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
        return;
      }
      //Create Property
      if (PropertyId == null && CreateProperty(zipCode))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddProperty") as string ?? "Property added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Property
      else if (PropertyId != null && UpdateProperty(zipCode, (int)PropertyId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateProperty") as string ?? "Property added successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddProperty") as string ?? "Error adding property", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (!string.IsNullOrEmpty(Description) &&
          !string.IsNullOrEmpty(Address) &&
          !string.IsNullOrEmpty(City) &&
          !string.IsNullOrEmpty(PropertyInsurance) &&
          !string.IsNullOrEmpty(PersonInsurance) &&
          !string.IsNullOrEmpty(LiabilityInsurance) &&
          Housekeeper != null)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Create new Property
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns></returns>
    private bool CreateProperty(int zipCode)
    {
      if (DbController.UpsertPropertyToDB(new Property()
      {
        Description = _description,
        Address = _address,
        ZipCode = zipCode,
        City = _city,
        PropertyInsurance = _propertyInsurance,
        PersonInsurance = _personInsurance,
        LiabilityInsurance = _liabilityInsurance,
        Housekeeper = _housekeeper
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// update Property
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    private bool UpdateProperty(int zipCode, int propertyId)
    {
      if (DbController.UpsertPropertyToDB(new Property()
      {
        PropertyId = propertyId,
        Description = _description,
        Address = _address,
        ZipCode = zipCode,
        City = _city,
        PropertyInsurance = _propertyInsurance,
        PersonInsurance = _personInsurance,
        LiabilityInsurance = _liabilityInsurance,
        Housekeeper = _housekeeper
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Sets all properties to null
    /// </summary>
    private void ClearValues()
    {
      Description = string.Empty;
      Address = string.Empty;
      ZipCode = string.Empty;
      City = string.Empty;
      PropertyInsurance = string.Empty;
      PersonInsurance = string.Empty;
      LiabilityInsurance = string.Empty;
      Housekeeper = null;
    }
  }
}
