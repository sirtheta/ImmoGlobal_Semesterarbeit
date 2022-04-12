﻿using ImmoGlobal.MainClasses.Enum;

namespace ImmoGlobal.MainClasses
{
  internal class User
  {
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public ERole Role { get; set; }
  }
}
