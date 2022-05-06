using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertRenterViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new renter
    /// </summary>
    internal UpsertRenterViewModel()
    {
      //set the title of the form
      FormTitel = Application.Current.FindResource("addNewRenter") as string ?? "create new renter";
    }

    /// <summary>
    /// c'tor to edit an existing renter
    /// </summary>
    /// <param name="selectedRenter"></param>
    internal UpsertRenterViewModel(Persona selectedRenter)
    {
      SelectedRenter = selectedRenter;

      Id = selectedRenter.PersonaId;
      LastName = selectedRenter.LastName;
      FirstName = selectedRenter.FirstName;
      Phone = selectedRenter.PhoneString;
      Email = selectedRenter.Email;
      DateOfBirth = selectedRenter.DateOfBirth;
      Address = selectedRenter.Address;
      Zip = selectedRenter.Zip.ToString();
      City = selectedRenter.City;
      CivilState = selectedRenter.CivilState ?? ECivilState.Single;
      AddressBefore = selectedRenter.AddressBefore;
      AccountNumber = selectedRenter.AccountNumber;
      MobilePhone = selectedRenter.MobileString;
      OfficePhone = selectedRenter.OfficePhoneString;

      //set the title of the form
      FormTitel = (Application.Current.FindResource("renter") as string ?? "renter") + " " +
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
    private ECivilState _civilState;
    private string? _addressBefore;
    private string? _accountNumber;
    private string? _mobilePhone;
    private string? _officePhone;

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
      get => _civilState;
      set
      {
        _civilState = value;
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

    internal override void SaveClicked(object obj)
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
      if (Id == null && CreatePersona(phone, mobilePhone, officePhone, zipCode))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddRenter") as string ?? "Renter added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update Perrsona
      else if (Id != null && UpdatePersona(phone, mobilePhone, officePhone, zipCode, (int)Id))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateRenter") as string ?? "Renter updated successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddRenter") as string ?? "Error adding renter", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (!string.IsNullOrEmpty(FirstName) &&
          !string.IsNullOrEmpty(LastName) &&
          !string.IsNullOrEmpty(Email) &&
          !string.IsNullOrEmpty(Address) &&
          !string.IsNullOrEmpty(Zip) &&
          !string.IsNullOrEmpty(City) &&
          !string.IsNullOrEmpty(AddressBefore) &&
          !string.IsNullOrEmpty(AccountNumber) &&
          DateOfBirth != null)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Create new persona
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="officePhone"></param>
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
    /// update persona
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="officePhone"></param>
    /// <param name="zipCode"></param>
    /// <param name="personaId"></param>
    /// <returns></returns>
    private bool UpdatePersona(long phone, long mobilePhone, long officePhone, int zipCode, int personaId)
    {
      if (DateOfBirth != null)
      {
        SelectedRenter.LastName = LastName;
        SelectedRenter.FirstName = FirstName;
        SelectedRenter.Phone = phone;
        SelectedRenter.Email = Email;
        SelectedRenter.DateOfBirth = (DateTime)DateOfBirth;
        SelectedRenter.Address = Address;
        SelectedRenter.Zip = zipCode;
        SelectedRenter.City = City;
        SelectedRenter.CivilState = CivilState;
        SelectedRenter.AddressBefore = AddressBefore;
        SelectedRenter.AccountNumber = AccountNumber;
        SelectedRenter.Mobile = mobilePhone;
        SelectedRenter.OfficePhone = officePhone;
        SelectedRenter.PersonaId = personaId;
        if (DbController.UpsertPersonaToDB(SelectedRenter))
        {
          return true;
        }
      }
      return false;
    }
  }
}
