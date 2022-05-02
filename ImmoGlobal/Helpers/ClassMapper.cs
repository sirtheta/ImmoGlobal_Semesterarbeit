using System;
using System.Linq;

namespace ImmoGlobal.Helpers
{
  internal static class ClassMapper
  {
    /// <summary>
    /// Maps one instance of a classe into another instance of the same class
    /// Only the properties wich hase get AND set are mapped
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="source"></param>
    public static void CopyValues<T>(T target, T source)
    {
      Type t = typeof(T);

      var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

      foreach (var prop in properties)
      {
        var value = prop.GetValue(source, null);
        if (value != null)
          prop.SetValue(target, value, null);
      }
    }
  }
}
