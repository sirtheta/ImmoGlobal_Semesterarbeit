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
  internal class UpsertCreditorViewModel : BaseViewModel

  {
    public UpsertCreditorViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      _formTitel = Application.Current.FindResource("addNewCreditor") as string ?? "create new creditor";
    }

    public UpsertCreditorViewModel(Persona selectedCreditor)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      SelectedCreditor = selectedCreditor;

      PersonaId = selectedCreditor.PersonaId;
      _lastName = selectedCreditor.LastName;
      _firstName = selectedCreditor.FirstName;
      _phone = selectedCreditor.PhoneString;
      _mobilePhone = selectedCreditor.MobileString;
      _officePhone = selectedCreditor.OfficePhoneString;
      _email = selectedCreditor.Email;
      _address = selectedCreditor.Address;
      _zip = selectedCreditor.Zip.ToString();
      _city = selectedCreditor.City;
      _creditorIsActive = selectedCreditor.CreditorIsActive;
      _vatNumber = selectedCreditor.VatNumber;
      _creditorCompanyName = selectedCreditor.CreditorCompanyName;
      _creditorContactPerson = selectedCreditor.CreditorContactPerson;

      _formTitel = (Application.Current.FindResource("creditor") as string ?? "creditor") + " " +
             (Application.Current.FindResource("edit") as string ?? "edit");
    }

    private string? _firstName;
    private string? _lastName;
    private string? _phone;
    private string? _mobilePhone;
    private string? _officePhone;
    private string? _email;
    private string? _address;
    private string? _zip;
    private string? _city;
    private bool _creditorIsActive;
    private string? _vatNumber;
    private string? _creditorCompanyName;
    private string? _creditorContactPerson;

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

    public bool CreditorIsActive
    {
      get => _creditorIsActive;
      set
      {
        _creditorIsActive = value;
        OnPropertyChanged();
      }
    }

    public string? VatNumber
    {
      get => _vatNumber;
      set
      {
        _vatNumber = value;
        OnPropertyChanged();
      }
    }

    public string? CreditorCompanyName
    {
      get => _creditorCompanyName;
      set
      {
        _creditorCompanyName = value;
        OnPropertyChanged();
      }
    }

    public string? CreditorContactPerson
    {
      get => _creditorContactPerson;
      set
      {
        _creditorContactPerson = value;
        OnPropertyChanged();
      }
    }

    public Persona? SelectedCreditor { get; set; }

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
        ShowNotification("Success", Application.Current.FindResource("successAddCreditor") as string ?? "Creditor added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Perrsona
      else if (PersonaId != null && UpdatePersona(phone, mobilePhone, officePhone, zipCode, (int)PersonaId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateCreditor") as string ?? "Creditor updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddCreditor") as string ?? "Error adding creditor", MessageType.Error, MessageButtons.Ok);
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
          string.IsNullOrEmpty(VatNumber) ||
          string.IsNullOrEmpty(CreditorCompanyName) ||
          string.IsNullOrEmpty(CreditorContactPerson))
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
    private bool CreatePersona(long phone, long mobilePhone, long officePhone, int zipCode)
    {
      if (DbController.UpsertPersonaToDB(new Persona(CreditorIsActive, CreditorCompanyName, Address, zipCode, City, phone, CreditorContactPerson, LastName, FirstName, Email, VatNumber, mobilePhone, officePhone)))
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
      if (DbController.UpsertPersonaToDB(new Persona(CreditorIsActive, CreditorCompanyName, Address, zipCode, City, phone, CreditorContactPerson, LastName, FirstName, Email, VatNumber, mobilePhone, officePhone, personaId)))
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
      VatNumber = null;
      CreditorCompanyName = null;
      CreditorContactPerson = null;
      CreditorIsActive = false;
    }
  }
}
