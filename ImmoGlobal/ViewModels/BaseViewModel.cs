using ImmoGlobal.MainClasses.Enum;
using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace ImmoGlobal.ViewModels
{
  internal abstract class BaseViewModel : DependencyObject, INotifyPropertyChanged
  {
    internal static void ShowNotification(string titel, string message, NotificationType type)
    {
      var notificationManager = new NotificationManager();
      _ = notificationManager.ShowAsync(new NotificationContent { Title = titel, Message = message, Type = type },
              areaName: "WindowArea", expirationTime: new TimeSpan(0, 0, 4));
    }

    internal static bool ShowMessageBox(string messageStr, MessageType type, MessageButtons buttons)
    {
#pragma warning disable CS8629 // Nullable value type may be null.
      return (bool)new MaterialDesignMessageBox(messageStr, type, buttons).ShowDialog();
#pragma warning restore CS8629 // Nullable value type may be null.
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Converts a secure string to a string using safe methods.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    internal protected static string? ToNormalString(SecureString input)
    {
      IntPtr strptr = IntPtr.Zero;
      try
      {
        strptr = Marshal.SecureStringToBSTR(input);
        string normal = Marshal.PtrToStringBSTR(strptr);
        return normal;
      }
      catch
      {
        return null;
      }
      finally
      {
        //Free the pointer holding the SecureString
        Marshal.ZeroFreeBSTR(strptr);
      }
    }

    private bool _canEdit;
    public bool CanEdit
    {
      get
      {
        var instance = MainWindowViewModel.GetInstance;
        if (instance != null)
        {
          if (instance.LogedInUserRole == ERole.User || instance.LogedInUserRole == ERole.Admin)
          {
            _canEdit = true;
          }
          else
          {
            _canEdit = false;
          }
        }
        return _canEdit;
      }
      set
      {
        _canEdit = value;
        OnPropertyChanged();
      }
    }

    public ICommand BtnSave
    {
      get;
      internal set;
    }

    public ICommand? BtnDelete
    {
      get;
      internal set;
    }

    public Visibility BtnDeleteVisibility { get; set; }
  }
}
