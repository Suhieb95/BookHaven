namespace BookHaven.Domain.Enums;
public enum TransactionType : byte
{
    Fine = 1,
    Sales = 2,
    Shipping = 3,
    Deposit = 4,
    Refund = 5,
    Payment = 6
}