using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertRenterViewModel : BaseViewModel
  {
    public UpsertRenterViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      _formTitel = Application.Current.FindResource("addNewRenter") as string ?? "create new renter";
    }

    public UpsertRenterViewModel(Persona selectedRenter)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      SelectedRenter = selectedRenter;

      PersonaId = selectedRenter.PersonaId;
      _lastName = selectedRenter.LastName;
      _firstName = selectedRenter.FirstName;
      _phone = selectedRenter.PhoneString;
      _email = selectedRenter.Email;
      _dateOfBirth = selectedRenter.DateOfBirth;
      _address = selectedRenter.Address;
      _zip = selectedRenter.Zip.ToString();
      _city = selectedRenter.City;
      _eCivilState = selectedRenter.CivilState;
      _addressBefore = selectedRenter.AddressBefore;
      _accountNumber = selectedRenter.AccountNumber;
      _mobilePhone = selectedRenter.MobileString;
      _officePhone = selectedRenter.OfficePhoneString;

      _formTitel = (Application.Current.FindResource("renter") as string ?? "property") + " " +
             (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string? _lastName;
    private string? _firstName;
    private string? _phone;
    private string? _email;
    private DateTime? _dateOfBirth;
    private string? _address;
    private string? _zip;
    private string? _city;
    private ECivilState _eCivilState;
    private string? _addressBefore;
    private string? _accountNumber;
    private string? _mobilePhone;
    private string? _officePhone;

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

    public string? OfficePhone
    {
      get => _officePhone;
      set
      {
        _officePhone = value;
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

    public DateTime? DateOfBirth
    {
      get => _dateOfBirth;
      set
      {
        _dateOfBirth = value;
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

    public ECivilState CivilState
    {
      get => _eCivilState;
      set
      {
        _eCivilState = value;
        OnPropertyChanged();
      }
    }

    public Dictionary<ECivilState, string> ECivilStateTypeWithCaptions { get; } =
    new Dictionary<ECivilState, string>()
    {
              {ECivilState.Single, Application.Current.FindResource("single") as string ?? "single" },
              {ECivilState.Married, Application.Current.FindResource("married") as string ?? "married"},
              {ECivilState.Divorced, Application.Current.FindResource("divorced") as string ?? "divorced"},
              {ECivilState.Widowed, Application.Current.FindResource("widowed") as string ?? "widowed"},
    };

    public string? AddressBefore
    {
      get => _addressBefore;
      set
      {
        _addressBefore = value;
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


    public Persona? SelectedRenter { get; set; }

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
      bool officePhoneParse = true;
      //check if at least one phone number can be parsed
      if (!long.TryParse(Phone, out long phone))
      {
        phoneParse = false;
      }
      if (!long.TryParse(MobilePhone, out long mobilePhone))
      {
        mobilePhoneParse = false;
      }
      if (!long.TryParse(OfficePhone, out long officePhone))
      {
        officePhoneParse = false;
      }
      
      if (!phoneParse && !mobilePhoneParse && !officePhoneParse)
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
      if (PersonaId == null && CreatePersona(phone, mobilePhone, officePhone, zipCode))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddRenter") as string ?? "Renter added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Perrsona
      else if (PersonaId != null && UpdatePersona(phone, mobilePhone, officePhone, zipCode, (int)PersonaId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateRenter") as string ?? "Renter updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddRenter") as string ?? "Error adding renter", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (string.IsNullOrEmpty(FirstName) ||
          string.IsNullOrEmpty(LastName) &&
          (string.IsNullOrEmpty(Phone) ||
          string.IsNullOrEmpty(MobilePhone) ||
          string.IsNullOrEmpty(OfficePhone)) ||
          string.IsNullOrEmpty(Email) ||
          string.IsNullOrEmpty(Address) ||
          string.IsNullOrEmpty(Zip) ||
          string.IsNullOrEmpty(City) ||
          string.IsNullOrEmpty(AddressBefore) ||
          string.IsNullOrEmpty(AccountNumber) ||
          DateOfBirth == null)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Create new Property
    /// </summary>
    /// <param name="zipCode"></param>
    /// <returns></returns>
    private bool CreatePersona(long phone, long mobilePhone, long officePhone, int zipCode)
    {
      if (DateOfBirth != null &&
        DbController.UpsertPersonaToDB(new Persona(LastName, FirstName, phone, Email, (DateTime)DateOfBirth, Address, zipCode, City, CivilState, AddressBefore, AccountNumber, mobilePhone, officePhone)))
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
    private bool UpdatePersona(long phone, long mobilePhone, long officePhone, int zipCode, int personaId)
    {
      if (DateOfBirth != null &&
        DbController.UpsertPersonaToDB(new Persona(LastName, FirstName, phone, Email, (DateTime)DateOfBirth, Address, zipCode, City, CivilState, AddressBefore, AccountNumber, mobilePhone, officePhone, personaId)))
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
      OfficePhone = null;
      Email = null;
      Address = null;
      Zip = null;
      City = null;
      AddressBefore = null;
      AccountNumber = null;
      DateOfBirth = null;
      CivilState = ECivilState.Single;
    }
  }
}
