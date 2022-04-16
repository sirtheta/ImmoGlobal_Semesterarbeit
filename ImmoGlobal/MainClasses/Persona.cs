﻿using ImmoGlobal.MainClasses.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ImmoGlobal.MainClasses
{
  internal class Persona
  {

    // Constructor for renter
    public Persona(string lastName,
                   string firstName,
                   long phone,
                   string email,
                   DateTime dateOfBirth,
                   string address,
                   int zip,
                   string city,
                   ECivilState eCivilState,
                   string addressBefore,
                   string accountNumber,
                   long mobile = 0,
                   long officePhone = 0)
    {
      FirstName = firstName;
      LastName = lastName;
      Phone = phone;
      Mobile = mobile;
      OfficePhone = officePhone;
      Email = email;
      DateOfBirth = dateOfBirth;
      Address = address;
      Zip = zip;
      City = city;
      CivilState = eCivilState;
      AddressBefore = addressBefore;
      AccountNumber = accountNumber;
      IsRenter = true;
    }

    // Constructor for creditor
    public Persona(bool creditorIsActive,
                   string creditorCompanyName,
                   string address,
                   int zip,
                   string city,
                   long phone,
                   string creditorContactPerson,
                   string lastName,
                   string firstName,
                   string email,
                   string vatNumber)
    {
      FirstName = firstName;
      LastName = lastName;
      Phone = phone;
      Email = email;
      Address = address;
      Zip = zip;
      City = city;
      CreditorIsActive = creditorIsActive;
      VatNumber = vatNumber;
      CreditorCompanyName = creditorCompanyName;
      CreditorContactPerson = creditorContactPerson;
      IsCreditor = true;
    }

    // Constructor for Housekeeper
    public Persona(string lastName,
                   string firstName,
                   long phone,
                   string email,
                   string address,
                   int zip,
                   string city,
                   string accountNumber,
                   long mobile = 0)
    {
      FirstName = firstName;
      LastName = lastName;
      Phone = phone;
      Mobile = mobile;
      Email = email;
      Address = address;
      Zip = zip;
      City = city;
      AccountNumber = accountNumber;
    }

    [Key]
    public int PersonaId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long Phone { get; set; }
    public long Mobile { get; set; }
    public long OfficePhone { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Address { get; set; }
    public int Zip { get; set; }
    public string City { get; set; }
    public ECivilState CivilState { get; set; }
    public string? AddressBefore { get; set; }
    public string? AccountNumber { get; set; }
    public string? CreditorContactPerson { get; set; }
    public bool CreditorIsActive { get; set; }
    public bool IsCreditor { get; set; }
    public bool IsRenter { get; set; }
    public string? VatNumber { get; set; }
    public string? CreditorCompanyName { get; set; }
    
    public string FullName => $"{LastName} {FirstName}";
    public string FullNameAndCity => $"{LastName} {FirstName}, {City}";
    public string CivilStateString
    {
      get
      {
        return CivilState switch
        {
          ECivilState.Single => Application.Current.FindResource("single") as string ?? "Single",
          ECivilState.Married => Application.Current.FindResource("married") as string ?? "Married",
          ECivilState.Divorced => Application.Current.FindResource("divorced") as string ?? "Divorced",
          ECivilState.Widowed => Application.Current.FindResource("widowed") as string ?? "Widowed",
          _ => "unknown",
        };
      }
    } 
  }
}
