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
    public Property? Property { get; set; }
    public EPropertyObjectType ObjectType { get; set; }
    public string? Description { get; set; }
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


    private List<RentalContract> GetRentalContractToObject()
    {
      return DbController.GetAllRentalContractsToPropertyObject(this).ToList();
    }

    public string CurrentRenter
    {
      get
      {        
        return GetRentalContractToObject().Where(x => x.ContractState == EContractState.Active).FirstOrDefault()?.GetRenterFullName() ?? 
          Application.Current.FindResource("notRented2") as string ?? "not rented";
      }
    }

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
      get { return new RelayCommand<object>(PropertyClick); }
    }

    private void PropertyClick(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        // to implement
      }
    }
  }
}
