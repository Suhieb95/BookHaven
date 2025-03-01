namespace BookHaven.Domain.Entities;
public class AllowedAgent
{
    public const string SectionName = "AllowedUserAgents";
    public required List<string> Agents { get; init; }
}