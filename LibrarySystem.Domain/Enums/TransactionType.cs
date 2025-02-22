namespace LibrarySystem.Domain.Enums;

public enum TransactionType : byte
{
    Fine = 1,
    Purchase = 2,
    Deposit = 3,
    Refund = 4,
    Payment = 5
}