namespace GitHubIssuesParserCli.IssueFormBody.Parser
{
    internal static class GitHubIssueFormBodyParserExtensions
    {
        // TODO should this extension method be here ?
        public static string Sanitize(this string value)
        {
            // TODO trim whitespaces as well?
            var sanitezedValue = value
                .Trim(NewLines.UnixNewlineChars)
                .Trim(NewLines.WindowsNewlineChars);
            return sanitezedValue.IsNoResponse() ? string.Empty : sanitezedValue;
        }

        // TODO should this extension method be here ?
        public static bool IsNoResponse(this string value)
        {
            return string.Equals(value, "_No response_", StringComparison.Ordinal);
        }
    }
}
