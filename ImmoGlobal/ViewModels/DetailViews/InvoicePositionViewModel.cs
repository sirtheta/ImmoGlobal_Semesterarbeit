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
    internal InvoicePositionViewModel()
    {
      PropertyCollection = new(DbController.GetAllPropertiesDB());
      AccountCollection = new(DbController.GetAllAccountsDB());
    }

    internal InvoicePositionViewModel(InvoicePosition invoicePosition)
    {
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
      {EAdditionalCosts.None, Application.Current.FindResource("none") as string ?? "none" },
      {EAdditionalCosts.Water, Application.Current.FindResource("water") as string ?? "Water" },
      {EAdditionalCosts.Housekeeper, Application.Current.FindResource("housekeeper") as string ?? "Housekeeper" },
      {EAdditionalCosts.Cleaning, Application.Current.FindResource("cleaning") as string ?? "Cleaning" },
      {EAdditionalCosts.Gardening, Application.Current.FindResource("gardening") as string ?? "OthGardeninger" },
      {EAdditionalCosts.Electricity, Application.Current.FindResource("electricity") as string ?? "Electricity" },
      {EAdditionalCosts.Gas, Application.Current.FindResource("gas") as string ?? "Gas" },
      {EAdditionalCosts.Lift, Application.Current.FindResource("lift") as string ?? "Lift" },
      {EAdditionalCosts.TV, Application.Current.FindResource("tv") as string ?? "TV" },
      {EAdditionalCosts.Sewer, Application.Current.FindResource("sewer") as string ?? "Sewer" },
      {EAdditionalCosts.Garbage, Application.Current.FindResource("garbage") as string ?? "Garbage" },
    };
  }
}
