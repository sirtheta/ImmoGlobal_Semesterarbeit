using System;

namespace ImmoGlobal.Helpers
{
  internal static class Validator
  {
    /// <summary>
    /// checks if an email is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    internal static bool IsValidEmail(string email)
    {
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
