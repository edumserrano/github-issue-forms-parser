namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal static class TrimExtensions
{
    public static string TrimIssueText(this string value)
    {
        return value.Trim(NewLines.UnixNewlineChars)
            .Trim(NewLines.WindowsNewlineChars)
            .Trim();
    }
}
