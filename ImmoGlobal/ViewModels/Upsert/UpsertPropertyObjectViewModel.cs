using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Collections.Generic;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertPropertyObjectViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new property object
    /// </summary>
    /// <param name="selectedProperty"></param>
    internal UpsertPropertyObjectViewModel(Property selectedProperty)
    {
      BtnDeleteVisibility = Visibility.Collapsed;

      Property = selectedProperty;

      //set the title of the form
      FormTitel =
        (Application.Current.TryFindResource("newPropertyObjectFor") as string ?? "new property object for") + " " +
        (Application.Current.TryFindResource("property") as string ?? "property") +
        " " + _property.Description + " " +
        (Application.Current.TryFindResource("create") as string ?? "create");
    }

    /// <summary>
    /// c'tor to edit an existing property object
    /// </summary>
    /// <param name="selectedProperty"></param>
    /// <param name="propertyObject"></param>
    internal UpsertPropertyObjectViewModel(Property selectedProperty, PropertyObject propertyObject)
    {
      BtnDeleteVisibility = Visibility.Visible;

      PropertyObject = propertyObject;
      Id = propertyObject.PropertyObjectId;
      Property = selectedProperty;
      Description = propertyObject.Description;
      ObjectType = propertyObject.ObjectType;
      Location = propertyObject.Location;
      NumberOfRooms = propertyObject.NumberOfRooms.ToString();
      Area = propertyObject.Area.ToString();
      NumberOfKeys = propertyObject.NumberOfKeys?.ToString() ?? "";
      Fridge = propertyObject.Fridge;
      Dishwasher = propertyObject.Dishwasher;
      Stove = propertyObject.Stove;
      Oven = propertyObject.Oven;
      WashingMachine = propertyObject.WashingMachine;
      Tumbler = propertyObject.Tumbler;

      //set the title of the form
      FormTitel =
        (Application.Current.TryFindResource("propertyObject") as string ?? "object") + " " +
        " " + propertyObject.Description + " " +
        (Application.Current.TryFindResource("edit") as string ?? "edit");
    }

    private string? _description;
    private EPropertyObjectType _objectType;
    private Property _property;
    private string _location;
    private string _numberOfRooms;
    private string _area;
    private string _numberOfKeys;
    private bool _fridge;
    private bool _dishwasher;
    private bool _stove;
    private bool _oven;
    private bool _washingMachine;
    private bool _tumbler;

    private PropertyObject PropertyObject { get; set; }
    public string? Description
    {
      get => _description;
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }

    public EPropertyObjectType ObjectType
    {
      get => _objectType;
      set
      {
        _objectType = value;
        OnPropertyChanged();
      }
    }

    public Dictionary<EPropertyObjectType, string> EPropertyObjectTypeWithCaptions { get; } =
    new Dictionary<EPropertyObjectType, string>()
    {
          {EPropertyObjectType.House,     Application.Current.TryFindResource("house") as string ?? "house" },
          {EPropertyObjectType.Apartment, Application.Current.TryFindResource("apartment") as string ?? "apartment"},
          {EPropertyObjectType.Room,      Application.Current.TryFindResource("room") as string ?? "room"},
          {EPropertyObjectType.Garage,    Application.Current.TryFindResource("garage") as string ?? "garage"},
          {EPropertyObjectType.Office,    Application.Current.TryFindResource("office") as string ?? "office"},
          {EPropertyObjectType.Parking,   Application.Current.TryFindResource("parking") as string ?? "parking"},
    };

    public Property Property
    {
      get => _property;
      set
      {
        _property = value;
        OnPropertyChanged();
      }
    }

    public string Location
    {
      get => _location;
      set
      {
        _location = value;
        OnPropertyChanged();
      }
    }

    public string NumberOfRooms
    {
      get => _numberOfRooms;
      set
      {
        _numberOfRooms = value;
        OnPropertyChanged();
      }
    }

    public string Area
    {
      get => _area;
      set
      {
        _area = value;
        OnPropertyChanged();
      }
    }

    public string NumberOfKeys
    {
      get => _numberOfKeys;
      set
      {
        _numberOfKeys = value;
        OnPropertyChanged();
      }
    }

    public bool Fridge
    {
      get => _fridge;
      set
      {
        _fridge = value;
        OnPropertyChanged();
      }
    }

    public bool Dishwasher
    {
      get => _dishwasher;
      set
      {
        _dishwasher = value;
        OnPropertyChanged();
      }
    }

    public bool Stove
    {
      get => _stove;
      set
      {
        _stove = value;
        OnPropertyChanged();
      }
    }

    public bool Oven
    {
      get => _oven;
      set
      {
        _oven = value;
        OnPropertyChanged();
      }
    }

    public bool WashingMachine
    {
      get => _washingMachine;
      set
      {
        _washingMachine = value;
        OnPropertyChanged();
      }
    }

    public bool Tumbler
    {
      get => _tumbler;
      set
      {
        _tumbler = value;
        OnPropertyChanged();
      }
    }

    internal override void DeleteClicked(object obj)
    {
      if (DbController.DeletePropertyObjcetDB(Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successDeletePropertyObject") as string ?? "Property object deleted successfully", NotificationType.Success);
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorDeletePropertyObject") as string ?? "Cannot delete property with objects", MessageType.Error, MessageButtons.Ok);
      }
    }

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(Area, out double area))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorArea") as string ?? "Please enter a valid number for area", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(NumberOfRooms, out double numberOfRooms))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorRooms") as string ?? "Please enter a valid number for rooms", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(NumberOfKeys, out int numberOfKeys))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorKeys") as string ?? "Please enter a valid number for keys", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (Id == null && CreatePropertyObject(numberOfRooms, area, numberOfKeys))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddProperty") as string ?? "Property added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else if (Id != null && UpdatePropertyObject(numberOfRooms, area, numberOfKeys, (int)Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdatePropertyObject") as string ?? "Property added successfully", NotificationType.Success);
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
          !string.IsNullOrEmpty(Location) &&
          !string.IsNullOrEmpty(NumberOfRooms) &&
          !string.IsNullOrEmpty(Area) &&
          !string.IsNullOrEmpty(NumberOfKeys))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Create a new property object
    /// </summary>
    /// <param name="numberOfRooms"></param>
    /// <param name="area"></param>
    /// <param name="numberOfKeys"></param>
    /// <returns></returns>
    private bool CreatePropertyObject(double numberOfRooms, double area, int numberOfKeys)
    {
      if (DbController.UpsertPropertyObjectDB(new PropertyObject()
      {
        Property = _property,
        Description = _description,
        ObjectType = _objectType,
        Location = _location,
        NumberOfRooms = numberOfRooms,
        Area = area,
        NumberOfKeys = numberOfKeys,
        Fridge = _fridge,
        Dishwasher = _dishwasher,
        Stove = _stove,
        Oven = _oven,
        WashingMachine = _washingMachine,
        Tumbler = _tumbler,
      }))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// update property object
    /// </summary>
    /// <param name="numberOfRooms"></param>
    /// <param name="area"></param>
    /// <param name="numberOfKeys"></param>
    /// <param name="propertyObjectId"></param>
    /// <returns></returns>
    private bool UpdatePropertyObject(double numberOfRooms, double area, int numberOfKeys, int propertyObjectId)
    {
      PropertyObject.PropertyObjectId = propertyObjectId;
      PropertyObject.Property = _property;
      PropertyObject.Description = _description;
      PropertyObject.ObjectType = _objectType;
      PropertyObject.Location = _location;
      PropertyObject.NumberOfRooms = numberOfRooms;
      PropertyObject.Area = area;
      PropertyObject.NumberOfKeys = numberOfKeys;
      PropertyObject.Fridge = _fridge;
      PropertyObject.Dishwasher = _dishwasher;
      PropertyObject.Stove = _stove;
      PropertyObject.Oven = _oven;
      PropertyObject.WashingMachine = _washingMachine;
      PropertyObject.Tumbler = _tumbler;

      if (DbController.UpsertPropertyObjectDB(PropertyObject))
      {
        return true;
      }
      return false;
    }
  }
}
