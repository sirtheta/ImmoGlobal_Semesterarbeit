using ImmoGlobal.Database;
using ImmoGlobal.Helpers;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Windows;


namespace ImmoGlobal.ViewModels
{
  internal class UpsertHousekeeperViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new housekeeper
    /// </summary>
    internal UpsertHousekeeperViewModel()
    {

      //set the title of the form
      FormTitel = Application.Current.FindResource("addNewHousekeeper") as string ?? "create new housekeeper";
    }

    /// <summary>
    /// c'tor to edit an existing housekeeper
    /// </summary>
    /// <param name="selectedHousekeeper"></param>
    internal UpsertHousekeeperViewModel(Persona selectedHousekeeper)
    {
      SelectedHousekeeper = selectedHousekeeper;

      Id = selectedHousekeeper.PersonaId;
      FirstName = selectedHousekeeper.FirstName;
      LastName = selectedHousekeeper.LastName;
      Phone = selectedHousekeeper.PhoneString;
      MobilePhone = selectedHousekeeper.MobileString;
      Email = selectedHousekeeper.Email;
      Address = selectedHousekeeper.Address;
      Zip = selectedHousekeeper.Zip.ToString();
      City = selectedHousekeeper.City;
      AccountNumber = selectedHousekeeper.AccountNumber;

      //set the title of the form
      FormTitel = (Application.Current.FindResource("housekeeper") as string ?? "housekeeper") + " " +
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
      if (Id == null && CreatePersona(phone, mobilePhone, zipCode))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddhousekeeperr") as string ?? "Housekeeper added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Perrsona
      else if (Id != null && UpdatePersona(phone, mobilePhone, zipCode, (int)Id))
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
      if (!string.IsNullOrEmpty(FirstName) &&
          !string.IsNullOrEmpty(LastName) &&
          !string.IsNullOrEmpty(Email) &&
          !string.IsNullOrEmpty(Address) &&
          !string.IsNullOrEmpty(Zip) &&
          !string.IsNullOrEmpty(City) &&
          !string.IsNullOrEmpty(AccountNumber))
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
      FirstName = string.Empty;
      LastName = string.Empty;
      Phone = string.Empty;
      MobilePhone = string.Empty;
      Email = string.Empty;
      Address = string.Empty;
      Zip = string.Empty;
      City = string.Empty;
      AccountNumber = string.Empty;
    }

  }
}
