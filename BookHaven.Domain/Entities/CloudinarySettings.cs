namespace BookHaven.Domain.Entities;

public class CloudinarySettings
{
    public const string SectionName = "Cloudinary";
    public string CloudName { get; set; } = default!;
    public string ApiSecret { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}
