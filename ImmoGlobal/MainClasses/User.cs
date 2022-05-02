using ImmoGlobal.MainClasses.Enum;

namespace ImmoGlobal.MainClasses
{
  internal class User
  {
    /// <summary>
    /// model for the user. (Login)
    /// </summary>
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public ERole Role { get; set; }


    public string FullName
    {
      get { return FirstName + " " + LastName; }
    }
  }
}
