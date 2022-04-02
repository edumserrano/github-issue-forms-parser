using GitHubIssuesParserCli.IssueFormTemplates;

namespace GitHubIssuesParserCli.IssueFormBody
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
}
