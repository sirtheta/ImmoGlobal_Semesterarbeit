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
  internal class PropertyObject
  {
    public int PropertyObjectId { get; set; }
    public string? Description { get; set; }
    public EPropertyObjectType ObjectType { get; set; }
    public Property? Property { get; set; }
    public string? Location { get; set; }
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

    public ICollection<InvoicePosition> GetInvoicePositions()
    {
      return DbController.GetInvoicePositionsToPropertyObjectDB(this);
    }

    public ICollection<RentalContract> GetRentalContractToObject()
    {
      return DbController.GetAllRentalContractsToPropertyObjectDB(this);
    }

    private Property? GetPropertyFromDb()
    {
      return DbController.GetPropertyWithObjectDB(this);
    }

    public string PropertyDescription => GetPropertyFromDb()?.Description ?? "";

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
  }
}
