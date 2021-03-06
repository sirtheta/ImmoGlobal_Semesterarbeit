using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using ImmoGlobal.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ImmoGlobal.MainClasses
{
  /// <summary>
  /// model for the property
  /// </summary>
  internal class Property
  {
    public int PropertyId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }
    public string PropertyInsurance { get; set; }
    public string PersonInsurance { get; set; }
    public string LiabilityInsurance { get; set; }
    public Persona Housekeeper { get; set; }
    public ICollection<PropertyObject>? Objects { get; set; }

    public int NumberOfPropertyObjects
    {
      get => GetPropertyObjectsToProperty()?.Count ?? 0;
    }

    /// <summary>
    /// gets all PropertyObjects of the Property
    /// </summary>
    /// <returns></returns>
    internal ICollection<PropertyObject> GetPropertyObjectsToProperty() => DbController.GetPropertyObjectsToPropertyDB(this);

    /// <summary>
    /// get all invoice positions to Property
    /// </summary>
    /// <returns></returns>
    internal ICollection<InvoicePosition> GetInvoicePositions() => DbController.GetInvoicePositionsToPropertyDB(this);

    /// <summary>
    /// get a list of all invoices to the property
    /// </summary>
    /// <returns></returns>
    internal List<Invoice>? GetAllInvoicesRelatedToProperty()
    {
      List<Invoice> invoices = new();
      foreach (var item in GetPropertyObjectsToProperty())
      {
        invoices = new(item.GetInvoicesOfPropertyObject());
      }

      foreach (var item in GetInvoicePositions())
      {
        invoices.Add(item.GetInvoiceToInvoicePosition());
      }
      return invoices.DistinctBy(p => p.InvoiceId).ToList();
    }



    /// <summary>
    /// Gets all rental contracts of all PropertyObjects in a property
    /// </summary>
    private List<RentalContract>? RentalContractsInPropertyObject()
    {
      List<RentalContract> _rentalContracts = new();
      foreach (var item in GetPropertyObjectsToProperty())
      {
        foreach (var contract in item.GetRentalContractToPropertyObject())
        {
          _rentalContracts.Add(contract);
        }
      }
      return _rentalContracts;
    }

    /// <summary>
    /// Gets all activ rental contract in a property
    /// </summary>
    public int NumberOfActiveContracts
    {
      get => RentalContractsInPropertyObject()?.Count(p => p.ContractState == EContractState.Active) ?? 0;
    }

    /// <summary>
    /// Gets all inactive rental conracts in a property
    /// </summary>
    public int NumberOfNotRentedPropertyObjects
    {
      get => NumberOfPropertyObjects - NumberOfActiveContracts;
    }

    public string DescriptionAndAddress
    {
      get => $"{Description}, {Address}, {ZipCode} {City}";
    }

    /// <summary>
    /// returns string of housekeeper name
    /// </summary>
    /// <returns>string FirstName + LastName</returns>
    internal Persona GetHouskeeper() => DbController.GetHouskeeperToPropertyDB(this);


    public ICommand PropertyClickCommand
    {
      get => new RelayCommand<object>(PropertyClick);
    }

    private void PropertyClick(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedProperty = this;
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyObjectOverviewViewModel();
      }
    }

    // Set icon type and color if any invoice of the property object is overdue
    public string IconKind
    {
      get
      {
        if (GetAllInvoicesRelatedToProperty().Where(x => x.DueDate < System.DateTime.Now && x.InvoiceState == EInvoiceState.Released).Any())
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
        if (GetAllInvoicesRelatedToProperty().Where(x => x.DueDate < System.DateTime.Now && x.InvoiceState == EInvoiceState.Released).Any())
        {
          return "Red";
        }
        return "Green";
      }
    }
  }
}
