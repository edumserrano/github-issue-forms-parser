using GitHubIssuesParserCli.IssueFormTemplates;

namespace GitHubIssuesParserCli.IssueFormBody.Parser
{
    internal static class GitHubIssueFormBodyParserExtensions
    {
        public static GitHubIssueFormBody ToGitHubIssueFormBody(this string issueFormBody, GitHubIssueFormTemplate issueFormTemplate)
        {
            return GitHubIssueFormBodyParser.Parse(issueFormBody, issueFormTemplate);
        }

        public static GitHubIssueFormBody ToGitHubIssueFormBody(this string issueFormBody, string yamlTemplate)
        {
            var issueFormTemplate = yamlTemplate.ToGitHubIssueFormTemplate();
            return issueFormBody.ToGitHubIssueFormBody(issueFormTemplate);
        }

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
