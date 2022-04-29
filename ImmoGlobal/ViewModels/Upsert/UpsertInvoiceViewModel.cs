using ImmoGlobal.Commands;
using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertInvoiceViewModel : BaseViewModel
  {
    /// <summary>
    /// c'tor to create a new invoice
    /// </summary>
    internal UpsertInvoiceViewModel()
    {
      BtnAddOnePosition = new RelayCommand<object>(AddOneInvoicePosition);
      BtnRemoveOnePosition = new RelayCommand<object>(RemoveOneInvoicePosition);
      BtnSave = new RelayCommand<object>(SaveClicked);
      DueDate = DateTime.Now.AddDays(30);
      PersonaCollection = new(DbController.GetAllPersonasDB());
      InvoiceDate = DateTime.Now;

      // sets the titel of the form      
      FormTitel = Application.Current.FindResource("createNewInvoice") as string ?? "create new invoioce";
      InvoicePositionViewModelCollection = new();

      AddOneInvoicePosition();
    }


    /// <summary>
    /// C'tor for editing an existing invoice
    /// </summary>
    /// <param name="selectedInvoice"></param>
    /// <param name="invoicePositions"></param>
    internal UpsertInvoiceViewModel(Invoice selectedInvoice, ICollection<InvoicePosition> invoicePositions)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);

      // Enable only the Invoice state field if the invoice is not NotReleased
      if (selectedInvoice.InvoiceState != EInvoiceState.NotReleased)
      {
        IsEnabled = false;
      }
      if (selectedInvoice.InvoiceState == EInvoiceState.Paid)
      {
        StateIsEnabled = false;
      }

      PersonaCollection = new(DbController.GetAllPersonasDB());
      SelectedInvoiceId = selectedInvoice.InvoiceId;
      InvoicePositionViewModelCollection = new();

      SelectedPersona = selectedInvoice.GetPersonaToInvoice();
      DueDate = selectedInvoice.DueDate;
      InvoiceDate = selectedInvoice.InvoiceDate;
      DueDate = selectedInvoice.DueDate;
      InvoicePurpose = selectedInvoice.InvoicePurpose;
      InvoiceCategory = selectedInvoice.InvoiceCategory;
      InvoiceState = selectedInvoice.InvoiceState;

      //add every invoice position to the collection
      foreach (var item in invoicePositions)
      {
        _invoicePositionNumber++;
        InvoicePositionViewModelCollection.Add(new InvoicePositionViewModel(item) { InvoicePositionNumber = _invoicePositionNumber });
      }

      // sets the titel of the form
      FormTitel = (Application.Current.FindResource("invoice") as string ?? "invoice") + " " +
               (Application.Current.FindResource("edit") as string ?? "edit");
    }

    public string FormTitel { get; set; }

    private Persona _persona;
    private Persona _selectedPersona;
    private ObservableCollection<Persona> _personaCollection;
    private ObservableCollection<InvoicePositionViewModel> _invoicePositionViewModelCollection;
    private DateTime _invoiceDate;
    private DateTime _dueDate;
    private string _invoicePurpose;
    private EInvoiceCategory _invoiceCategory;
    private EInvoiceState _invoiceState;
    private int _invoicePositionNumber;

    public int? SelectedInvoiceId { get; set; }
    public bool IsEnabled { get; set; } = true;
    public bool StateIsEnabled { get; set; } = true;
    public ObservableCollection<InvoicePositionViewModel> InvoicePositionViewModelCollection
    {
      get => _invoicePositionViewModelCollection;
      set
      {
        _invoicePositionViewModelCollection = value;
        OnPropertyChanged();
      }
    }

    public Persona? Persona
    {
      get => _persona;
      set
      {
        _persona = value;
        OnPropertyChanged();
      }
    }
    public Persona? SelectedPersona
    {
      get => _selectedPersona;
      set
      {
        _selectedPersona = value;
        OnPropertyChanged();
      }
    }
    public ObservableCollection<Persona> PersonaCollection
    {
      get => _personaCollection;
      set
      {
        _personaCollection = value;
        OnPropertyChanged();
      }
    }
    public DateTime InvoiceDate
    {
      get => _invoiceDate;
      set
      {
        _invoiceDate = value;
        OnPropertyChanged();
      }
    }

    public DateTime DueDate
    {
      get => _dueDate;
      set
      {
        _dueDate = value;
        OnPropertyChanged();
      }
    }

    public string InvoicePurpose
    {
      get => _invoicePurpose;
      set
      {
        _invoicePurpose = value;
        OnPropertyChanged();
      }
    }

    public EInvoiceCategory InvoiceCategory
    {
      get => _invoiceCategory;
      set
      {
        _invoiceCategory = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// converts the invoice category enum to a string related to the enum 
    /// </summary>
    public Dictionary<EInvoiceCategory, string> EInvoiceCategoryWithCaptions { get; } = new Dictionary<EInvoiceCategory, string>
    {
          {EInvoiceCategory.Property,Application.Current.FindResource("property") as string ?? "Property" },
          {EInvoiceCategory.Object, Application.Current.FindResource("propertyObject") as string ?? "Property Object" },
          {EInvoiceCategory.Rent,Application.Current.FindResource("rent") as string ?? "Rent" },
          {EInvoiceCategory.AdditionalCosts, Application.Current.FindResource("additionalCosts") as string ?? "Additional Costs" },
          {EInvoiceCategory.BillReminder, Application.Current.FindResource("billReminder") as string ?? "Bill Reminder" },
      {EInvoiceCategory.None, Application.Current.FindResource("none") as string ?? "none" },
    };

    public EInvoiceState InvoiceState
    {
      get => _invoiceState;
      set
      {
        _invoiceState = value;
        OnPropertyChanged();
      }
    }
    /// <summary>
    /// converts the invoice state enum to a string related to the enum
    /// if the invoice is != NotReleased, the user can only select all other states
    /// if the invoice state is Paid, the user cant select anything, see property StatIsEnabled
    /// </summary>
    public Dictionary<EInvoiceState, string> EInvoiceStateWithCaptions
    {
      get
      {
        if (IsEnabled)
        {
          return new Dictionary<EInvoiceState, string>
          {
            {EInvoiceState.NotReleased, Application.Current.FindResource("notReleased") as string ?? "not Released" },
            {EInvoiceState.Released,Application.Current.FindResource("released") as string ?? "Released" },
            {EInvoiceState.OverDue, Application.Current.FindResource("overDue") as string ?? "Over Due" },
            {EInvoiceState.Paid, Application.Current.FindResource("paid") as string ?? "Paid" },
            {EInvoiceState.Canceled, Application.Current.FindResource("canceled") as string ?? "Canceled" },
          };
        }
        else
        {
          return new Dictionary<EInvoiceState, string>
          {
            {EInvoiceState.Released,Application.Current.FindResource("released") as string ?? "Released" },
            {EInvoiceState.OverDue, Application.Current.FindResource("overDue") as string ?? "Over Due" },
            {EInvoiceState.Paid, Application.Current.FindResource("paid") as string ?? "Paid" },
            {EInvoiceState.Canceled, Application.Current.FindResource("canceled") as string ?? "Canceled" },
          };
        }
      }
    }
    public ICommand BtnSave
    {
      get;
      private set;
    }

    public ICommand BtnAddOnePosition
    {
      get;
      private set;
    }

    public ICommand BtnRemoveOnePosition
    {
      get;
      private set;
    }

    /// <summary>
    /// Add one invoice position to the invoice
    /// </summary>
    /// <param name="parameter"></param>
    private void AddOneInvoicePosition(object? parameter = null)
    {
      _invoicePositionNumber++;
      InvoicePositionViewModelCollection.Add(new InvoicePositionViewModel() { InvoicePositionNumber = _invoicePositionNumber });
    }

    /// <summary>
    /// Remove one invoice position from the invoice
    /// </summary>
    /// <param name="obj"></param>
    private void RemoveOneInvoicePosition(object obj)
    {
      if (InvoicePositionViewModelCollection.Count > 1)
      {
        _invoicePositionNumber--;
        InvoicePositionViewModelCollection.RemoveAt(InvoicePositionViewModelCollection.Count - 1);
      }
    }

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create invoice
      if (SelectedInvoiceId == null && CreateInvoice())
      {
        ShowNotification("Success", Application.Current.FindResource("successAddInvoice") as string ?? "Invoice added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update invoice
      else if (SelectedInvoiceId != null && UpdateInvoice((int)SelectedInvoiceId))
      {
        ShowNotification("Success", Application.Current.FindResource("successUpdateInvoice") as string ?? "Invoice updated successfully", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("errorAddInvoice") as string ?? "Error adding invoice", MessageType.Error, MessageButtons.Ok);
      }
    }

    private bool NullFieldCheck()
    {
      if (SelectedPersona != null &&
        InvoicePurpose != null &&
        InvoiceCategory !=
        EInvoiceCategory.None)
      {
        return true;
      }
      return false;
    }


    private bool CreateInvoice()
    {
      var success = true;
      var invoice = new Invoice()
      {
        Persona = SelectedPersona,
        InvoiceDate = InvoiceDate,
        DueDate = DueDate,
        InvoicePurpose = InvoicePurpose,
        InvoiceCategory = InvoiceCategory,
        InvoiceState = InvoiceState
      };
      if (DbController.UpsertInvoiceToDB(invoice))
      {
        foreach (var item in InvoicePositionViewModelCollection)
        {
          var invoicePosition = new InvoicePosition()
          {
            InvoicePositionNumber = item.InvoicePositionNumber,
            Property = item.SelectedProperty,
            PropertyObject = item.SelectedPropertyObject,
            Invoice = invoice,
            Value = item.Value,
            AdditionalCostsCategory = item.AdditionalCostsCategory,
            Account = item.SelectedAccount
          };
          if (!DbController.UpsertInvoicePositionToDB(invoicePosition))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      if (success)
      {
        return true;
      }
      return false;
    }


    private bool UpdateInvoice(int invoiceId)
    {
      var success = true;
      var invoice = new Invoice()
      {
        InvoiceId = invoiceId,
        Persona = SelectedPersona,
        InvoiceDate = InvoiceDate,
        DueDate = DueDate,
        InvoicePurpose = InvoicePurpose,
        InvoiceCategory = InvoiceCategory,
        InvoiceState = InvoiceState
      };
      if (DbController.UpsertInvoiceToDB(invoice))
      {
        foreach (var item in InvoicePositionViewModelCollection)
        {
          if (item.SelectedInvoicePositionId != null)
          {
            var invoicePosition = new InvoicePosition()
            {
              InvoicePositionId = (int)item.SelectedInvoicePositionId,
              InvoicePositionNumber = item.InvoicePositionNumber,
              Property = item.SelectedProperty,
              PropertyObject = item.SelectedPropertyObject,
              Invoice = invoice,
              Value = item.Value,
              AdditionalCostsCategory = item.AdditionalCostsCategory,
              Account = item.SelectedAccount
            };
            if (!DbController.UpsertInvoicePositionToDB(invoicePosition))
            {
              success = false;
            }

          }
        }
      }
      else
      {
        success = false;
      }
      if (success)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Sets all properties to null
    /// </summary>
    private void ClearValues()
    {
      Persona = null;
      InvoiceDate = DateTime.Now;
      DueDate = DateTime.Now;
      InvoicePurpose = string.Empty;
      InvoiceCategory = EInvoiceCategory.None;
      InvoiceState = EInvoiceState.NotReleased;
      InvoicePositionViewModelCollection.Clear();
      AddOneInvoicePosition();
    }
  }
}
