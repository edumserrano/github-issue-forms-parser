namespace GitHubIssuesParserCli.IssueFormTemplates
{
    internal static class GitHubIssueFormYmlTemplateParserStringExtensions
    {
        public static GitHubIssueFormTemplate ToGitHubIssueFormTemplate(this string ymlTemplate)
        {
            return GitHubIssueFormYmlTemplateParser.Parse(ymlTemplate);
        }
    }
}
