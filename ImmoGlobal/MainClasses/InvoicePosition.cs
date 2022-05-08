using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  /// <summary>
  /// model for the invoice positions in the invoice
  /// </summary>
  internal class InvoicePosition
  {
    public int InvoicePositionId { get; set; }
    public int InvoicePositionNumber { get; set; }
    public Property? Property { get; set; }
    public PropertyObject? PropertyObject { get; set; }
    public Invoice Invoice { get; set; }
    public double Value { get; set; }
    public EAdditionalCosts? AdditionalCostsCategory { get; set; }
    public Account Account { get; set; }


    internal Invoice GetInvoiceToInvoicePosition() => DbController.GetInvoiceToPositionDB(this);

    internal Account GetAccountToInvoicePosition() => DbController.GetAccountToInvoicePositionDB(this);

    internal Property? GetPropertyToInvoicePosition() => DbController.GetPropertyToInvoicePosition(this);

    internal PropertyObject? GetPropertyObjectToInvoicePosition() => DbController.GetPropertyObjectToInvoicePosition(this);

    /// <summary>
    /// maps the Additional cost enum to the string for the combobox
    /// </summary>
    public string AdditionalCostsCategoryString
    {
      get
      {
        return AdditionalCostsCategory switch
        {
          EAdditionalCosts.Water => Application.Current.TryFindResource("water") as string ?? "Water",
          EAdditionalCosts.Housekeeper => Application.Current.TryFindResource("housekeeper") as string ?? "Housekeeper",
          EAdditionalCosts.Cleaning => Application.Current.TryFindResource("cleaning") as string ?? "Cleaning",
          EAdditionalCosts.Gardening => Application.Current.TryFindResource("gardening") as string ?? "OthGardeninger",
          EAdditionalCosts.Electricity => Application.Current.TryFindResource("electricity") as string ?? "Electricity",
          EAdditionalCosts.Gas => Application.Current.TryFindResource("gas") as string ?? "Gas",
          EAdditionalCosts.Lift => Application.Current.TryFindResource("lift") as string ?? "Lift",
          EAdditionalCosts.TV => Application.Current.TryFindResource("tv") as string ?? "TV",
          EAdditionalCosts.Sewer => Application.Current.TryFindResource("sewer") as string ?? "Sewer",
          EAdditionalCosts.Garbage => Application.Current.TryFindResource("garbage") as string ?? "Garbage",
          _ => Application.Current.TryFindResource("none") as string ?? "none"
        };
      }
    }

    /// <summary>
    /// returns the string either of the property or the property object
    /// </summary>
    public string Assignment
    {
      get
      {
        var propj = GetPropertyObjectToInvoicePosition();
        var prop = GetPropertyToInvoicePosition();
        if (propj != null)
        {
          return propj.Description ?? "";
        }
        else if (prop != null)
        {
          return prop.Description ?? "";
        }
        return "";
      }
    }

    public string AccountDescription
    {
      get => GetAccountToInvoicePosition().Description ?? "";
    }
  }
}
