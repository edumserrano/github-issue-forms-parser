namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems;

internal static class NewLines
{
    public const char CR = '\r';
    public const char LF = '\n';
    public static readonly char[] UnixNewlineChars = [LF];
    public static readonly char[] WindowsNewlineChars = [CR, LF];
}
