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
    
    internal static ICollection<Invoice> GetInvoiceToPerson(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              where p.Persona == persona
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
    /// Get Property Objects by Property from Database
    /// </summary>
    /// <param name="property"></param>
    /// <returns> List of PropertyObject</returns>
    internal static PropertyObject? GetPropertyObjectToRentalContractDB(RentalContract contract)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
              where p.RentalContracts.Contains(contract)
              select p).FirstOrDefault();
    }

    internal static bool DeletePropertyObjcetDB(int? propertyObjectId)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        var propertyObject = db.PropertyObjects.Find(propertyObjectId);
        db.PropertyObjects.Remove(propertyObject);
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {

        return false;
      }
    }

    /// <summary>
    /// Create and Update Property Object in DB
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns></returns>
    internal static bool UpsertPropertyObjectDB(PropertyObject propertyObject)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        if (propertyObject.Property != null)
          db.Attach(propertyObject.Property);
        if (propertyObject.PropertyObjectId == 0)
        {
          db.PropertyObjects.Add(propertyObject);
        }
        else
        {
          db.PropertyObjects.Update(propertyObject);
        }
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
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
    /// Create or update a property in DB
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static bool UpsertPropertyToDB(Property property)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        if (property.Housekeeper != null)
          db.Attach(property.Housekeeper);

        if (property.PropertyId == 0)
        {
          db.Properties.Add(property);
        }
        else
        {
          db.Properties.Update(property);
        }
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    internal static bool DeletePropertyDB(int? propertyId)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        var property = db.Properties.Find(propertyId);
        db.Properties.Remove(property);
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {

        return false;
      }
    }

    /// <summary>
    /// Return a List of RentalContracts related to a propertyObject
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
    /// Return a List of RentalContracts related to a persona
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>List of RentalContract</returns>
    internal static List<RentalContract> GetRentalContractsToPersonDB(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return (from r in db.RentalContracts
              where r.Renter == persona
              select r).ToList();
    }

    /// <summary>
    /// Return the Housekeeper to a property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static Persona GetHouskeeperToPropertyDB(Property property)
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
    internal static Persona GetRenterToRentalContractDB(RentalContract rentalContract)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.RentalContracts
              where p == rentalContract
              select p.Renter).First();
    }

    internal static bool UpsertPersonaToDB(Persona persona)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        if (persona.PersonaId == 0)
        {
          db.Personas.Add(persona);
        }
        else
        {
          db.Personas.Update(persona);
        }
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// returns all renters from DB
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Persona> GetAllRenters()
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Personas
              where p.IsRenter
              select p).ToList();
    }

    internal static ICollection<Persona> GetAllPersonasDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Personas.ToList();
    }

    internal static bool DeletePersonaDB(Persona selectedRenter)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        db.Personas.Remove(selectedRenter);
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    internal static Account GetAccountToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p == invoicePosition
              select p.Account).First();
    }
  }
}
