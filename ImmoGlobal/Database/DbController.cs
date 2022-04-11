using ImmoGlobal.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.Database
{
  internal class DbController
  {
    /// <summary>
    /// Get Properties from DB
    /// </summary>
    /// <returns>
    /// List of Properties
    /// </returns>
    internal static List<Property> GetPropertiesFromDb()
    {
      using var db = new ImmoGlobalContext();
      return db.Properties.ToList();
    }
    /// <summary>
    /// Get Property Objects by Property from Database
    /// </summary>
    /// <param name="property"></param>
    /// <returns> List of PropertyObject</returns>
    public static List<PropertyObject> GetPropertiesWithIdFromDb(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p.Property == property
              select p).ToList();
    }

    internal static List<RentalContract> GetAllRentalContractsToPropertyObject(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return (from r in db.RentalContracts
              where r.PropertyObject == propertyObject
              select r).ToList();
    }
  }
}
