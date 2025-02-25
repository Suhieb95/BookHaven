using System.ComponentModel;

namespace LibrarySystem.Domain.Enums;
public enum UserType : byte
{
    [Description("Customers")]
    Customer = 1,
    [Description("Users")]
    Internal = 2
}