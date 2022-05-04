using ImmoGlobal.Commands;
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
    /// <summary>
    /// mehtod to show a notification toast
    /// </summary>
    /// <param name="titel"></param>
    /// <param name="message"></param>
    /// <param name="type"></param>
    internal static void ShowNotification(string titel, string message, NotificationType type)
    {
      var notificationManager = new NotificationManager();
      _ = notificationManager.ShowAsync(new NotificationContent { Title = titel, Message = message, Type = type },
              areaName: "WindowArea", expirationTime: new TimeSpan(0, 0, 4));
    }

    /// <summary>
    /// method to show the material Design messagebox
    /// </summary>
    /// <param name="messageStr"></param>
    /// <param name="type"></param>
    /// <param name="buttons"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Method gets called when the user clicks on the "Save" button.
    /// </summary>
    /// <param name="obj"></param>
    internal virtual void SaveClicked(object obj) { }

    /// <summary>
    /// Method gets called when the user clicks on the "Delete" button.
    /// </summary>
    /// <param name="obj"></param>
    internal virtual void DeleteClicked(object obj) { }

    internal static MainWindowViewModel? MainWindowViewModelInstance { get => MainWindowViewModel.GetInstance; }

    // bool for edit button, gets disabled if the user has not enough rights
    private bool _canEdit;
    public bool CanEdit
    {
      get
      {
        if (MainWindowViewModelInstance.LogedInUser != null)
        {
          if (MainWindowViewModelInstance.LogedInUser.Role == ERole.User || MainWindowViewModelInstance.LogedInUser.Role == ERole.Admin)
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
      get => new RelayCommand<object>(SaveClicked);
    }

    public ICommand? BtnDelete
    {
      get => new RelayCommand<object>(DeleteClicked);
    }

    public Visibility BtnDeleteVisibility { get; set; }

    public string FormTitel { get; set; }
    internal int? Id { get; set; }
  }
}
