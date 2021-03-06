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
    /// returns user by given email, needed for login
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    internal static User? GetUserFromDb(string email)
    {
      using var db = new ImmoGlobalContext();
      return db.Users.FirstOrDefault(u => u.Email == email);
    }

    #region Get Invoice
    /// <summary>
    /// returns all invoices
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Invoice> GetAllInvoicesDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Invoices.ToList();
    }

    /// <summary>
    /// returns invoice to invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Invoice GetInvoiceToPositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return db.Invoices.First(i => i.InvoicePositions.Contains(invoicePosition));
    }

    /// <summary>
    /// returns invoice related to persona
    /// </summary>
    /// <param name="persona"></param>
    /// <returns></returns>
    internal static ICollection<Invoice> GetInvoiceToPersonaDB(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return db.Invoices.Where(i => i.Persona == persona).ToList();
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
      return db.InvoicePositions.Where(i => i.PropertyObject == propertyObject).ToList();
    }

    /// <summary>
    /// Return Invoice positions to property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static ICollection<InvoicePosition> GetInvoicePositionsToPropertyDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return db.InvoicePositions.Where(i => i.Property == property).ToList();
    }

    /// <summary>
    /// returns all Invoice Positions to an given invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static ICollection<InvoicePosition> GetInvoicePositionsToInvoiceDB(Invoice invoice)
    {
      using var db = new ImmoGlobalContext();
      return db.InvoicePositions.Where(i => i.Invoice == invoice).ToList();
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
      return db.PropertyObjects.ToList();
    }

    /// <summary>
    /// Get Property Objects by Property from Database
    /// </summary>
    /// <param name="property"></param>
    /// <returns> List of PropertyObject</returns>
    internal static ICollection<PropertyObject> GetPropertyObjectsToPropertyDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return db.PropertyObjects.Where(p => p.Property == property).ToList();
    }

    /// <summary>
    /// Get Property Objects by Property from Database
    /// </summary>
    /// <param name="property"></param>
    /// <returns> List of PropertyObject</returns>
    internal static PropertyObject? GetPropertyObjectToRentalContractDB(RentalContract contract)
    {
      using var db = new ImmoGlobalContext();
      return db.PropertyObjects.FirstOrDefault(p => p.RentalContracts.Contains(contract));
    }

    /// <summary>
    /// retruns property object to InvoicePosition
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static PropertyObject? GetPropertyObjectToInvoicePosition(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return db.InvoicePositions.Where(i => i == invoicePosition).Select(p => p.PropertyObject).FirstOrDefault();
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
      return db.InvoicePositions.Where(i => i == invoicePosition).Select(p => p.Property).FirstOrDefault();
    }

    /// <summary>
    /// Return a property from a propertyObject
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>Property</returns>
    internal static Property GetPropertyToPropertyObjectDB(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return db.PropertyObjects.Where(p => p == propertyObject).Select(p => p.Property).First();
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
      return db.RentalContracts.ToList();
    }

    /// <summary>
    /// Return a List of RentalContracts related to a propertyObject
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>List of RentalContract</returns>
    internal static ICollection<RentalContract> GetAllRentalContractsToPropertyObjectDB(PropertyObject propertyObject)
    {
      using var db = new ImmoGlobalContext();
      return db.RentalContracts.Where(r => r.PropertyObject == propertyObject).ToList();
    }

    /// <summary>
    /// Return a List of RentalContracts related to a persona
    /// </summary>
    /// <param name="propertyObject"></param>
    /// <returns>List of RentalContract</returns>
    internal static ICollection<RentalContract> GetRentalContractsToPersonDB(Persona persona)
    {
      using var db = new ImmoGlobalContext();
      return db.RentalContracts.Where(r => r.Renter == persona).ToList();
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
      return db.RentalContracts.Where(r => r == rentalContract).Select(p => p.Renter).First();
    }

    /// <summary>
    /// returns persona related to invoice
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    internal static Persona GetPersonaToInvoiceDB(Invoice invoice)
    {
      using var db = new ImmoGlobalContext();
      return db.Invoices.Where(i => i == invoice).Select(p => p.Persona).First();
    }

    /// <summary>
    /// Return the Housekeeper to a property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    internal static Persona GetHouskeeperToPropertyDB(Property property)
    {
      using var db = new ImmoGlobalContext();
      return db.Properties.Where(p => p == property).Select(h => h.Housekeeper).First();
    }
    /// <summary>
    /// returns all renters from DB
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Persona> GetAllRentersDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Personas.Where(p => p.IsRenter).ToList();
    }

    /// <summary>
    /// returns all creditors from DB
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<Persona> GetAllCreditorsDB()
    {
      using var db = new ImmoGlobalContext();
      return db.Personas.Where(p => p.IsCreditor).ToList();
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

    #region Get account related
    /// <summary>
    /// return account to invoice position
    /// </summary>
    /// <param name="invoicePosition"></param>
    /// <returns></returns>
    internal static Account GetAccountToInvoicePositionDB(InvoicePosition invoicePosition)
    {
      using var db = new ImmoGlobalContext();
      return db.InvoicePositions.Where(i => i == invoicePosition).Select(a => a.Account).First();
    }

    /// <summary>
    /// return all accounts from DB
    /// </summary>
    /// <returns></returns>
    internal static ICollection<Account> GetAllAccountsDB()
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
    internal static ICollection<PaymentRecord> GetIncomeToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return db.PaymentRecords.Where(p => p.Account == account).Where(i => i.IncomeAmount != null).ToList();
    }

    /// <summary>
    /// returns expense list of account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception></exception>
    internal static ICollection<PaymentRecord> GetExpenseToAccountDB(Account account)
    {
      using var db = new ImmoGlobalContext();
      return db.PaymentRecords.Where(p => p.Account == account).Where(i => i.ExpenseAmount != null).ToList();
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
      return db.BillReminders.Where(b => b.Invoice == invoice).ToList();
    }

    #region Insert and Edit
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
          db.Attach(property.Housekeeper);

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
          db.Attach(rentalContract.Renter);

        if (rentalContract.PropertyObject != null)
          db.Attach(rentalContract.PropertyObject);

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
          db.Attach(paymentRecord.Account);

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
        if (property != null)
        {
          db.Entry(property).State = EntityState.Deleted;
          db.Properties.Remove(property);
          db.SaveChanges();
          return true;
        }
        return false;
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
        if (propertyObject != null)
        {
          db.Entry(propertyObject).State = EntityState.Deleted;
          db.PropertyObjects.Remove(propertyObject);
          db.SaveChanges();
          return true;
        }
        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// delete rental contract
    /// </summary>
    /// <param name="rentalContract"></param>
    /// <returns></returns>
    internal static bool DeleteRentalContractDB(int? rentalContract)
    {
      try
      {
        using var db = new ImmoGlobalAuditableContext();
        var rc = db.RentalContracts.Find(rentalContract);
        if (rc != null)
        {
          db.Entry(rc).State = EntityState.Deleted;
          db.RentalContracts.Remove(rc);
          db.SaveChanges();
          return true;
        }
        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }
    #endregion
  }
}
