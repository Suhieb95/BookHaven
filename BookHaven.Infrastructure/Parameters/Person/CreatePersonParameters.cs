namespace BookHaven.Infrastructure.Parameters.Person;
internal class CreatePersonParameters
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
    public required bool IsActive { get; init; }
    public required bool IsVerified { get; init; }
}
