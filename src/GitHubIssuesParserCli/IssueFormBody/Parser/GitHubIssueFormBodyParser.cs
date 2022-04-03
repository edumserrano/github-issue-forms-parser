using GitHubIssuesParserCli.IssueFormBody.IssueFormItems;
using GitHubIssuesParserCli.IssueFormTemplates;

namespace GitHubIssuesParserCli.IssueFormBody.Parser
{
    internal static class GitHubIssueFormBodyParser
    {
        public static GitHubIssueFormBody Parse(string issueFormBody, GitHubIssueFormTemplate issueFormTemplate)
        {
            // markdown template item types do NOT show in the issue form body, they are only used
            // to show some markdown text when creating the issue.
            var templateItems = issueFormTemplate.Body
                .Where(x => x.Type is not GitHubIssueFormTemplateItemTypes.Markdown)
                .ToList();
            var issueFormItems = new List<GitHubIssueFormItem>();
            for (var i = 0; i < templateItems.Count; i++)
            {
                var currentTemplateItem = templateItems[i];
                var nextTemplateItem = templateItems.GetNextTemplateElement(i);
                var (startIdx, valueLength) = GetLevel3HeaderValueIndexes(currentTemplateItem, nextTemplateItem, issueFormBody);
                var value = issueFormBody.Substring(startIdx, valueLength);
                var issueFormItem = GitHubIssueFormItemFactory.CreateFormItem(currentTemplateItem.Id, currentTemplateItem.Type, value);
                issueFormItems.Add(issueFormItem);
            }

            return new GitHubIssueFormBody(issueFormItems);
        }

        private static GitHubIssueFormTemplateItem? GetNextTemplateElement(this List<GitHubIssueFormTemplateItem> templateItems, int templateItemIdx)
        {
            return templateItemIdx == templateItems.Count - 1 ? null : templateItems[templateItemIdx + 1];
        }

        private static (int startIdx, int valueLength) GetLevel3HeaderValueIndexes(
            GitHubIssueFormTemplateItem currentTemplateElement,
            GitHubIssueFormTemplateItem? nextTemplateElement,
            string issueFormBody)
        {
            var currentLevel3Header = $"### {currentTemplateElement.Attributes.Label}";
            var startIdx = issueFormBody.IndexOf(currentLevel3Header, StringComparison.Ordinal) + currentLevel3Header.Length;

            int endIdx;
            if (nextTemplateElement is null)
            {
                endIdx = issueFormBody.Length - 1;
            }
            else
            {
                var nextLevel3Header = $"### {nextTemplateElement.Attributes.Label}";
                endIdx = issueFormBody.IndexOf(nextLevel3Header, StringComparison.Ordinal) - 1;
            }

            var valueLength = endIdx - startIdx + 1;
            return (startIdx, valueLength);
        }
    }
}
