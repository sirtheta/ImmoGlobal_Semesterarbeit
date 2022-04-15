using ImmoGlobal.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmoGlobal.Database
{
  internal class DbController
  {
    internal static Invoice GetInvoiceToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              where p.InvoicePositions.Contains(invoicePosition)
              select p).First();
    }

    internal static Account GetAccountToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p == invoicePosition
              select p.Account).First();
    }

    /// <summary>
    /// Get Property Objects by Property from Database
    /// </summary>
    /// <param name="property"></param>
    /// <returns> List of PropertyObject</returns>
    internal static ICollection<PropertyObject> GetPropertyObjectsToPropertyDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p.Property == property
              select p).ToList();
    }
    /// <summary>
    /// Return invoice positions to propertyObject
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns></returns>
    internal static ICollection<InvoicePosition> GetInvoicePositionsToPropertyObjectDB(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p.PropertyObject == propertyObject
              select p).ToList();
    }
    /// <summary>
    /// Return Invoice positions to property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static ICollection<InvoicePosition> GetInvoicePositionsToPropertyDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p.Property == property
              select p).ToList();
    }

    /// <summary>
    /// Get Properties from DB
    /// </summary>
    /// <returns>
    /// List of Properties
    /// </returns>
    internal static List<Property> GetPropertiesDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Properties.ToList();
    }

    /// <summary>
    /// Return a property from a propertyObject
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>Property</returns>
    internal static Property GetPropertyWithObjectDB(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p == propertyObject
              select p.Property).First();
    }

    internal static bool AddNewPropertyDB(Property property)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        if (property.Housekeeper != null)
          db.Attach(property.Housekeeper);
        db.Properties.Add(property);
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Return a List of RentalContracts
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>List of RentalContract</returns>
    internal static List<RentalContract> GetAllRentalContractsToPropertyObjectDB(PropertyObject propertyObject)
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
    internal static Persona GetHouskeeperDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Properties
              where p == property
              select p.Housekeeper).First();
    }

    /// <summary>
    /// Return renter to a contract
    /// </summary>
    /// <param name="rentalContract"></param>
    /// <returns>Persona</returns>
    internal static Persona GetRenterDB(RentalContract rentalContract)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.RentalContracts
              where p == rentalContract
              select p.Renter).First();
    }

    internal static ICollection<Persona> GetAllPersonasDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Personas.ToList();
    }
  }
}
