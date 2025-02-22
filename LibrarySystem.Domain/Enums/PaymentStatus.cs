namespace LibrarySystem.Domain.Enums;

public enum PaymentStatus : byte
{
    Approved = 1,
    Processing = 2,
    Returned = 3,
    Pending = 4,
    Rejected = 5,
}