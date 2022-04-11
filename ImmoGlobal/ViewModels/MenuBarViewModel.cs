using ImmoGlobal.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ImmoGlobal.ViewModels
{
  internal class MenuBarViewModel : BaseViewModel
  {

    public MenuBarViewModel()
    {
      BtnProperty = new RelayCommand<object>(BtnPropertyClick);
      BtnPropertyObject = new RelayCommand<object>(BtnPropertyObjectClick);
      BtnRenter = new RelayCommand<object>(BtnRenterClick);
      BtnCreditor = new RelayCommand<object>(BtnCreditorsClick);
      BtnRentalContract = new RelayCommand<object>(BtnRentalContractClick);
      BtnInvoice = new RelayCommand<object>(BtnInvoiceClick);
      BtnAccount = new RelayCommand<object>(BtnAccountClick);


    }


    private void BtnPropertyClick(object obj)
    {
      throw new NotImplementedException();
    }
    private void BtnPropertyObjectClick(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnRenterClick(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnRentalContractClick(object obj)
    {
      throw new NotImplementedException();
    }

    private void BtnCreditorsClick(object obj)
    {
      throw new NotImplementedException();
    }
    private void BtnInvoiceClick(object obj)

    {
      throw new NotImplementedException();
    }

    private void BtnAccountClick(object obj)
    {
      throw new NotImplementedException();
    }

    public ICommand BtnProperty
    {
      get;
      private set;
    }

    public ICommand BtnPropertyObject
    {
      get;
      private set;
    }

    public ICommand BtnRenter
    {
      get;
      private set;
    }

    public ICommand BtnCreditor
    {
      get;
      private set;
    }

    public ICommand BtnRentalContract
    {
      get;
      private set;
    }

    public ICommand BtnInvoice
    {
      get;
      private set;
    }

    public ICommand BtnAccount
    {
      get;
      private set;
    }
    public Brush BtnPropertyColor { get; set; }
    public Brush BtnPropertyObjectColor { get ; set; }
    public Brush BtnRenterColor { get; set; }
    public Brush BtnCreditorColor { get; set; }
    public Brush BtnRentalContractColor { get; set; }
    public Brush BtnInvoiceColor { get; set; }
    public Brush BtnAccountColor { get; set; }
  }
}
