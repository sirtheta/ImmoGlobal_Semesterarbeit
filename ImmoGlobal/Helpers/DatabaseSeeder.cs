using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;

namespace ImmoGlobal.Helpers
{
  internal static class DatabaseSeeder
  {

    internal static void CreateTestEntries()
    {

      var _renter = new Persona("Musterman", "Max", 0334328978, "mail@mail.de", DateTime.Parse("25.10.1988"), "Barstreet 5", 3612, "Steffisburg", ECivilState.Married, "Thunstr. 15, 3600 Bern", "12ET34UI56789", 0791234567);
      var _creditor = new Persona(true, "Creditors Company Name", "Creditors Company Address", 3800, "Interlaken", 0797891236, "Creditor ContactPerson", "CreditorName", "CreditorSurname", "mail@test.de", "CHE-123.456.789");
      var _housekeeper = new Persona("Houskeeperman", "Moritz", 0238905678, "moritz@mail.de", "Obsolet Adress 4", 2344, "Ronmalingen", "893489uip099322", 8907894561);
      var _housekeeper2 = new Persona("Schneider", "Peter", 0236565678, "peter@schneider.de", "Obsolet Adress 5", 3262, "Tübingen", "893489uip6544542", 5896314782);

      var _property1 = new Property() { Housekeeper = _housekeeper, Description = "TestLiegenschaft1", Address = "TestAdress1", ZipCode = 3612, City = "TestCity1", PropertyInsurance = "Helvetia1", PersonInsurance = "Mobiliar1", LiabilityInsurance = "Emmitaler1" };
      var _property2 = new Property() { Housekeeper = _housekeeper2, Description = "TestLiegenschaft2", Address = "TestAdress2", ZipCode = 3613, City = "TestCity2", PropertyInsurance = "Helvetia2", PersonInsurance = "Mobiliar2", LiabilityInsurance = "Emmitaler2" };

      var _objectHouse = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.House, Description = "Wohnung 1", Location = "TestLocation", NumberOfRooms = 4.5, Area = 100, NumberOfKeys = 5, Fridge = true, Dishwasher = true, Stove = true, Oven = true };
      var _objectGarage = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.Garage, Description = "Garage von Wohnung 1", Location = "irgendwo nebenan", Area = 15 };
      var _objectRoom = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.Room, Description = "Partyraum", Location = "Sous partere", NumberOfRooms = 1, Area = 10, NumberOfKeys = 2 };
      var _objectOffice = new PropertyObject() { Property = _property2, ObjectType = EPropertyObjectType.Office, Description = "Office in " + _property1.Description, Location = "4 Stock", NumberOfRooms = 2, Area = 35, NumberOfKeys = 3, Fridge = true, Dishwasher = true };

      var _rentalContract1 = new RentalContract() { Renter = _renter, PropertyObject = _objectHouse, RentStartDate = DateTime.Parse("01.04.2022"), RentEndDate = DateTime.Parse("31.10.2030"), Rent = 1650, Deposit = true, ContractState = EContractState.Active };
      var _rentalContract2 = new RentalContract() { Renter = _renter, PropertyObject = _objectGarage, RentStartDate = DateTime.Parse("01.11.2030"), Rent = 1850, Deposit = true, ContractState = EContractState.Singend };
      var _rentalContract3 = new RentalContract() { Renter = _renter, PropertyObject = _objectOffice, RentStartDate = DateTime.Parse("01.04.2018"), RentEndDate = DateTime.Parse("31.03.2022"), Rent = 1450, Deposit = true, ContractState = EContractState.Canceled };

      var _account1 = new Account() { AccountNumber = "5634RE56034", Description = "Konto 1" };
      var _account2 = new Account() { AccountNumber = "23VFYYXX034", Description = "Konto 2" };
      var _account3 = new Account() { AccountNumber = "DSDD2334445", Description = "Konto 3" };

      var _income1 = new PaymentRecord() { Account = _account1, IncomeAmount = 345.05, Description = "SeedDescriptonIncome1", ReceiptNumber = 20, Date = DateTime.Now.AddDays(-5) };
      var _income2 = new PaymentRecord() { Account = _account2, IncomeAmount = 315.10, Description = "SeedDescriptonIncome2", ReceiptNumber = 40, Date = DateTime.Now.AddDays(-10) };
      var _income3 = new PaymentRecord() { Account = _account3, IncomeAmount = 395.75, Description = "SeedDescriptonIncome3", ReceiptNumber = 60, Date = DateTime.Now.AddDays(-3) };

      var _expense1 = new PaymentRecord() { Account = _account1, ExpenseAmount = 268.65, Description = "SeedDescriptionExpense1", ReceiptNumber = 10, Date = DateTime.Now.AddDays(-25) };
      var _expense2 = new PaymentRecord() { Account = _account2, ExpenseAmount = 295.95, Description = "SeedDescriptionExpense2", ReceiptNumber = 30, Date = DateTime.Now.AddDays(-15) };
      var _expense3 = new PaymentRecord() { Account = _account3, ExpenseAmount = 252.15, Description = "SeedDescriptionExpense3", ReceiptNumber = 50, Date = DateTime.Now.AddDays(-8) };

      var _invoice1 = new Invoice()
      {
        Persona = _renter,
        InvoiceDate = DateTime.Parse("01.03.2022"),
        DueDate = DateTime.Parse("01.04.2022"),
        InvoicePurpose = "Ihre Miete",
        InvoiceState = EInvoiceState.OverDue,
        InvoiceCategory = EInvoiceCategory.Rent
      };

      var _invoice2 = new Invoice()
      {
        Persona = _renter,
        InvoiceDate = DateTime.Parse("05.01.2022"),
        DueDate = DateTime.Parse("28.02.2022"),
        InvoicePurpose = "Nebenkostenabrechnung",
        InvoiceState = EInvoiceState.Paid,
        InvoiceCategory = EInvoiceCategory.AdditionalCosts
      };

      var _invoice3 = new Invoice()
      {
        Persona = _creditor,
        InvoiceDate = DateTime.Parse("15.04.2022"),
        DueDate = DateTime.Parse("15.05.2022"),
        InvoicePurpose = "Rechnung zu Liegenschaft",
        InvoiceState = EInvoiceState.Released,
        InvoiceCategory = EInvoiceCategory.Property
      };


      //additionalCosts prop 1
      var _invoicePosition1 = new InvoicePosition()
      {
        InvoicePositionNumber = 1,
        Property = _property1,
        Value = 1320,
        AdditionalCostsCategory = EAdditionalCosts.Electricity,
        Account = _account3,
        Invoice = _invoice3
      };

      var _invoicePosition2 = new InvoicePosition()
      {
        InvoicePositionNumber = 2,
        Property = _property1,
        Value = 45.23,
        AdditionalCostsCategory = EAdditionalCosts.Gas,
        Account = _account3,
        Invoice = _invoice3
      };

      //rent
      var _invoicePosition3 = new InvoicePosition()
      {
        InvoicePositionNumber = 1,
        Property = _property1,
        PropertyObject = _objectHouse,
        Value = _rentalContract1.Rent,
        Account = _account2,
        Invoice = _invoice1
      };

      //additionalCosts object 1
      var _invoicePosition4 = new InvoicePosition()
      {
        InvoicePositionNumber = 1,
        Property = _property1,
        PropertyObject = _objectHouse,
        Value = 320,
        AdditionalCostsCategory = EAdditionalCosts.Lift,
        Account = _account1,
        Invoice = _invoice2
      };

      var _invoicePosition5 = new InvoicePosition()
      {
        InvoicePositionNumber = 2,
        Property = _property1,
        PropertyObject = _objectHouse,
        Value = 50,
        AdditionalCostsCategory = EAdditionalCosts.Gas,
        Account = _account1,
        Invoice = _invoice2
      };

      var _invoicePosition6 = new InvoicePosition()
      {
        InvoicePositionNumber = 3,
        Property = _property1,
        PropertyObject = _objectHouse,
        Value = 25,
        AdditionalCostsCategory = EAdditionalCosts.Sewer,
        Account = _account1,
        Invoice = _invoice2
      };

      var _invoicePosition7 = new InvoicePosition()
      {
        InvoicePositionNumber = 4,
        Property = _property1,
        PropertyObject = _objectHouse,
        Value = 39.50,
        AdditionalCostsCategory = EAdditionalCosts.Gardening,
        Account = _account1,
        Invoice = _invoice2
      };

      _invoice1.InvoicePositions = new List<InvoicePosition>() { _invoicePosition3 };
      _invoice2.InvoicePositions = new List<InvoicePosition>() { _invoicePosition4, _invoicePosition5, _invoicePosition6, _invoicePosition7 };
      _invoice3.InvoicePositions = new List<InvoicePosition>() { _invoicePosition1, _invoicePosition2 };


      //var _user1 = new User() { FirstName = "Michael", LastName = "Neuhaus", Password = SecurePasswordHasher.Hash("1"), Email = "mi@app.de", Role = ERole.Admin };
      //var _user2 = new User() { FirstName = "Patrick", LastName = "Graber", Password = SecurePasswordHasher.Hash("2"), Email = "pg@app.de", Role = ERole.User };
      //var _user3 = new User() { FirstName = "Test", LastName = "User", Password = SecurePasswordHasher.Hash("3"), Email = "ts@app.de", Role = ERole.Viewer };

      var _user1 = new User() { FirstName = "Michael", LastName = "Neuhaus", Password = SecurePasswordHasher.Hash("1"), Email = "1", Role = ERole.Admin };
      var _user2 = new User() { FirstName = "Patrick", LastName = "Graber", Password = SecurePasswordHasher.Hash("2"), Email = "2", Role = ERole.User };
      var _user3 = new User() { FirstName = "Test", LastName = "User", Password = SecurePasswordHasher.Hash("3"), Email = "3", Role = ERole.Viewer };

      using var db = new ImmoGlobalContext();

      db.Properties.Add(_property1);
      db.Properties.Add(_property2);
      db.Personas.Add(_renter);
      db.Personas.Add(_creditor);
      db.Personas.Add(_housekeeper);
      db.Personas.Add(_housekeeper2);
      db.SaveChanges();
      db.PropertyObjects.Add(_objectHouse);
      db.PropertyObjects.Add(_objectGarage);
      db.PropertyObjects.Add(_objectRoom);
      db.PropertyObjects.Add(_objectOffice);
      db.RentalContracts.Add(_rentalContract1);
      db.RentalContracts.Add(_rentalContract2);
      db.RentalContracts.Add(_rentalContract3);
      db.Accounts.Add(_account1);
      db.Accounts.Add(_account2);
      db.Accounts.Add(_account3);
      db.PaymentRecords.Add(_income1);
      db.PaymentRecords.Add(_income2);
      db.PaymentRecords.Add(_income3);
      db.PaymentRecords.Add(_expense1);
      db.PaymentRecords.Add(_expense2);
      db.PaymentRecords.Add(_expense3);
      db.Invoices.Add(_invoice1);
      db.Invoices.Add(_invoice2);
      db.Invoices.Add(_invoice3);
      db.InvoicePositions.Add(_invoicePosition1);
      db.InvoicePositions.Add(_invoicePosition2);
      db.InvoicePositions.Add(_invoicePosition3);
      db.InvoicePositions.Add(_invoicePosition4);
      db.InvoicePositions.Add(_invoicePosition5);
      db.InvoicePositions.Add(_invoicePosition6);
      db.InvoicePositions.Add(_invoicePosition7);
      db.Users.Add(_user1);
      db.Users.Add(_user2);
      db.Users.Add(_user3);
      db.SaveChanges();
    }
  }
}
