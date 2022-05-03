using System.Windows;
using System.Windows.Controls;

namespace ImmoGlobal.Views
{
  /// <summary>
  /// Interaction logic for LoginView.xaml
  /// </summary>
  public partial class LoginView : UserControl
  {
    public LoginView()
    {
      InitializeComponent();
    }

    //This passes the password to the Property in Control. DO NOT BIND THIS!!
    private void PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (this.DataContext != null)
      {
        ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
      }
    } 
  }
}
