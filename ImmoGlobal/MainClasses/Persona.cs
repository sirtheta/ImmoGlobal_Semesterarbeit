﻿using ImmoGlobal.MainClasses.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoGlobal.MainClasses
{
  internal class Persona
  {

    // Constructor for renter
    public Persona(string name, 
                   string surname, 
                   long phone,
                   string email, 
                   DateTime dateOfBirth, 
                   string address, 
                   int zip, 
                   string city, 
                   ECivilState eCivilState,
                   string adressBefore, 
                   string accountNumber, 
                   long mobile = 0, 
                   long officePhone = 0)
    {
      Name = name;
      Surname = surname;
      Phone = phone;
      Mobile = mobile;
      OfficePhone = officePhone;
      Email = email;
      DateOfBirth = dateOfBirth;
      Address = address;
      Zip = zip;
      City = city;
      CivilState = eCivilState;
      AdressBefore = adressBefore;
      AccountNumber = accountNumber;
    }

    // Constructor for creditor
    public Persona(bool creditorIsActive,
                   string creditorCompanyName,
                   string address,
                   int zip,
                   string city,
                   long phone,
                   string creditorContactPerson,
                   string name,
                   string surname,
                   string email,
                   string vatNumber)
    {
      Name = name;
      Surname = surname;
      Phone = phone;
      Email = email;
      Address = address;
      Zip = zip;
      City = city;
      CreditorIsActive = creditorIsActive;
      VatNumber = vatNumber;
      CreditorCompanyName = creditorCompanyName;
      CreditorContactPerson = creditorContactPerson;
    }

    // Constructor for Housekeeper
    public Persona(string name,
                   string surname, 
                   long phone,
                   string email, 
                   string address,
                   int zip,
                   string city,
                   string accountNumber,
                   long mobile = 0)
    {
      Name = name;
      Surname = surname;
      Phone = phone;
      Mobile = mobile;
      Email = email;
      Address = address;
      Zip = zip;
      City = city;
      AccountNumber = accountNumber;
    }

    public int PersonaId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public long Phone { get; set; }
    public long Mobile { get; set; }
    public long OfficePhone { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Address { get; set; }
    public int Zip { get; set; }
    public string City { get; set; }
    public ECivilState CivilState { get; set; }
    public string? AdressBefore { get; set; }
    public string? AccountNumber { get; set; }
    public string? CreditorContactPerson { get; set; }
    public bool CreditorIsActive { get; set; }
    public string? VatNumber { get; set; }
    public string? CreditorCompanyName { get; set; }
  }
}
