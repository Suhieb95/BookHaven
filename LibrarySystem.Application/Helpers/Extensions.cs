using System.ComponentModel;
using System.Reflection;
using System.Text;
using LibrarySystem.Domain.BaseModels;
using Microsoft.IdentityModel.Tokens;

namespace LibrarySystem.Application.Helpers;
public static class Extensions
{
    public static string GetEnumValue(this Enum e)
    {
        var attribute =
              e.GetType()?
                .GetTypeInfo()?
                .GetMember(e.ToString()!)?
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field!)?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault()
                as DescriptionAttribute;

        return attribute?.Description ?? e.ToString();
    }
    public static int GenerateRandom(int rangeFrom, int rangeTo)
    {
        Random rnd = new();
        return rnd.Next(rangeFrom, rangeTo + 1);
    }
    public static string GenerateImageURL(string format, string? folderName = null)
    {
        string folder = !string.IsNullOrEmpty(folderName) ? $"{folderName}" : "";
        return string.Concat(folder, Guid.NewGuid().ToString().Replace("-", "").AsSpan(10), $".{format[(format.IndexOf("/") + 1)..]}");
    }
    public static void EnsureHasTAttribute<TAttribute>(this MemberInfo classType, bool inherit = false)
    where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull($"{classType.Name} cannot be null.");
        var attr = classType.GetCustomAttributes(typeof(TAttribute), inherit).FirstOrDefault() as TAttribute ?? throw new InvalidOperationException($"Class {classType.Name} must have attribute of type {typeof(TAttribute).Name} applied.");
    }
    public static TValue? GetAttributeValue<TType, TAttribute, TValue>(this TType classType, Func<TAttribute, TValue> valueSelector, bool inherit = false)
    where TAttribute : Attribute where TType : MemberInfo // Type 
    {
        classType.EnsureHasTAttribute<TAttribute>(inherit);

        var attr = classType.GetCustomAttributes(typeof(TAttribute), inherit).First() as TAttribute;
        if (typeof(TValue) is not null)
            return valueSelector(attr!);

        return default;
    }
    public static TokenValidationParameters ValidateJwtToken(JwtSettingsBase jwtSettings) =>
        new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ClockSkew = TimeSpan.Zero
        };

    // public static bool IsEmptyList<T>(ICollection<T> list) => !list.Any(); //false == false => true
    public static bool IsEmptyList<T>(ICollection<T> list) => list.Count == 0;
    public static bool IsNotEmptyList<T>(ICollection<T> list) => list.Count > 0;
}
