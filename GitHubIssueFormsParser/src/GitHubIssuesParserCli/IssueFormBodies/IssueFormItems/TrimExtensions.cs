namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems;

internal static class TrimExtensions
{
    public static string TrimIssueText(this string value)
    {
        return value.Trim(NewLines.WindowsNewlineChars)
            .Trim(NewLines.UnixNewlineChars)
            .Trim();
    }
}
