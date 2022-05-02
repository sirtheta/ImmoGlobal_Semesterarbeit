using ImmoGlobal.Helpers;
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
    /// returns user by given email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    internal static User? GetUserFromDb(string email)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Users
              where p.Email == email
              select p).SingleOrDefault();
    }

    #region Get Invoice
    /// <summary>
    /// returns all invoices
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Invoice> GetAllInvoicesDB()
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              select p).ToList();
    }
    
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
    /// returns invoice related to persona
    /// </summary>
    /// <param name="persona"></param>
    /// <returns></returns>
    internal static ICollection<Invoice> GetInvoiceToPersonaDB(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              where p.Persona == persona
              select p).ToList();
    }

    #endregion  

    #region Get InvoicePosition
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
    /// returns all Invoice Positions to an given invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static ICollection<InvoicePosition> GetInvoicePositionsToInvoiceDB(Invoice invoice)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p.Invoice == invoice
              select p).ToList();
    }
    #endregion

    #region get PropertyObject
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
    /// retruns property object to InvoicePosition
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static PropertyObject? GetPropertyObjectToInvoicePosition(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p == invoicePosition
              select p.PropertyObject).FirstOrDefault();
    }
    #endregion

    #region get Property
    /// <summary>
    /// retruns property to InvoicePosition
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Property? GetPropertyToInvoicePosition(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.InvoicePositions
              where p == invoicePosition
              select p.Property).FirstOrDefault();
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
    internal static ICollection<Property> GetAllPropertiesDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Properties.ToList();
    }
    #endregion

    #region get RentalContract
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
    internal static ICollection<RentalContract> GetAllRentalContractsToPropertyObjectDB(PropertyObject propertyObject)
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
    internal static ICollection<RentalContract> GetRentalContractsToPersonDB(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return (from r in db.RentalContracts
              where r.Renter == persona
              select r).ToList();
    }
    #endregion

    #region Get Personas
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
    /// returns persona related to invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static Persona GetPersonaToInvoiceDB(Invoice invoice)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.Invoices
              where p == invoice
              select p.Persona).First();
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
    #endregion

    #region account related
    /// <summary>
    /// return account to invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Account GetAccountToInvoicePositionDB(InvoicePosition invoicePosition)
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


    /// <summary>
    /// returns income list of account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception></exception>
    internal static IEnumerable<PaymentRecord> GetIncomeToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PaymentRecords
              where p.Account == account
              where p.IncomeAmount != null
              select p).ToList();
    }

    /// <summary>
    /// returns expense list of account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception></exception>
    internal static IEnumerable<PaymentRecord> GetExpenseToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.PaymentRecords
              where p.Account == account
              where p.ExpenseAmount != null
              select p).ToList();
    }
    #endregion

    /// <summary>
    /// returns bill reminders related to invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static ICollection<BillReminder>? GetBillRemindersToInvoiceDB(Invoice invoice)
    {
      using var db = new ImmoGlobalContext();
      return (from p in db.BillReminders
              where p.Invoice == invoice
              select p).ToList();
    }

    #region UPSERT
    /// <summary>
    /// Create or update a property in DB
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static bool UpsertPropertyToDB(Property property)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (property.Housekeeper != null)
        {
          db.Attach(property.Housekeeper);
        }

        if (property.PropertyId == 0)
        {
          db.Properties.Add(property);
        }
        else
        {
          var prop = db.Properties.Find(property.PropertyId);
          ClassMapper.CopyValues(prop, property);
          db.Properties.Update(prop);
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
    /// Create and Update Property Object in DB
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns></returns>
    internal static bool UpsertPropertyObjectDB(PropertyObject propertyObject)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (propertyObject.Property != null)
          db.Attach(propertyObject.Property);
        if (propertyObject.PropertyObjectId == 0)
        {
          db.PropertyObjects.Add(propertyObject);
        }
        else
        {
          var proObj = db.PropertyObjects.Find(propertyObject.PropertyObjectId);
          ClassMapper.CopyValues(proObj, propertyObject);
          db.PropertyObjects.Update(proObj);
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
    /// update or create a rental contract
    /// </summary>
    /// <param name="rentalContract"></param>
    /// <returns></returns>
    internal static bool UpsertRentalContractToDB(RentalContract rentalContract)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (rentalContract.Renter != null)
        {
          db.Attach(rentalContract.Renter);
        }
        if (rentalContract.PropertyObject != null)
        {
          db.Attach(rentalContract.PropertyObject);
        }
        if (rentalContract.RentalContractId == 0)
        {
          db.RentalContracts.Add(rentalContract);
        }
        else
        {
          var rc = db.RentalContracts.Find(rentalContract.RentalContractId);
          ClassMapper.CopyValues(rc, rentalContract);
          db.RentalContracts.Update(rc);
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
    /// update or create a new persona
    /// </summary>
    /// <param name="persona"></param>
    /// <returns></returns>
    internal static bool UpsertPersonaToDB(Persona persona)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (persona.PersonaId == 0)
        {
          db.Personas.Add(persona);
        }
        else
        {
          var per = db.Personas.Find(persona.PersonaId);
          ClassMapper.CopyValues(per, persona);
          db.Personas.Update(per);
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
    /// update or create a new account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    internal static bool UpsertAccountToDB(Account account)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (account.AccountId == 0)
        {
          db.Accounts.Add(account);
        }
        else
        {
          var acc = db.Accounts.Find(account.AccountId);
          ClassMapper.CopyValues(acc, account);
          db.Accounts.Update(acc);
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
    /// create or update a new payment record
    /// </summary>
    /// <param name="paymentRecord"></param>
    /// <returns></returns>
    internal static bool UpsertPaymentRecordToDB(PaymentRecord paymentRecord)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (paymentRecord.Account != null)
        {
          db.Attach(paymentRecord.Account);
        }
        if (paymentRecord.PaymentRecordId == 0)
        {
          db.PaymentRecords.Add(paymentRecord);
        }
        else
        {
          var pay = db.PaymentRecords.Find(paymentRecord.PaymentRecordId);
          ClassMapper.CopyValues(pay, paymentRecord);
          db.PaymentRecords.Update(paymentRecord);
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
    /// save or update invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static bool UpsertInvoiceToDB(Invoice invoice)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (invoice.Persona != null)
          db.Attach(invoice.Persona);
        if (invoice.InvoiceId == 0)
        {
          db.Invoices.Add(invoice);
        }
        else
        {
          var inv = db.Invoices.Find(invoice.InvoiceId);
          ClassMapper.CopyValues(inv, invoice);
          db.Invoices.Update(inv);
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
    /// save or update invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static bool UpsertInvoicePositionToDB(InvoicePosition invoicePosition)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (invoicePosition.Invoice != null)
        {
          db.Entry(invoicePosition.Invoice).State = EntityState.Modified;
          db.Attach(invoicePosition.Invoice);

        }
        if (invoicePosition.Property != null)
        {
          db.Entry(invoicePosition.Property).State = EntityState.Modified;
          db.Attach(invoicePosition.Property);

        }
        if (invoicePosition.PropertyObject != null)
        {
          db.Entry(invoicePosition.PropertyObject).State = EntityState.Modified;
          db.Attach(invoicePosition.PropertyObject);

        }
        if (invoicePosition.Account != null)
        {
          db.Entry(invoicePosition.Account).State = EntityState.Modified;
          db.Attach(invoicePosition.Account);
        }
        if (invoicePosition.InvoicePositionId == 0)
        {
          db.InvoicePositions.Add(invoicePosition);
        }
        else
        {
          var invp = db.InvoicePositions.Find(invoicePosition.InvoicePositionId);
          ClassMapper.CopyValues(invp, invoicePosition);
          db.InvoicePositions.Update(invp);
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
    /// create or update bill reminder
    /// </summary>
    /// <param name="billReminder"></param>
    /// <returns></returns>
    internal static bool UpsertBillReminderToDB(BillReminder billReminder)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        if (billReminder.Invoice != null)
          db.Attach(billReminder.Invoice);
        if (billReminder.BillReminderId == 0)
        {
          db.BillReminders.Add(billReminder);
        }
        else
        {
          var br = db.BillReminders.Find(billReminder.BillReminderId);
          ClassMapper.CopyValues(br, billReminder);
          db.BillReminders.Update(br);
        }
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
    #endregion
    
    #region Delete
    /// <summary>
    /// delete a property with specific id
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    internal static bool DeletePropertyDB(int? propertyId)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
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
    /// delete a property object
    /// </summary>
    /// <param name="propertyObjectId"></param>
    /// <returns></returns>
    internal static bool DeletePropertyObjcetDB(int? propertyObjectId)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
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
    #endregion
  }
}
