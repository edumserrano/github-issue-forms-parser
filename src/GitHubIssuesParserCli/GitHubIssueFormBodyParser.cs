namespace GitHubIssuesParserCli
{
    internal static class GitHubIssueFormBodyParser
    {
        public static GitHubIssueFormBody Parse(string issueFormBody, GitHubIssueFormTemplate issueFormTemplate)
        {
            var issueFormItems = new List<GitHubIssueFormItem>();
            for (var i = 0; i < issueFormTemplate.FormTemplateElements.Count; i++)
            {
                var currentTemplateElement = issueFormTemplate.FormTemplateElements[i];
                var currentLevel3Header = $"### {currentTemplateElement.Label}";
                var startIdx = issueFormBody.IndexOf(currentLevel3Header, StringComparison.Ordinal) + currentLevel3Header.Length;

                string value;
                if (i == issueFormTemplate.FormTemplateElements.Count - 1)
                {
                    var endIx = issueFormBody.Length - 1;
                    value = issueFormBody.Substring(startIdx, endIx - startIdx + 1);
                }
                else
                {
                    var nextTemplateElement = issueFormTemplate.FormTemplateElements[i + 1];
                    var nextLevel3Header = $"### {nextTemplateElement.Label}";
                    var endIx = issueFormBody.IndexOf(nextLevel3Header, StringComparison.Ordinal) - 1;
                    value = issueFormBody.Substring(startIdx, endIx - startIdx + 1);
                }

                var issueFormItem = GitHubIssueFormItemFactory.CreateFormItem(currentTemplateElement.Id, currentTemplateElement.Type, value);
                issueFormItems.Add(issueFormItem);
            }

            return new GitHubIssueFormBody(issueFormItems);
        }
    }

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
