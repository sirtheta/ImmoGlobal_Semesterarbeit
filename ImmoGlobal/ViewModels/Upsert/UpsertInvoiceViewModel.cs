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
      DueDate = DateTime.Now.AddDays(30);
      PersonaCollection = new(DbController.GetAllPersonasDB());
      InvoiceDate = DateTime.Now;

      // sets the titel of the form      
      FormTitel = Application.Current.TryFindResource("createNewInvoice") as string ?? "create new invoioce";
      InvoicePositionViewModelCollection = new();

      AddOneInvoicePosition();
    }

    /// <summary>
    /// c'tor to update an existing invoice
    /// </summary>
    /// <param name="selectedPersona"></param>
    internal UpsertInvoiceViewModel(Persona selectedPersona)
    {
      BtnAddOnePosition = new RelayCommand<object>(AddOneInvoicePosition);
      BtnRemoveOnePosition = new RelayCommand<object>(RemoveOneInvoicePosition);

      PersonaCollection = new(DbController.GetAllPersonasDB());
      SelectedPersona = selectedPersona;

      InvoicePositionViewModelCollection = new();

      InvoiceDate = DateTime.Now;
      DueDate = DateTime.Now.AddDays(30);

      // sets the titel of the form      
      FormTitel = Application.Current.TryFindResource("createNewInvoice") as string ?? "create new invoioce";
      InvoicePositionViewModelCollection = new();

      AddOneInvoicePosition();
    }


    /// <summary>
    /// C'tor for editing an existing invoice
    /// </summary>
    /// <param name="selectedInvoice"></param>
    /// <param name="invoicePositions"></param>
    internal UpsertInvoiceViewModel(Invoice selectedInvoice)
    {
      SelectedInvoice = selectedInvoice;

      PersonaCollection = new(DbController.GetAllPersonasDB());
      Id = selectedInvoice.InvoiceId;
      InvoicePositionViewModelCollection = new();

      SelectedPersona = selectedInvoice.GetPersonaToInvoice();
      DueDate = selectedInvoice.DueDate;
      InvoiceDate = selectedInvoice.InvoiceDate;
      DueDate = selectedInvoice.DueDate;
      InvoicePurpose = selectedInvoice.InvoicePurpose;
      InvoiceCategory = selectedInvoice.InvoiceCategory;
      InvoiceState = selectedInvoice.InvoiceState;

      var invoicePositions = selectedInvoice.GetInvoicePositonToInvoice();
      //add every invoice position to the collection
      foreach (var item in invoicePositions)
      {
        _invoicePositionNumber++;
        InvoicePositionViewModelCollection.Add(new InvoicePositionViewModel(item) { InvoicePositionNumber = _invoicePositionNumber });
      }

      EnableOrDisableFields();

      // sets the titel of the form
      FormTitel = (Application.Current.TryFindResource("invoice") as string ?? "invoice") + " " +
               (Application.Current.TryFindResource("edit") as string ?? "edit");
    }

    /// <summary>
    /// Enable or  disable fields for editing, depending on the invoice state
    /// </summary>
    private void EnableOrDisableFields()
    {
      // Enable only the Invoice state field if the invoice is not NotReleased
      if (SelectedInvoice.InvoiceState != EInvoiceState.NotReleased)
      {
        IsEnabled = false;
      }
      if (SelectedInvoice.InvoiceState == EInvoiceState.Paid)
      {
        StateIsEnabled = false;
      }

      if (!CanEdit)
      {
        IsEnabled = false;
        StateIsEnabled = false;
      }
    }

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
          {EInvoiceCategory.Property,Application.Current.TryFindResource("property") as string ?? "Property" },
          {EInvoiceCategory.Object, Application.Current.TryFindResource("propertyObject") as string ?? "Property Object" },
          {EInvoiceCategory.Rent,Application.Current.TryFindResource("rent") as string ?? "Rent" },
          {EInvoiceCategory.AdditionalCosts, Application.Current.TryFindResource("additionalCosts") as string ?? "Additional Costs" },
          {EInvoiceCategory.BillReminder, Application.Current.TryFindResource("billReminder") as string ?? "Bill Reminder" },
      {EInvoiceCategory.None, Application.Current.TryFindResource("none") as string ?? "none" },
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
            {EInvoiceState.NotReleased, Application.Current.TryFindResource("notReleased") as string ?? "not Released" },
            {EInvoiceState.Released,Application.Current.TryFindResource("released") as string ?? "Released" },
            {EInvoiceState.Paid, Application.Current.TryFindResource("paid") as string ?? "Paid" },
            {EInvoiceState.Canceled, Application.Current.TryFindResource("canceled") as string ?? "Canceled" },
          };
        }
        else
        {
          return new Dictionary<EInvoiceState, string>
          {
            {EInvoiceState.Released,Application.Current.TryFindResource("released") as string ?? "Released" },
            {EInvoiceState.Paid, Application.Current.TryFindResource("paid") as string ?? "Paid" },
            {EInvoiceState.Canceled, Application.Current.TryFindResource("canceled") as string ?? "Canceled" },
          };
        }
      }
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

    internal override void SaveClicked(object obj)
    {
      if (!NullFieldCheck())
      {
        ShowMessageBox(Application.Current.TryFindResource("errorFillAllFields") as string ?? "Please fill in all fields", MessageType.Error, MessageButtons.Ok);
        return;
      }

      //Create invoice
      if (Id == null && CreateInvoice())
      {
        ShowNotification("Success", Application.Current.TryFindResource("successAddInvoice") as string ?? "Invoice added successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      // Update invoice
      else if (Id != null && UpdateInvoice((int)Id))
      {
        ShowNotification("Success", Application.Current.TryFindResource("successUpdateInvoice") as string ?? "Invoice updated successfully", NotificationType.Success);
        MainWindowViewModelInstance.NavigateBack();
      }
      else
      {
        ShowMessageBox(Application.Current.TryFindResource("errorAddInvoice") as string ?? "Error adding invoice", MessageType.Error, MessageButtons.Ok);
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

      SelectedInvoice.InvoiceId = invoiceId;
      SelectedInvoice.Persona = SelectedPersona;
      SelectedInvoice.InvoiceDate = InvoiceDate;
      SelectedInvoice.DueDate = DueDate;
      SelectedInvoice.InvoiceCategory = InvoiceCategory;
      SelectedInvoice.InvoiceState = InvoiceState;

      if (DbController.UpsertInvoiceToDB(SelectedInvoice))
      {
        foreach (var item in InvoicePositionViewModelCollection)
        {
          if (item.SelectedInvoicePositionId != null)
          {
            var invoicePosition = item.InvoicePosition;

            invoicePosition.InvoicePositionId = (int)item.SelectedInvoicePositionId;
            invoicePosition.InvoicePositionNumber = item.InvoicePositionNumber;
            invoicePosition.Property = item.SelectedProperty;
            invoicePosition.PropertyObject = item.SelectedPropertyObject;
            invoicePosition.Invoice = SelectedInvoice;
            invoicePosition.Value = item.Value;
            invoicePosition.AdditionalCostsCategory = item.AdditionalCostsCategory;
            invoicePosition.Account = item.SelectedAccount;

            //write updated invoice position to db
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
  }
}
