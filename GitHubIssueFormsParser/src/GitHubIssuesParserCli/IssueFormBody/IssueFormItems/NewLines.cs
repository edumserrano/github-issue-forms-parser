namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal static class NewLines
{
    public const string CR = "\r";
    public const string LF = "\n";
    public static readonly char[] UnixNewlineChars = CR.ToCharArray();
    public static readonly char[] WindowsNewlineChars = LF.ToCharArray();
}
