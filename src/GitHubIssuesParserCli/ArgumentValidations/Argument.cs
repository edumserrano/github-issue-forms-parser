namespace GitHubIssuesParserCli.ArgumentValidations;

internal static class Argument
{
    public static T NotNull<T>(this T? value, [CallerArgumentExpression("value")] string name = "")
        where T : class
    {
        return value ?? throw new ArgumentNullException(name);
    }

    internal static string NotNullOrWhiteSpace(this string? value, [CallerArgumentExpression("value")] string name = "")
    {
        var message = $"{name} cannot be null or whitespace.";
        return string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException(message)
            : value;
    }
}
