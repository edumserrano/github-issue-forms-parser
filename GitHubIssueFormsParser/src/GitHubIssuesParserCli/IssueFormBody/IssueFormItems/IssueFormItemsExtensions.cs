namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal static class IssueFormItemsExtensions
{
    public static bool IsNoResponse(this string value)
    {
        return string.Equals(value, "_No response_", StringComparison.Ordinal);
    }
}
