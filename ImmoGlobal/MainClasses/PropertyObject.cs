using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using ImmoGlobal.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.MainClasses
{
  /// <summary>
  /// model for the property object in a property
  /// </summary>
  internal class PropertyObject
  {
    public int PropertyObjectId { get; set; }
    public string? Description { get; set; }
    public EPropertyObjectType ObjectType { get; set; }
    public Property Property { get; set; }
    public string Location { get; set; }
    public double NumberOfRooms { get; set; }
    public double Area { get; set; }
    public int? NumberOfKeys { get; set; }
    public bool Fridge { get; set; }
    public bool Dishwasher { get; set; }
    public bool Stove { get; set; }
    public bool Oven { get; set; }
    public bool WashingMachine { get; set; }
    public bool Tumbler { get; set; }
    public ICollection<RentalContract>? RentalContracts { get; set; }

    internal ICollection<InvoicePosition> GetInvoicePositions()
    {
      return DbController.GetInvoicePositionsToPropertyObjectDB(this);
    }

    /// <summary>
    /// returns all invoices related to the property object
    /// </summary>
    /// <returns></returns>
    internal List<Invoice>? GetInvoicesOfPropertyObject()
    {
      var invoices = new List<Invoice>();
      foreach (var item in GetInvoicePositions())
      {
        invoices.Add(item.GetInvoiceToInvoicePosition());
      }
      return new List<Invoice>(invoices.DistinctBy(p => p.InvoiceId));
    }

    /// <summary>
    /// returns a collection of rental contracts related to the property
    /// </summary>
    /// <returns></returns>
    internal ICollection<RentalContract> GetRentalContractToObject()
    {
      return DbController.GetAllRentalContractsToPropertyObjectDB(this);
    }

    /// <summary>
    /// returns property relatet to this property object
    /// </summary>
    /// <returns></returns>
    internal Property? GetPropertyToPropertyObject()
    {
      return DbController.GetPropertyToPropertyObjectDB(this);
    }

    public string PropertyDescription => GetPropertyToPropertyObject()?.Description ?? "";

    public string CurrentRenter
    {
      get => GetRentalContractToObject().Where(x => x.ContractState == EContractState.Active).FirstOrDefault()?.RenterFullName ??
          Application.Current.FindResource("notRented2") as string ?? "not rented";
    }

    /// <summary>
    /// gets the Object Type name from enum to a translated string
    /// </summary>
    public string ObjectTypeName
    {
      get
      {
        return ObjectType switch
        {
          EPropertyObjectType.House => Application.Current.FindResource("house") as string ?? "house",
          EPropertyObjectType.Apartment => Application.Current.FindResource("apartment") as string ?? "apartment",
          EPropertyObjectType.Room => Application.Current.FindResource("room") as string ?? "room",
          EPropertyObjectType.Garage => Application.Current.FindResource("garage") as string ?? "garage",
          EPropertyObjectType.Office => Application.Current.FindResource("office") as string ?? "office",
          EPropertyObjectType.Parking => Application.Current.FindResource("parking") as string ?? "parking",
          _ => "string not defined",
        };
      }
    }

    public ICommand PropertyObjectClickCommand
    {
      get => new RelayCommand<object>(PropertyClick);
    }

    private void PropertyClick(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedPropertyObject = this;
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyObjectViewModel(this);
      }
    }

    // Gets translatet string for boolean, yes or no
    public string FridgeString
    {
      get => Fridge ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string DishwasherString
    {
      get => Dishwasher ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string StoveString
    {
      get => Stove ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string OvenString
    {
      get => Oven ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string WashingMachineString
    {
      get => WashingMachine ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string TumblerString
    {
      get => Tumbler ? Application.Current.FindResource("yes") as string ?? "yes" : Application.Current.FindResource("no") as string ?? "no";
    }
    public string? NumberOfKeysString
    {
      get => NumberOfKeys > 0 ? NumberOfKeys.ToString() : Application.Current.FindResource("none") as string ?? "none";
    }

    // Set icon type and color if any invoice of the property object is overdue
    public string IconKind
    {
      get
      {
        if (GetInvoicesOfPropertyObject().Where(x => x.DueDate < System.DateTime.Now && x.InvoiceState == EInvoiceState.Released).Any())
        {
          return "Error";
        }
        return "TickCircle";
      }
    }

    public string IconColor
    {
      get
      {
        if (GetInvoicesOfPropertyObject().Where(x => x.DueDate < System.DateTime.Now && x.InvoiceState == EInvoiceState.Released).Any())
        {
          return "Red";
        }
        return "Green";
      }
    }
  }
}
