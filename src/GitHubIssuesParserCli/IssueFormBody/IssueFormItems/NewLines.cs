namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal static class NewLines
{
    public const string UnixNewline = "\n\n";
    public const string WindowsNewline = "\r\n";
    public static readonly char[] UnixNewlineChars = UnixNewline.ToCharArray();
    public static readonly char[] WindowsNewlineChars = WindowsNewline.ToCharArray();
}
