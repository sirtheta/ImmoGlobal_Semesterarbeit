using System;

namespace ImmoGlobal.Helpers
{
  internal static class Validator
  {
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
