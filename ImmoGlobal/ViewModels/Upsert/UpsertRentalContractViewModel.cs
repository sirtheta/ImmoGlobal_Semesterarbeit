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


namespace ImmoGlobal.ViewModels
{
  internal class UpsertRentalContractViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new rental contract
    /// </summary>
    internal UpsertRentalContractViewModel()
    {
      PersonaCollection = new(DbController.GetAllPersonasDB());
      PropertyCollection = new(DbController.GetAllPropertiesDB());

      RentStartDate = DateTime.Now;
      RentEndDate = RentStartDate.AddDays(30);

      //set the title of the form
      FormTitel = Application.Current.TryFindResource("createNewRentalContract") as string ?? "create new rental contract";
    }

    /// <summary>
    /// c'tor to edit an existing rental contract
    /// </summary>
    /// <param name="selectedRentalContract"></param>
    internal UpsertRentalContractViewModel(RentalContract selectedRentalContract)
    {
      SelectedRentalContract = selectedRentalContract;
      Id = selectedRentalContract.RentalContractId;

      PersonaCollection = new(DbController.GetAllPersonasDB());
      PropertyCollection = new(DbController.GetAllPropertiesDB());

      SelectedPersona = selectedRentalContract.GetRenter();
      //Load selected property object after the selected property is loaded, otherewise it is set to null!
      SelectedProperty = selectedRentalContract.GetPropertyObjectToRentalContract().GetPropertyToPropertyObject();
      SelectedPropertyObject = selectedRentalContract.GetPropertyObjectToRentalContract();
      RentStartDate = selectedRentalContract.RentStartDate;
      RentEndDate = selectedRentalContract.RentEndDate;
      Rent = selectedRentalContract.Rent.ToString();
      Deposit = selectedRentalContract.Deposit;
      ContractState = selectedRentalContract.ContractState;

      //set the title of the form
      FormTitel = (Application.Current.TryFindResource("rentalContract") as string ?? "rental contract") + " " +
                     (Application.Current.TryFindResource("edit") as string ?? "edit");
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
        if (_selectedProperty != value)
        {
          _selectedProperty = value;
          SelectedPropertyObject = null;
        }
        PropertyObjectCollection = new(DbController.GetPropertyObjectsToPropertyDB(_selectedProperty));
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
      {EContractState.NotActive, Application.Current.TryFindResource("notActiveContract") as string ?? "not active contract" },
      {EContractState.Singend, Application.Current.TryFindResource("singnedContract") as string ?? "singned contract"},
      {EContractState.Active, Application.Current.TryFindResource("activeContract") as string ?? "active contract"},
      {EContractState.Canceled, Application.Current.TryFindResource("canceledContract") as string ?? "canceled contract"},
    };

    internal override void DeleteClicked(object obj)
    {
      if (DbController.DeleteRentalContractDB(Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successDeleteRentalContract") as string ?? "Rental Contract deleted successfully", NotificationType.Success);
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyOverviewViewModel();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorDeleteRentalContract") as string ?? "Error on delete Rental Contract", MessageType.Error, MessageButtons.Ok);
      }
    }

    internal override void SaveClicked(object obj)
    {
      if (Id == null && ContractState == EContractState.Active && DbController.GetPropertyObjectsToPropertyDB(_selectedProperty).Where(x => !x.GetRentalContractToPropertyObject().Any(y => y.ContractState == EContractState.Active)).Any())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorActivContract") as string ?? "Cannot add second activ contract", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      if (!double.TryParse(Rent, out double rent))
      {
        ShowMessageBox(Application.Current.TryFindResource("errorRent") as string ?? "Please enter a valid rent", MessageType.Error, MessageButtons.Ok);
        return;
      }
      //Create RentalContract
      if (Id == null && CreateRentalContract(rent))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddRentalContract") as string ?? "Rental Contract added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update Property
      else if (Id != null && UpdateRentalContract(rent, (int)Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdateRentalContract") as string ?? "Rental Contract updated successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorAddRentalContract") as string ?? "Error adding Rental Contract", MessageType.Error, MessageButtons.Ok);
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
      SelectedRentalContract.RentalContractId = propertyId;
      SelectedRentalContract.Renter = SelectedPersona;
      SelectedRentalContract.PropertyObject = SelectedPropertyObject;
      SelectedRentalContract.RentStartDate = RentStartDate;
      SelectedRentalContract.RentEndDate = RentEndDate;
      SelectedRentalContract.Rent = rent;
      SelectedRentalContract.Deposit = Deposit;
      SelectedRentalContract.ContractState = ContractState;
      if (DbController.UpsertRentalContractToDB(SelectedRentalContract))
      {
        return true;
      }
      return false;
    }
  }
}
