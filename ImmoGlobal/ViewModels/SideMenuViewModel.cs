using ImmoGlobal.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal class SideMenuViewModel : BaseViewModel
  {

    public SideMenuViewModel()
    {
      BtnNewProperty = new RelayCommand<object>(BtnNewPropertyClicked);
    }
    public ICommand BtnNewProperty
    {
      get;
      private set;
    }

    private void BtnNewPropertyClicked(object obj)
    {
      throw new NotImplementedException();
    }
  }
}
