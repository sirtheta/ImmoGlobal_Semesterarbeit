using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertPropertyViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new property
    /// </summary>
    internal UpsertPropertyViewModel()
    {
      BtnDeleteVisibility = Visibility.Collapsed;
      _personas = new(DbController.GetAllPersonasDB());

      //set the title of the form
      FormTitel = Application.Current.TryFindResource("createNewProperty") as string ?? "create new property";
    }

    /// <summary>
    /// c'tor to edit an existing property
    /// </summary>
    /// <param name="property"></param>
    internal UpsertPropertyViewModel(Property property)
    {
      BtnDeleteVisibility = Visibility.Visible;
      _personas = new(DbController.GetAllPersonasDB());

      Housekeeper = property.GetHouskeeper();
      Property = property;
      Id = property.PropertyId;
      Description = property.Description ?? "";
      Address = property.Address;
      ZipCode = property.ZipCode.ToString();
      City = property.City;
      PropertyInsurance = property.PropertyInsurance;
      PersonInsurance = property.PersonInsurance;
      LiabilityInsurance = property.LiabilityInsurance;

      //set the title of the form
      FormTitel = (Application.Current.TryFindResource("property") as string ?? "property") + " " +
                   (Application.Current.TryFindResource("edit") as string ?? "edit");
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

    private Property? Property { get; set; }
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

    internal override void DeleteClicked(object obj)
    {
      if (Property.GetPropertyObjectsToProperty().Count == 0 && DbController.DeletePropertyDB(Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successDeleteProperty") as string ?? "Property deleted successfully", NotificationType.Success);
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorDeleteProperty") as string ?? "Cannot delete property with objects", MessageType.Error, MessageButtons.Ok);
      }
    }

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(ZipCode, out int zipCode) && zipCode < 1000 && zipCode > 9999)
      {
        ShowMessageBox(Application.Current.TryFindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
        return;
      }
      //Create Property
      if (Id == null && CreateProperty(zipCode))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddProperty") as string ?? "Property added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update Property
      else if (Id != null && UpdateProperty(zipCode, (int)Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdateProperty") as string ?? "Property added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorAddProperty") as string ?? "Error adding property", MessageType.Error, MessageButtons.Ok);
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
      Property.PropertyId = propertyId;
      Property.Description = _description;
      Property.Address = _address;
      Property.ZipCode = zipCode;
      Property.City = _city;
      Property.PropertyInsurance = _propertyInsurance;
      Property.PersonInsurance = _personInsurance;
      Property.LiabilityInsurance = _liabilityInsurance;
      Property.Housekeeper = _housekeeper;
      if (DbController.UpsertPropertyToDB(Property))
      {
        return true;
      }
      return false;
    }
  }
}
