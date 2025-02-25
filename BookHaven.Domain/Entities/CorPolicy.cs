namespace BookHaven.Domain.Entities;
public class AnyCorsPolicy
{
    public const string SectionName = "AnyOriginCorsPolicy";
    public string PolicyName { get; init; } = null!;
}
public class SpecifiedOriginCorsPolicy
{
    public const string SectionName = "SpecifiedOriginCorsPolicy";
    public string ProductionPolicyName { get; init; } = null!;
    public string? ProductionURL { get; init; }
    public string LocalPolicyName { get; init; } = null!;
    public string? LocalURL { get; init; }

}