using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;

namespace ImmoGlobal.Database
{
  internal static class DatabaseSeeder
  {

    public static void CreateTestEntries()
    {

      var _renter = new Persona("Max", "Musterman", 0334328978, "mail@mail.de", DateTime.Parse("25.10.1988"), "Barstreet 5", 3612, "Steffisburg", ECivilState.Married, "Thunstr. 15, 3600 Bern", "12ET34UI56789", 0791234567);
      var _creditor = new Persona(true, "Creditors Company Name", "Creditors Company Address", 3800, "Interlaken", 0797891236, "Creditor ContactPerson", "CreditorName", "CreditorSurname", "mail@test.de", "CHE-123.456.789");
      var _housekeeper = new Persona("Moritz", "Houskeeperman", 0238905678, "moritz@mail.de", "Obsolet Adress 4", 2344, "Ronmalingen", "893489uip099322", 8907894561);

      var _property1 = new Property() { Housekeeper = _housekeeper, Description = "TestLiegenschaft1", Adress = "TestAdress1", ZipCode = 3612, City = "TestCity1", PropertyInsurance = "Helvetia1", PersonInsurance = "Mobiliar1", LiabilityInsurance = "Emmitaler1" };
      var _property2 = new Property() { Housekeeper = _housekeeper, Description = "TestLiegenschaft2", Adress = "TestAdress2", ZipCode = 3613, City = "TestCity2", PropertyInsurance = "Helvetia2", PersonInsurance = "Mobiliar2", LiabilityInsurance = "Emmitaler2" };

      var _objectHouse = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.House, Description = "Wohnung 1", Location = "TestLocation", NumberOfRooms = 4.5, Area = 100, NumberOfKeys = 5, Fridge = true, Dishwasher = true, Stove = true, Oven = true };
      var _objectGarage = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.Garage, Description = "Garage von Wohnung 1", Location = "irgendwo nebenan", Area = 15 };
      var _objectRoom = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.Room, Description = "Partyraum", Location = "Sous partere", NumberOfRooms = 1, Area = 10, NumberOfKeys = 2 };
      var _objectOffice = new PropertyObject() { Property = _property1, ObjectType = EPropertyObjectType.Office, Description = "Office in " + _property1.Description, Location = "4 Stock", NumberOfRooms = 2, Area = 35, NumberOfKeys = 3, Fridge = true, Dishwasher = true };

      var _rentalContract1 = new RentalContract() { Renter = _renter, PropertyObject = _objectHouse, RentStartDate = DateTime.Parse("01.04.2022"), RentEndDate = DateTime.Parse("31.10.2030"), Rent = 1650, Deposit = true, ContractState = EContractState.Active };
      var _rentalContract2 = new RentalContract() { Renter = _renter, PropertyObject = _objectHouse, RentStartDate = DateTime.Parse("01.11.2030"), Rent = 1850, Deposit = true, ContractState = EContractState.Singend };
      var _rentalContract3 = new RentalContract() { Renter = _renter, PropertyObject = _objectHouse, RentStartDate = DateTime.Parse("01.04.2018"), RentEndDate = DateTime.Parse("31.03.2022"), Rent = 1450, Deposit = true, ContractState = EContractState.Canceled };

      var _account1 = new Account() { AccountNumber = "5634RE56034", Balance = 500.4, AccountDescription = "Konto 1" };
      var _account2 = new Account() { AccountNumber = "23VFYYXX034", Balance = 54566.49, AccountDescription = "Konto 2" };
      var _account3 = new Account() { AccountNumber = "DSDD2334445", Balance = 554879.89, AccountDescription = "Konto 3" };

      //additionalCosts prop 1
      var _invoicePosition1 = new InvoicePosition() { Property = _property1, Value = 1320, AdditionalCostsCategory = EAdditionalCosts.Electricity };
      var _invoicePosition2 = new InvoicePosition() { Property = _property1, Value = 45.23, AdditionalCostsCategory = EAdditionalCosts.Gas };
      //rent
      var _invoicePosition3 = new InvoicePosition() { PropertyObject = _objectHouse, Value = _rentalContract1.Rent };
      //additionalCosts object 1
      var _invoicePosition4 = new InvoicePosition() { PropertyObject = _objectHouse, Value = 320, AdditionalCostsCategory = EAdditionalCosts.Lift };
      var _invoicePosition5 = new InvoicePosition() { PropertyObject = _objectHouse, Value = 50, AdditionalCostsCategory = EAdditionalCosts.Gas };
      var _invoicePosition6 = new InvoicePosition() { PropertyObject = _objectHouse, Value = 25, AdditionalCostsCategory = EAdditionalCosts.Sewer };
      var _invoicePosition7 = new InvoicePosition() { PropertyObject = _objectHouse, Value = 39.50, AdditionalCostsCategory = EAdditionalCosts.Gardening };

      var _invoice1 = new Invoice() { Persona = _renter, InvoiceDate = DateTime.Parse("01.03.2022"), DueDate = DateTime.Parse("01.04.2022"), InvoiceState = EInvoiceState.OverDue, InvoiceCategory = EInvoiceCategory.Rent, TotalValue = _invoicePosition3.Value, InvoicePositions = new List<InvoicePosition>() { _invoicePosition3 } };
      var _invoice2 = new Invoice() { Persona = _renter, InvoiceDate = DateTime.Parse("05.01.2022"), DueDate = DateTime.Parse("28.02.2022"), InvoiceState = EInvoiceState.Paid, InvoiceCategory = EInvoiceCategory.AdditionalCosts, TotalValue = _invoicePosition4.Value + _invoicePosition5.Value + _invoicePosition6.Value + _invoicePosition7.Value, InvoicePositions = new List<InvoicePosition>() { _invoicePosition4, _invoicePosition5, _invoicePosition6, _invoicePosition7 } };
      var _invoice3 = new Invoice() { Persona = _creditor, InvoiceDate = DateTime.Parse("15.04.2022"), DueDate = DateTime.Parse("15.05.2022"), InvoiceState = EInvoiceState.Released, InvoiceCategory = EInvoiceCategory.Property, TotalValue = _invoicePosition1.Value + _invoicePosition2.Value, InvoicePositions = new List<InvoicePosition>() { _invoicePosition1, _invoicePosition2 } };

      var _user1 = new User() { Name = "Admin", Surname = "Tester", Email = "user1@immoglogbal.ch", Password = "password", Role = ERole.Admin };
      var _user2 = new User() { Name = "User", Surname = "Tester", Email = "user2@immoglogbal.ch", Password = "password", Role = ERole.User };

      using var db = new ImmoGlobalContext();

      db.Properties.Add(_property1);
      db.Properties.Add(_property2);
      db.Personas.Add(_renter);
      db.Personas.Add(_creditor);
      db.Personas.Add(_housekeeper);
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
      db.InvoicePositions.Add(_invoicePosition1);
      db.InvoicePositions.Add(_invoicePosition2);
      db.InvoicePositions.Add(_invoicePosition3);
      db.InvoicePositions.Add(_invoicePosition4);
      db.InvoicePositions.Add(_invoicePosition5);
      db.InvoicePositions.Add(_invoicePosition6);
      db.InvoicePositions.Add(_invoicePosition7);
      db.Invoices.Add(_invoice1);
      db.Invoices.Add(_invoice2);
      db.Invoices.Add(_invoice3);
      db.Users.Add(_user1);
      db.Users.Add(_user2);

      db.SaveChanges();
    }
  }
}
