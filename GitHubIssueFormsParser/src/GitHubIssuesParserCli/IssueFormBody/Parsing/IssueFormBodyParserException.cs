namespace GitHubIssuesParserCli.IssueFormBody.Parsing;

public sealed class IssueFormBodyParserException : Exception
{
    private IssueFormBodyParserException(string message)
       : base(message)
    {
    }

    internal static IssueFormBodyParserException H3HeaderNotFound(string h3Header)
    {
        var message = $"H3 header value '{h3Header}' not found in issue forms body.";
        return new IssueFormBodyParserException(message);
    }

    internal static IssueFormBodyParserException InvalidH3HeaderValue(IssueFormYmlTemplateItemLabel currentH3Header, IssueFormYmlTemplateItemLabel? nextH3Header)
    {
        var message = nextH3Header is null
            ? $"Failed to obtain the value for H3 header: '{currentH3Header.H3HeaderValue}'."
            : $"Failed to obtain the value for H3 header: '{currentH3Header.H3HeaderValue}'. Couldn't find any text between that H3 header and the next H3 header: '{nextH3Header.H3HeaderValue}'.";
        return new IssueFormBodyParserException(message);
    }

    internal static IssueFormBodyParserException InvalidCheckboxOption(string option)
    {
        var message = $"Invalid checkbox option text: '{option}'.";
        return new IssueFormBodyParserException(message);
    }
}
