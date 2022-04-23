using ImmoGlobal.MainClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmoGlobal.Database
{
  internal class DbController
  {
    /// <summary>
    /// returns invoice to invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Invoice GetInvoiceToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              where p.InvoicePositions.Contains(invoicePosition)
              select p).First();
    }

    /// <summary>
    /// returns income list of account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception></exception>
    internal static IEnumerable<Income> GetIncomeToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Incomes
              where p.Account == account
              select p).ToList();
    }

    /// <summary>
    /// returns expense list of account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception></exception>
    internal static IEnumerable<Expense> GetExpenseToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Expenses
              where p.Account == account
              select p).ToList();
    }

    /// <summary>
    /// returns invoice related to persona
    /// </summary>
    /// <param name="persona"></param>
    /// <returns></returns>
    internal static ICollection<Invoice> GetInvoiceToPersonDB(Persona persona)
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
    /// returns all property objects
    /// </summary>
    /// <returns></returns>
    internal static ICollection<PropertyObject> GetAllPropertyObjectsDB()
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PropertyObjects
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

    /// <summary>
    /// delete a property object
    /// </summary>
    /// <param name="propertyObjectId"></param>
    /// <returns></returns>
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
    internal static Property GetPropertyToPropertyObjectDB(PropertyObject propertyObject)
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
    internal static List<Property> GetAllPropertiesDB()
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
        {
          db.Entry(property.Housekeeper).State = EntityState.Modified;
          db.Attach(property.Housekeeper);
        }

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

    /// <summary>
    /// delete a property with specific id
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
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
    /// update or create a rental contract
    /// </summary>
    /// <param name="rentalContract"></param>
    /// <returns></returns>
    internal static bool UpsertRentalContractToDB(RentalContract rentalContract)
    {
      try
      {
        using var db = new ImmoGlobalContext();
        if (rentalContract.Renter != null)
        {
          db.Entry(rentalContract.Renter).State = EntityState.Modified;
          db.Attach(rentalContract.Renter);
        }
        if (rentalContract.PropertyObject != null)
        {
          db.Entry(rentalContract.PropertyObject).State = EntityState.Modified;
          db.Attach(rentalContract.PropertyObject);
        }
        if (rentalContract.RentalContractId == 0)
        {
          db.RentalContracts.Add(rentalContract);
        }
        else
        {
          db.RentalContracts.Update(rentalContract);
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
    /// returns all rental contracts from DB
    /// </summary>
    /// <returns></returns>
    internal static ICollection<RentalContract> GetAllRentalContractsDB()
    {
      using var db = new ImmoGlobalContext();
      return (from r in db.RentalContracts
              select r).ToList();
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

    /// <summary>
    /// update or create a new persona
    /// </summary>
    /// <param name="persona"></param>
    /// <returns></returns>
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
    internal static ICollection<Persona> GetAllRentersDB()
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Personas
              where p.IsRenter
              select p).ToList();
    }

    /// <summary>
    /// returns all creditors from DB
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Persona> GetAllCreditorsDB()
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Personas
              where p.IsCreditor
              select p).ToList();
    }

    /// <summary>
    /// return all personas from DB
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Persona> GetAllPersonasDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Personas.ToList();
    }

    /// <summary>
    /// delete Persona
    /// </summary>
    /// <param name="selectedRenter"></param>
    /// <returns></returns>
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

    /// <summary>
    /// return account to invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Account GetAccountToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p == invoicePosition
              select p.Account).First();
    }

    /// <summary>
    /// return all accounts from DB
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Account> GetAllAccountsDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Accounts.ToList();
    }
  }
}
