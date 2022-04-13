using ImmoGlobal.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public static List<PropertyObject> GetPropertyObjectsWithIdFromDb(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p.Property == property
              select p).ToList();
    }

    /// <summary>
    /// Return renter to a contract
    /// </summary>
    /// <param name="rentalContract"></param>
    /// <returns>Persona</returns>
    internal static Persona? GetRenterFromDb(RentalContract rentalContract)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.RentalContracts
              where p == rentalContract
              select p.Renter).FirstOrDefault();
    }

    /// <summary>
    /// Return a property from a propertyObject
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>Property</returns>
    internal static Property? GetPropertyWithObject(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p == propertyObject
              select p.Property).FirstOrDefault();
    }

    /// <summary>
    /// Return a List of RentalContracts
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>List of RentalContract</returns>
    internal static List<RentalContract> GetAllRentalContractsToPropertyObject(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return (from r in db.RentalContracts
              where r.PropertyObject == propertyObject
              select r).ToList();
    }

    /// <summary>
    /// Return the Housekeeper to a property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static Persona? GetHouskeeperFromDb(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Properties
              where p == property
              select p.Housekeeper).FirstOrDefault();

    }
  }
}
