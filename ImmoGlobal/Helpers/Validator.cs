using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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

    private static readonly Regex _regex = new("[^0-9.]+"); //regex that matches disallowed text
    internal static bool IsTextAllowed(string text)
    {
      return !_regex.IsMatch(text);
    }
  }
}
