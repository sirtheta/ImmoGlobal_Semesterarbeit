using System;
using System.Globalization;
using System.Windows.Data;

namespace ImmoGlobal.Helpers
{
  /// <summary>
  /// Converts a int value from xaml minus one and sends it back. 
  /// needed for ComboBoxes to work properly for selected item
  /// </summary>
  public class MinusOneConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return ((int)value) - 1;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return null;
      //throw new NotSupportedException("Cannot convert back");
    }
  }
}
