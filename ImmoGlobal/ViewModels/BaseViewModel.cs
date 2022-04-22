using MaterialDesignMessageBoxSirTheta;
using Notifications.Wpf.Core;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

  }
}
