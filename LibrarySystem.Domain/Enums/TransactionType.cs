namespace LibrarySystem.Domain.Enums;

public enum TransactionType : byte
{
    Debit = 1,
    Credit = 2,
    Refund = 3,
    Adjustment = 4
}