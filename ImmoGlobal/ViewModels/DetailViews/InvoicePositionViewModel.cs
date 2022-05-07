using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class InvoicePositionViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new invoice position
    /// </summary>
    internal InvoicePositionViewModel()
    {
      PropertyCollection = new(DbController.GetAllPropertiesDB());
      AccountCollection = new(DbController.GetAllAccountsDB());
    }

    /// <summary>
    /// c'tor for editing an existing invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    internal InvoicePositionViewModel(InvoicePosition invoicePosition)
    {
      InvoicePosition = invoicePosition;

      PropertyCollection = new(DbController.GetAllPropertiesDB());
      AccountCollection = new(DbController.GetAllAccountsDB());

      SelectedInvoicePositionId = invoicePosition.InvoicePositionId;
      SelectedProperty = invoicePosition.GetPropertyToInvoicePosition();
      SelectedPropertyObject = invoicePosition.GetPropertyObjectToInvoicePosition();
      Value = invoicePosition.Value;
      SelectedAccount = invoicePosition.GetAccountToInvoicePosition();
      AdditionalCostsCategory = invoicePosition.AdditionalCostsCategory;

    }

    private Property? _selectedProperty;
    private ObservableCollection<Property> _propertyCollection;
    private PropertyObject? _selectedPropertyObject;
    private ObservableCollection<PropertyObject>? _propertyObjectCollection;
    private Account? _selectedAccount;
    private ObservableCollection<Account>? _accountCollection;
    private int _invoicePositionNumber;
    private double _value;
    private EAdditionalCosts? _additionalCosts;

    internal InvoicePosition InvoicePosition { get; set; }
    public InvoicePositionViewModel InvoicePositionViewModelContent { get => this; }
    public int InvoicePositionNumber
    {
      get => _invoicePositionNumber;
      set
      {
        _invoicePositionNumber = value;
        OnPropertyChanged();
      }
    }

    public int? SelectedInvoicePositionId { get; set; }
    public double Value
    {
      get => _value;
      set
      {
        _value = value;
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

    public Account? SelectedAccount
    {
      get => _selectedAccount;
      set
      {
        _selectedAccount = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Account>? AccountCollection
    {
      get => _accountCollection;
      set
      {
        _accountCollection = value;
        OnPropertyChanged();
      }
    }

    public EAdditionalCosts? AdditionalCostsCategory
    {
      get
      {
        if (_additionalCosts != EAdditionalCosts.None)
        {
          return _additionalCosts;
        }
        else
        {
          return null;
        }

      }
      set
      {
        _additionalCosts = value;
        OnPropertyChanged();
      }
    }

    public Dictionary<EAdditionalCosts, string> EAdditionalCostsWithCaptions { get; } = new Dictionary<EAdditionalCosts, string>()
    {
      {EAdditionalCosts.None, Application.Current.TryFindResource("none") as string ?? "none" },
      {EAdditionalCosts.Water, Application.Current.TryFindResource("water") as string ?? "Water" },
      {EAdditionalCosts.Housekeeper, Application.Current.TryFindResource("housekeeper") as string ?? "Housekeeper" },
      {EAdditionalCosts.Cleaning, Application.Current.TryFindResource("cleaning") as string ?? "Cleaning" },
      {EAdditionalCosts.Gardening, Application.Current.TryFindResource("gardening") as string ?? "OthGardeninger" },
      {EAdditionalCosts.Electricity, Application.Current.TryFindResource("electricity") as string ?? "Electricity" },
      {EAdditionalCosts.Gas, Application.Current.TryFindResource("gas") as string ?? "Gas" },
      {EAdditionalCosts.Lift, Application.Current.TryFindResource("lift") as string ?? "Lift" },
      {EAdditionalCosts.TV, Application.Current.TryFindResource("tv") as string ?? "TV" },
      {EAdditionalCosts.Sewer, Application.Current.TryFindResource("sewer") as string ?? "Sewer" },
      {EAdditionalCosts.Garbage, Application.Current.TryFindResource("garbage") as string ?? "Garbage" },
    };
  }
}
