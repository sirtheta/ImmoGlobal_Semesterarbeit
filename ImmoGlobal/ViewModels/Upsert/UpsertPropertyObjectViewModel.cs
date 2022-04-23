using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertPropertyObjectViewModel : BaseViewModel
  {
    public UpsertPropertyObjectViewModel(Property selectedProperty)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      BtnDeleteVisibility = Visibility.Collapsed;

      _property = selectedProperty;
      FormTitel =
        (Application.Current.FindResource("newPropertyObjectFor") as string ?? "new property object for") + " " +
        (Application.Current.FindResource("property") as string ?? "property") +
        " " + _property.Description + " " +
        (Application.Current.FindResource("create") as string ?? "create");
    }

    public UpsertPropertyObjectViewModel(Property selectedProperty, PropertyObject propertyObject)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      BtnDelete = new RelayCommand<object>(DeleteClicked);
      BtnDeleteVisibility = Visibility.Visible;
      PropertyObjectId = propertyObject.PropertyObjectId;
      _property = selectedProperty;
      Description = propertyObject.Description;
      ObjectType = propertyObject.ObjectType;
      Location = propertyObject.Location;
      NumberOfRooms = propertyObject.NumberOfRooms.ToString();
      Area = propertyObject.Area.ToString();
      NumberOfKeys = propertyObject.NumberOfKeys.ToString();
      Fridge = propertyObject.Fridge;
      Dishwasher = propertyObject.Dishwasher;
      Stove = propertyObject.Stove;
      Oven = propertyObject.Oven;
      WashingMachine = propertyObject.WashingMachine;
      Tumbler = propertyObject.Tumbler;
      FormTitel =
        (Application.Current.FindResource("propertyObject") as string ?? "object") + " " +
        " " + propertyObject.Description + " " +
        (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string? _description;
    private EPropertyObjectType _objectType;
    private Property _property;
    private string? _location;
    private string? _numberOfRooms;
    private string? _area;
    private string? _numberOfKeys;
    private bool _fridge;
    private bool _dishwasher;
    private bool _stove;
    private bool _oven;
    private bool _washingMachine;
    private bool _tumbler;

    public string FormTitel { get; set; }

    private int? PropertyObjectId { get; set; }

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
          {EPropertyObjectType.House,     Application.Current.FindResource("house") as string ?? "house" },
          {EPropertyObjectType.Apartment, Application.Current.FindResource("apartment") as string ?? "apartment"},
          {EPropertyObjectType.Room,      Application.Current.FindResource("room") as string ?? "room"},
          {EPropertyObjectType.Garage,    Application.Current.FindResource("garage") as string ?? "garage"},
          {EPropertyObjectType.Office,    Application.Current.FindResource("office") as string ?? "office"},
          {EPropertyObjectType.Parking,   Application.Current.FindResource("parking") as string ?? "parking"},
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

    public string? Location
    {
      get => _location;
      set
      {
        _location = value;
        OnPropertyChanged();
      }
    }

    public string? NumberOfRooms
    {
      get => _numberOfRooms;
      set
      {
        _numberOfRooms = value;
        OnPropertyChanged();
      }
    }

    public string? Area
    {
      get => _area;
      set
      {
        _area = value;
        OnPropertyChanged();
      }
    }

    public string? NumberOfKeys
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
      if (DbController.DeletePropertyObjcetDB(PropertyObjectId))
      {
        ShowNotification("Success", Application.Current.FindResource("successDeletePropertyObject") as string ?? "Property object deleted successfully", NotificationType.Success);
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorDeletePropertyObject") as string ?? "Cannot delete property with objects", MessageType.Error, MessageButtons.Ok);
      }
    }

    private void SaveClicked(object obj)
    {
      if (string.IsNullOrEmpty(Description) ||
          string.IsNullOrEmpty(Location) ||
          string.IsNullOrEmpty(NumberOfRooms) ||
          string.IsNullOrEmpty(Area) ||
          string.IsNullOrEmpty(NumberOfKeys))
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(Area, out double area))
      {
        ShowMessageBox(Application.Current.FindResource("errorArea") as string ?? "Please enter a valid number for area", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(NumberOfRooms, out double numberOfRooms))
      {
        ShowMessageBox(Application.Current.FindResource("errorRooms") as string ?? "Please enter a valid number for rooms", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(NumberOfKeys, out int numberOfKeys))
      {
        ShowMessageBox(Application.Current.FindResource("errorKeys") as string ?? "Please enter a valid number for keys", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (PropertyObjectId == null && CreatePropertyObject(numberOfRooms, area, numberOfKeys))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddProperty") as string ?? "Property added successfully", NotificationType.Success);
        ClearValues();
      }
      else if (PropertyObjectId != null && UpdatePropertyObject(numberOfRooms, area, numberOfKeys, (int)PropertyObjectId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdatePropertyObject") as string ?? "Property added successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddProperty") as string ?? "Error adding property", MessageType.Error, MessageButtons.Ok);
      }
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
      if (DbController.UpsertPropertyObjectDB(new PropertyObject()
      {
        PropertyObjectId = propertyObjectId,
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
    /// Sets all properties to null
    /// </summary>
    private void ClearValues()
    {
      Description = null;
      Location = null;
      NumberOfRooms = null;
      Area = null;
      NumberOfKeys = null;
      Fridge = false;
      Dishwasher = false;
      Stove = false;
      Oven = false;
      WashingMachine = false;
      Tumbler = false;
    }
  }
}
