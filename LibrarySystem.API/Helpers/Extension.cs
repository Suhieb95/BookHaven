namespace LibrarySystem.API.Helpers;
using FluentValidation;
public static class Extension
{
    public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
    {
        List<T> filteredList =[];

        foreach (T record in records)
            if (func(record))
                filteredList.Add(record);

        return filteredList;
    }
    public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
    {
        ruleBuilder
        .MinimumLength(minimumLength)
        .WithMessage("Password Length has to be at least 8 Characters.")
        .Matches("[a-z]")
        .WithMessage("You need to have at one lowercase letter.")
        .Matches("[A-Z]")
        .WithMessage("You need to have at one uppercase letter.")
        .Matches("[0-9]")
        .WithMessage("You need to have at least one digit.")
        .Matches("[^a-zA-Z0-9]")
        .WithMessage("You need to have at least one special Characters.");
    }

}