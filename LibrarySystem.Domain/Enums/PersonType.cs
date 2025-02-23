using System.ComponentModel;

namespace LibrarySystem.Domain.Enums;
public enum PersonType : byte
{
    [Description("Customers")]
    Customer = 1,
    [Description("Users")]
    InternalUser = 2
}