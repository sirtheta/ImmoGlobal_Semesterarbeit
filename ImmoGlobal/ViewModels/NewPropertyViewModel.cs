using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class NewPropertyViewModel : BaseViewModel
  {
    public NewPropertyViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      _personas = new(DbController.GetAllPersonasDB());
      
    }

    private string? _description;
    private string? _address;
    private string? _zipCode;
    private string? _city;
    private string? _propertyInsurance;
    private string? _personInsurance;
    private string? _liabilityInsurance;
    private Persona? _housekeeper;
    private ObservableCollection<Persona> _personas;

    public string? Description
    {
      get => _description;
      set
      {
        _description = value;
        OnPropertyChanged();
      }
    }
    public string? Address
    {
      get => _address;
      set
      {
        _address = value;
        OnPropertyChanged();
      }
    }
    public string? ZipCode
    {
      get => _zipCode;
      set
      {
        _zipCode = value;
        OnPropertyChanged();
      }
    }
    public string? City
    {
      get => _city;
      set
      {
        _city = value;
        OnPropertyChanged();
      }
    }
    public string? PropertyInsurance
    {
      get => _propertyInsurance;
      set
      {
        _propertyInsurance = value;
        OnPropertyChanged();
      }
    }
    public string? PersonInsurance
    {
      get => _personInsurance;
      set
      {
        _personInsurance = value;
        OnPropertyChanged();
      }
    }
    public string? LiabilityInsurance
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
    private void SaveClicked(object obj)
    {
      if (string.IsNullOrEmpty(Description) ||
        string.IsNullOrEmpty(Address) || 
        string.IsNullOrEmpty(City) || 
        string.IsNullOrEmpty(PropertyInsurance) || 
        string.IsNullOrEmpty(PersonInsurance) ||
        string.IsNullOrEmpty(LiabilityInsurance) || 
        Housekeeper == null)
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (int.TryParse(ZipCode, out int zipCode))
      {
        if (zipCode < 1000 || zipCode > 9999)
        {
          ShowMessageBox(Application.Current.FindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
          return;
        }
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
        return;
      }
      
      if (DbController.AddNewPropertyDB(new Property()
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
        ShowNotification("Success", Application.Current.FindResource("successAddProperty") as string ?? "Property added successfully", NotificationType.Success);
        ClearValues();
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddProperty") as string ?? "Error adding property", MessageType.Error, MessageButtons.Ok);
      }
    }

    /// <summary>
    /// Sets all properties to null
    /// </summary>
    private void ClearValues()
    {
      Description = null;
      Address = null;
      ZipCode = null;
      City = null;
      PropertyInsurance = null;
      PersonInsurance = null;
      LiabilityInsurance = null;
      Housekeeper = null;
    }
  }
}
