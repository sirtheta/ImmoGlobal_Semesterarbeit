using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertCreditorViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new creditor
    /// </summary>
    internal UpsertCreditorViewModel()
    {
      //set the title of the form
      FormTitel = Application.Current.TryFindResource("addNewCreditor") as string ?? "create new creditor";
    }

    /// <summary>
    /// c'tor to edit an existing creditor
    /// </summary>
    /// <param name="selectedCreditor"></param>
    internal UpsertCreditorViewModel(Persona selectedCreditor)
    {
      SelectedCreditor = selectedCreditor;

      Id = selectedCreditor.PersonaId;
      LastName = selectedCreditor.LastName;
      FirstName = selectedCreditor.FirstName;
      Phone = selectedCreditor.PhoneString;
      MobilePhone = selectedCreditor.MobileString;
      OfficePhone = selectedCreditor.OfficePhoneString;
      Email = selectedCreditor.Email;
      Address = selectedCreditor.Address;
      Zip = selectedCreditor.Zip.ToString();
      City = selectedCreditor.City;
      CreditorIsActive = selectedCreditor.CreditorIsActive ?? false;
      VatNumber = selectedCreditor.VatNumber;
      CreditorCompanyName = selectedCreditor.CreditorCompanyName;
      CreditorContactPerson = selectedCreditor.CreditorContactPerson;

      //set the title of the form
      FormTitel = (Application.Current.TryFindResource("creditor") as string ?? "creditor") + " " +
             (Application.Current.TryFindResource("edit") as string ?? "edit");
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

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!int.TryParse(Zip, out int zipCode) && zipCode < 1000 && zipCode > 9999)
      {
        ShowMessageBox(Application.Current.TryFindResource("errorZipCode") as string ?? "Please enter a valid zip code", MessageType.Error, MessageButtons.Ok);
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
        ShowMessageBox(Application.Current.TryFindResource("errorPhone") as string ?? "Please enter a valid phone number", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!Validator.IsValidEmail(Email))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorMail") as string ?? "Please enter a valid Email Address", MessageType.Error, MessageButtons.Ok);
        return;
      }


      //Create Persona
      if (Id == null && CreatePersona(phone, mobilePhone, officePhone, zipCode))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddCreditor") as string ?? "Creditor added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update Perrsona
      else if (Id != null && UpdatePersona(phone, mobilePhone, officePhone, zipCode, (int)Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdateCreditor") as string ?? "Creditor updated successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorAddCreditor") as string ?? "Error adding creditor", MessageType.Error, MessageButtons.Ok);
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
          !string.IsNullOrEmpty(VatNumber) &&
          !string.IsNullOrEmpty(CreditorCompanyName) &&
          !string.IsNullOrEmpty(CreditorContactPerson))
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
      SelectedCreditor.PersonaId = personaId;
      SelectedCreditor.FirstName = FirstName;
      SelectedCreditor.LastName = LastName;
      SelectedCreditor.Email = Email;
      SelectedCreditor.CreditorCompanyName = CreditorCompanyName;
      SelectedCreditor.Address = Address;
      SelectedCreditor.Zip = zipCode;
      SelectedCreditor.City = City;
      SelectedCreditor.Phone = phone;
      SelectedCreditor.OfficePhone = officePhone;
      SelectedCreditor.Mobile = mobilePhone;
      SelectedCreditor.CreditorContactPerson = CreditorContactPerson;
      SelectedCreditor.VatNumber = VatNumber;
      SelectedCreditor.CreditorIsActive = CreditorIsActive;

      if (DbController.UpsertPersonaToDB(SelectedCreditor))
      {
        return true;
      }
      return false;
    }
  }
}
