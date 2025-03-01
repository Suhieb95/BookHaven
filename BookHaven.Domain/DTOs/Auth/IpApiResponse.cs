namespace BookHaven.Domain.DTOs.Auth;

public record IPApiResponse(string? Status, string? Continent, string? Country, string? RegionName, string? City, string? District,
             string? Zip, double? Lat, double? Lon, string? Isp, string? Query);