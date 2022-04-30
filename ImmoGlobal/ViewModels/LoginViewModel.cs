using ImmoGlobal.Database;
using ImmoGlobal.MainClasses;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System.Security;
using System.Windows;

namespace ImmoGlobal.ViewModels
{
  internal class LoginViewModel : BaseViewModel
  {
    internal LoginViewModel()
    {
      //set the title of the form
      FormTitel = "Login";
    }

    private string _email;
    public string Email
    {
      get => _email;
      set
      {
        _email = value;
        OnPropertyChanged();
      }
    }

    internal SecureString Password { get; set; }

    /// <summary>
    /// Command to login, overrides the implemented SaveCklick command from BaseViewModel
    /// </summary>
    /// <param name="obj"></param>
    internal override void SaveClicked(object obj)
    {
      if (VerifyUser())
      {
        ShowNotification("Success", Application.Current.FindResource("loginSuccessfull") as string ?? "Login successfull", NotificationType.Success);
      }
      else
      {
        ShowMessageBox(Application.Current.FindResource("loginFailed") as string ?? "Login failed", MessageType.Error, MessageButtons.Ok);
      }
    }

    /// <summary>
    /// Verifies the user by calling the email and password from the database
    /// the password is then checked with the password hasher
    /// </summary>
    /// <returns></returns>
    internal bool VerifyUser()
    {
      var user = DbController.GetUserFromDb(Email);
      if (user != null)
      {
        if (Password != null && SecurePasswordHasher.Verify(ToNormalString(Password), user.Password))
        {
          Verified(user);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// this is called when the user is successfull verified
    /// </summary>
    /// <param name="user"></param>
    internal void Verified(User user)
    {
      var instance = MainWindowViewModelInstance;
      if (instance != null)
      {
        instance.MenuBarViewModel.LogedInUserFullName = user.FullName;
        instance.LogedInUserRole = user.Role;
        instance.SelectedViewModel = new PropertyOverviewViewModel();
        instance.MenuBarViewModel.IsEnabled = true;

        if (CanEdit)
        {
          instance.SideMenuViewModel.BtnHousekeeperText = Application.Current.FindResource("btnHousekeeperExtended") as string ?? "edit";
        }
        else
        {
          instance.SideMenuViewModel.BtnHousekeeperText = Application.Current.FindResource("btnHousekeeper") as string ?? "edit";
        }
      }
    }
  }
}
