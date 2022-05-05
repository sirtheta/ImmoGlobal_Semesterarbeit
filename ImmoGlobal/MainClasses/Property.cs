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
    public string? Description { get; set; }
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
      get
      {
        return GetPropertyObjects()?.Count ?? 0;
      }
    }

    /// <summary>
    /// gets all PropertyObjects of the Property
    /// </summary>
    /// <returns></returns>
    internal List<PropertyObject> GetPropertyObjects()
    {
      return new(DbController.GetPropertyObjectsToPropertyDB(this));
    }

    /// <summary>
    /// get all invoice positions to Property
    /// </summary>
    /// <returns></returns>
    internal ICollection<InvoicePosition> GetInvoicePositions()
    {
      return DbController.GetInvoicePositionsToPropertyDB(this);
    }

    /// <summary>
    /// get a list of all invoices to the property
    /// </summary>
    /// <returns></returns>
    internal List<Invoice>? GetAllInvoicesRelatedToProperty()
    {
      List<Invoice> invoices = new();
      foreach (var item in GetPropertyObjects())
      {
        invoices = new(item.GetInvoicesOfPropertyObject());
      }

      foreach (var item in GetInvoicePositions())
      {
        invoices.Add(item.GetInvoiceToInvoicePosition());
      }
      return new List<Invoice>(invoices.DistinctBy(p => p.InvoiceId));
    }


    private readonly List<RentalContract> _rentalContracts = new();

    /// <summary>
    /// Gets all rental contracts of all PropertyObjects in a property
    /// </summary>
    private List<RentalContract>? RentalContracts()
    {
      foreach (var item in GetPropertyObjects())
      {
        foreach (var contract in DbController.GetAllRentalContractsToPropertyObjectDB(item).ToList())
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
      get => RentalContracts()?.Where(x => x.ContractState == EContractState.Active).Count() ?? 0;

    }

    /// <summary>
    /// Gets all inactive rental conracts in a property
    /// </summary>
    public int NumberOfInactiveContracts
    {
      get => RentalContracts()?.Where(x => x.ContractState != EContractState.Active).Count() ?? 0;
    }

    public string DescriptionAndAddress
    {
      get => $"{Description}, {Address}, {ZipCode} {City}";
    }

    /// <summary>
    /// returns string of housekeeper name
    /// </summary>
    /// <returns>string FirstName + LastName</returns>
    internal Persona GetHouskeeper()
    {
      return DbController.GetHouskeeperToPropertyDB(this);
    }

    public ICommand PropertyClickCommand
    {
      get => new RelayCommand<object>(PropertyClick);
    }

    private void PropertyClick(object obj)
    {
      if (MainWindowViewModel.GetInstance != null)
      {
        MainWindowViewModel.GetInstance.SelectedProperty = this;
        MainWindowViewModel.GetInstance.SelectedViewModel = new PropertyObjectOverviewViewModel(GetPropertyObjects(), GetHouskeeper(), Description ?? "no description found");
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
