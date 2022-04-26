using ImmoGlobal.Database;
using ImmoGlobal.MainClasses.Enum;
using ImmoGlobal.MainClasses;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ImmoGlobal.Commands;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Collections.ObjectModel;

namespace ImmoGlobal.ViewModels
{
  internal class UpsertInvoiceViewModel : BaseViewModel
  {
    public UpsertInvoiceViewModel()
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      DueDate = DateTime.Now.AddDays(30);
      PersonaCollection = new(DbController.GetAllPersonasDB());
      InvoiceDate = DateTime.Now;
      FormTitel = Application.Current.FindResource("createNewInvoice") as string ?? "create new invoioce";
    }

    public UpsertInvoiceViewModel(Invoice selectedInvoice)
    {
      BtnSave = new RelayCommand<object>(SaveClicked);
      SelectedInvoiceId = selectedInvoice.InvoiceId;
      
      SelectedPersona = selectedInvoice.GetPersonaToInvoice();
      DueDate = selectedInvoice.DueDate;
      InvoiceDate = selectedInvoice.InvoiceDate;
      DueDate = selectedInvoice.DueDate;
      InvoicePurpose = selectedInvoice.InvoicePurpose;
      InvoiceCategory = selectedInvoice.InvoiceCategory;
      InvoiceState = selectedInvoice.InvoiceState;

      FormTitel = (Application.Current.FindResource("invoice") as string ?? "invoice") + " " +
               (Application.Current.FindResource("edit") as string ?? "edit");
    }

    // sets the titel of the form
    public string FormTitel { get; set; }

    private Persona _persona;
    private Persona _selectedPersona;
    private ObservableCollection<Persona> _personaCollection;
    private DateTime _invoiceDate;
    private DateTime _dueDate;
    private string _invoicePurpose;
    private EInvoiceCategory _invoiceCategory;
    private EInvoiceState _invoiceState;
    
    public int? SelectedInvoiceId { get; set; }
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
    public Dictionary<EInvoiceState, string> EInvoiceStateWithCaptions { get; } = new Dictionary<EInvoiceState, string>
    {
      {EInvoiceState.NotReleased, Application.Current.FindResource("notReleased") as string ?? "not Released" },
      {EInvoiceState.Released,Application.Current.FindResource("released") as string ?? "Released" },
      {EInvoiceState.OverDue, Application.Current.FindResource("overDue") as string ?? "Over Due" },
      {EInvoiceState.Paid, Application.Current.FindResource("paid") as string ?? "Paid" },
      {EInvoiceState.Canceled, Application.Current.FindResource("canceled") as string ?? "Canceled" },
    };

    public ICommand BtnSave
    {
      get;
      private set;
    }

    private void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.FindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create RentalContract
      if (SelectedInvoiceId == null && CreateInvoice())
      {
        ShowNotification("Success", Application.Current.FindResource("successAddInvoice") as string ?? "Invoice added successfully", NotificationType.Success);
        ClearValues();
      }
      // Update Property
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
      if (DbController.UpsertInvoiceToDB(new Invoice()
      {
        Persona = SelectedPersona,
        InvoiceDate = InvoiceDate,
        DueDate = DueDate,
        InvoicePurpose = InvoicePurpose,
        InvoiceCategory = InvoiceCategory,
        InvoiceState = InvoiceState        
      }))
      {
        return true;
      }
      return false;
    }


    private bool UpdateInvoice(int invoiceId)
    {
      if (DbController.UpsertInvoiceToDB(new Invoice()
      {
        InvoiceId = invoiceId,
        Persona = SelectedPersona,
        InvoiceDate = InvoiceDate,
        DueDate = DueDate,
        InvoicePurpose = InvoicePurpose,
        InvoiceCategory = InvoiceCategory,
        InvoiceState = InvoiceState

      }))
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
    }
  }
}
