namespace BookHaven.Domain.Enums;

public enum TransactionStatus : byte
{
    Approved = 1,
    Processing = 2,
    Returned = 3,
    Pending = 4,
    Rejected = 5,
}