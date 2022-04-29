using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  internal class InvoicePosition
  {
    public int InvoicePositionId { get; set; }
    public int InvoicePositionNumber { get; set; }
    public Property? Property { get; set; }
    public PropertyObject? PropertyObject { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public double Value { get; set; }
    public EAdditionalCosts? AdditionalCostsCategory { get; set; }
    public Account Account { get; set; }


    internal Invoice GetInvoiceToInvoicePosition()
    {
      return DbController.GetInvoiceToPositionDB(this);
    }

    internal Account GetAccountToInvoicePosition()
    {
      return DbController.GetAccountToInvoicePositionDB(this);
    }

    internal Property? GetPropertyToInvoicePosition()
    {
      return DbController.GetPropertyToInvoicePosition(this);
    }

    internal PropertyObject? GetPropertyObjectToInvoicePosition()
    {
      return DbController.GetPropertyObjectToInvoicePosition(this);
    }

    public string AdditionalCostsCategoryString
    {
      get
      {
        return AdditionalCostsCategory switch
        {
          EAdditionalCosts.Water => Application.Current.FindResource("water") as string ?? "Water",
          EAdditionalCosts.Housekeeper => Application.Current.FindResource("housekeeper") as string ?? "Housekeeper",
          EAdditionalCosts.Cleaning => Application.Current.FindResource("cleaning") as string ?? "Cleaning",
          EAdditionalCosts.Gardening => Application.Current.FindResource("gardening") as string ?? "OthGardeninger",
          EAdditionalCosts.Electricity => Application.Current.FindResource("electricity") as string ?? "Electricity",
          EAdditionalCosts.Gas => Application.Current.FindResource("gas") as string ?? "Gas",
          EAdditionalCosts.Lift => Application.Current.FindResource("lift") as string ?? "Lift",
          EAdditionalCosts.TV => Application.Current.FindResource("tv") as string ?? "TV",
          EAdditionalCosts.Sewer => Application.Current.FindResource("sewer") as string ?? "Sewer",
          EAdditionalCosts.Garbage => Application.Current.FindResource("garbage") as string ?? "Garbage",
          _ => Application.Current.FindResource("none") as string ?? "none"
        };
      }
    }

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
      get
      {
        return GetAccountToInvoicePosition().Description ?? "";
      }
    }
  }
}
