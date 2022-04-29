using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace ImmoGlobal.ViewModels
{
  internal class UpsertRentalContractViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new rental contract
    /// </summary>
    internal UpsertRentalContractViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      PersonaCollection = new(DbController.GetAllPersonasDB());
      PropertyCollection = new(DbController.GetAllPropertiesDB());

      RentStartDate = DateTime.Now;
      RentEndDate = RentStartDate.AddDays(30);

      //set the title of the form
      FormTitel = Application.Current.FindResource("createNewRentalContract") as string ?? "create new rental contract";
    }

    /// <summary>
    /// c'tor to edit an existing rental contract
    /// </summary>
    /// <param name="selectedRentalContract"></param>
    internal UpsertRentalContractViewModel(RentalContract selectedRentalContract)
    {
      SelectedRentalContract = selectedRentalContract;
      RentalContractId = selectedRentalContract.RentalContractId;

      BtnSave = new RelayCommand<object>(SaveClicked);
      PersonaCollection = new(DbController.GetAllPersonasDB());
      PropertyCollection = new(DbController.GetAllPropertiesDB());

      SelectedPersona = selectedRentalContract.GetRenter();
      SelectedPropertyObject = selectedRentalContract.GetPropertyObjectToRentalContract();
      SelectedProperty = SelectedPropertyObject.GetPropertyToPropertyObject();
      RentStartDate = selectedRentalContract.RentStartDate;
      RentEndDate = selectedRentalContract.RentEndDate;
      Rent = selectedRentalContract.Rent.ToString();
      Deposit = selectedRentalContract.Deposit;
      ContractState = selectedRentalContract.ContractState;

      //set the title of the form
      FormTitel = (Application.Current.FindResource("rentalContract") as string ?? "rental contract") + " " +
                     (Application.Current.FindResource("edit") as string ?? "edit");
    }


    private Persona? _selectedPersona;
    private ObservableCollection<Persona> _personaCollection;
    private Property? _selectedProperty;
    private ObservableCollection<Property> _propertyCollection;
    private PropertyObject? _selectedPropertyObject;
    private ObservableCollection<PropertyObject>? _propertyObjectCollection;
    private DateTime _rentStartDate;
    private DateTime? _rentEndDate;
    private string? _rent;
    private bool _deposit;
    private EContractState _contractState;


    public string FormTitel { get; set; }

    private int? RentalContractId { get; set; }
    public RentalContract? SelectedRentalContract { get; set; }

    public Persona? SelectedPersona
    {
      get => _selectedPersona;
      set
      {
        _selectedPersona = value;
        OnPropertyChanged();
      }
    }
    public ObservableCollection<Persona> PersonaCollection
    {
      get => _personaCollection;
      set
      {
        _personaCollection = value;
        OnPropertyChanged();
      }
    }

    public PropertyObject? SelectedPropertyObject
    {
      get => _selectedPropertyObject;
      set
      {
        _selectedPropertyObject = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Property> PropertyCollection
    {
      get => _propertyCollection;
      set
      {
        _propertyCollection = value;
        OnPropertyChanged();
      }
    }

    public Property? SelectedProperty
    {
      get => _selectedProperty;
      set
      {
        _selectedProperty = value;
        if (SelectedRentalContract == null)
        {
          PropertyObjectCollection = new(DbController.GetPropertyObjectsToPropertyDB(_selectedProperty).Where(x => !x.GetRentalContractToObject().Any(y => y.ContractState == EContractState.Active)));
        }
        else
        {
          PropertyObjectCollection = new(DbController.GetPropertyObjectsToPropertyDB(_selectedProperty));
        }
        OnPropertyChanged();
      }
    }

    public ObservableCollection<PropertyObject>? PropertyObjectCollection
    {
      get => _propertyObjectCollection;
      set
      {
        _propertyObjectCollection = value;
        OnPropertyChanged();
      }
    }

    public DateTime RentStartDate
    {
      get => _rentStartDate;
      set
      {
        _rentStartDate = value;
        OnPropertyChanged();
      }
    }

    public DateTime? RentEndDate
    {
      get => _rentEndDate;
      set
      {
        _rentEndDate = value;
        OnPropertyChanged();
      }
    }

    public string? Rent
    {
      get => _rent;
      set
      {
        _rent = value;
        OnPropertyChanged();
      }
    }

    public bool Deposit
    {
      get => _deposit;
      set
      {
        _deposit = value;
        OnPropertyChanged();
      }
    }

    public ICommand BtnSave
    {
      get;
      private set;
    }

    public EContractState ContractState
    {
      get => _contractState;
      set
      {
        _contractState = value;
        OnPropertyChanged();
      }
    }

    public Dictionary<EContractState, string> EContractStateWithCaptions { get; } = new Dictionary<EContractState, string>()
    {
      {EContractState.NotActive, Application.Current.FindResource("notActiveContract") as string ?? "not active contract" },
      {EContractState.Singend, Application.Current.FindResource("singnedContract") as string ?? "singned contract"},
      {EContractState.Active, Application.Current.FindResource("activeContract") as string ?? "active contract"},
      {EContractState.Canceled, Application.Current.FindResource("canceledContract") as string ?? "canceled contract"},
    };

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(Rent, out double rent))
      {
        ShowMessageBox(Application.Current.FindResource("errorRent") as string ?? "Please enter a valid rent", MessageType.Error, MessageButtons.Ok);
        return;
      }
      //Create RentalContract
      if (RentalContractId == null && CreateRentalContract(rent))
      {
        ShowNotification("Success", Application.Current.FindResource("successAddRentalContract") as string ?? "Rental Contract added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Property
      else if (RentalContractId != null && UpdateRentalContract(rent, (int)RentalContractId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateRentalContract") as string ?? "Rental Contract updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddRentalContract") as string ?? "Error adding Rental Contract", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (SelectedPersona != null &&
        SelectedProperty != null &&
        SelectedPropertyObject != null &&
        !string.IsNullOrEmpty(Rent))
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
    private bool CreateRentalContract(double rent)
    {
      if (DbController.UpsertRentalContractToDB(new RentalContract()
      {
        Renter = SelectedPersona,
        PropertyObject = SelectedPropertyObject,
        RentStartDate = RentStartDate,
        RentEndDate = RentEndDate,
        Rent = rent,
        Deposit = Deposit,
        ContractState = ContractState
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
    private bool UpdateRentalContract(double rent, int propertyId)
    {
      if (DbController.UpsertRentalContractToDB(new RentalContract()
      {
        RentalContractId = propertyId,
        Renter = SelectedPersona,
        PropertyObject = SelectedPropertyObject,
        RentStartDate = RentStartDate,
        RentEndDate = RentEndDate,
        Rent = rent,
        Deposit = Deposit,
        ContractState = ContractState

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
      SelectedPersona = null;
      SelectedProperty = null;
      SelectedPropertyObject = null;
      RentStartDate = DateTime.Now;
      RentEndDate = RentStartDate.AddDays(30);
      Rent = string.Empty;
      Deposit = false;
      ContractState = EContractState.NotActive;
    }
  }
}
