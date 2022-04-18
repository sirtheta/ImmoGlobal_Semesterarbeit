using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertHousekeeperViewModel : BaseViewModel

  {
    public UpsertHousekeeperViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      _formTitel = Application.Current.FindResource("addNewHousekeeper") as string ?? "create new housekeeper";
    }

    public UpsertHousekeeperViewModel(Persona selectedHousekeeper)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      SelectedHousekeeper = selectedHousekeeper;

      PersonaId = selectedHousekeeper.PersonaId;
      _firstName = selectedHousekeeper.FirstName;
      _lastName = selectedHousekeeper.LastName;
      _phone = selectedHousekeeper.PhoneString;
      _mobilePhone = selectedHousekeeper.MobileString;
      _email = selectedHousekeeper.Email;
      _address = selectedHousekeeper.Address;
      _zip = selectedHousekeeper.Zip.ToString();
      _city = selectedHousekeeper.City;
      _accountNumber = selectedHousekeeper.AccountNumber;


      _formTitel = (Application.Current.FindResource("housekeeper") as string ?? "housekeeper") + " " +
             (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string? _firstName;
    private string? _lastName;
    private string? _phone;
    private string? _mobilePhone;
    private string? _email;
    private string? _address;
    private string? _zip;
    private string? _city;
    private string? _accountNumber;

    private int? PersonaId { get; set; }

    // sets the titel of the form
    private string _formTitel;

    public string FormTitel
    {
      get => _formTitel;
      set => _formTitel = value;
    }

    public string? LastName
    {
      get => _lastName;
      set
      {
        _lastName = value;
        OnPropertyChanged();
      }
    }

    public string? FirstName
    {
      get => _firstName;
      set
      {
        _firstName = value;
        OnPropertyChanged();
      }
    }

    public string? Phone
    {
      get => _phone;
      set
      {
        _phone = value;
        OnPropertyChanged();
      }
    }

    public string? MobilePhone
    {
      get => _mobilePhone;
      set
      {
        _mobilePhone = value;
        OnPropertyChanged();
      }
    }

    public string? AccountNumber
    {
      get => _accountNumber;
      set
      {
        _accountNumber = value;
        OnPropertyChanged();
      }
    }

    public string? Email
    {
      get => _email;
      set
      {
        _email = value;
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

    public string? Zip
    {
      get => _zip;
      set
      {
        _zip = value;
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

    public Persona? SelectedHousekeeper { get; set; }

    public ICommand BtnSave
    {
      get;
      private set;
    }

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(Zip, out int zipCode) && zipCode < 1000 && zipCode > 9999)
      {
        ShowMessageBox(Application.Current.FindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
        return;
      }

      bool phoneParse = true;
      bool mobilePhoneParse = true;
      //check if at least one phone number can be parsed
      if (!long.TryParse(Phone, out long phone))
      {
        phoneParse = false;
      }
      if (!long.TryParse(MobilePhone, out long mobilePhone))
      {
        mobilePhoneParse = false;
      }

      if (!phoneParse && !mobilePhoneParse)
      {
        ShowMessageBox(Application.Current.FindResource("errorPhone") as string ?? "Please enter a valid phone number", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!Validator.IsValidEmail(Email))
      {
        ShowMessageBox(Application.Current.FindResource("errorMail") as string ?? "Please enter a valid Email Address", MessageType.Error, MessageButtons.Ok);
        return;
      }


      //Create Persona
      if (PersonaId == null && CreatePersona(phone, mobilePhone, zipCode))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddhousekeeperr") as string ?? "Housekeeper added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Perrsona
      else if (PersonaId != null && UpdatePersona(phone, mobilePhone, zipCode, (int)PersonaId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateHousekeeper") as string ?? "Housekeeper updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddHousekeeper") as string ?? "Error adding housekeeper", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (string.IsNullOrEmpty(FirstName) ||
          string.IsNullOrEmpty(LastName) &&
          (string.IsNullOrEmpty(Phone) ||
          string.IsNullOrEmpty(MobilePhone)) ||
          string.IsNullOrEmpty(Email) ||
          string.IsNullOrEmpty(Address) ||
          string.IsNullOrEmpty(Zip) ||
          string.IsNullOrEmpty(City) ||
          string.IsNullOrEmpty(AccountNumber))
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Create new persona
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="officePhone"></param>
    /// <param name="zipCode"></param>
    /// <returns></returns>
    private bool CreatePersona(long phone, long mobilePhone, int zipCode)
    {
      if (DbController.UpsertPersonaToDB(new Persona(LastName, FirstName, phone, Email, Address, zipCode, City, AccountNumber, mobilePhone)))
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// update persona
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="officePhone"></param>
    /// <param name="zipCode"></param>
    /// <param name="personaId"></param>
    /// <returns></returns>
    private bool UpdatePersona(long phone, long mobilePhone, int zipCode, int personaId)
    {
      if (DbController.UpsertPersonaToDB(new Persona(LastName, FirstName, phone, Email, Address, zipCode, City, AccountNumber, mobilePhone, personaId)))
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
      FirstName = null;
      LastName = null;
      Phone = null;
      MobilePhone = null;
      Email = null;
      Address = null;
      Zip = null;
      City = null;
      AccountNumber = null;
    }
  }
}
