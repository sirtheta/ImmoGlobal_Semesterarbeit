using System.Windows.Input;
using ImmoGlobal.Commands;

namespace ImmoGlobal.ViewModels
{
  internal class MainViewModel : BaseViewModel
  {
    private BaseViewModel _selectedViewModel;
    
    private static MainViewModel? instance = null;

    /// <summary>
    /// returns instance of class MainViewModel
    /// </summary>
    public static MainViewModel? GetInstance {
      get {
        return instance; 
      }
    }

    public MainViewModel(BaseViewModel viewModel)
    {
      _selectedViewModel = viewModel;
      instance = this;
    }

    public BaseViewModel SelectedViewModel {
      get {
        return _selectedViewModel;
      }
      set {
        _selectedViewModel = value;
        OnPropertyChanged(nameof(SelectedViewModel));
      }
    }
  }
}
