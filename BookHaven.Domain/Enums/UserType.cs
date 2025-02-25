using System.ComponentModel;

namespace BookHaven.Domain.Enums;
public enum UserType : byte
{
    [Description("Customers")]
    Customer = 1,
    [Description("Users")]
    Internal = 2
}