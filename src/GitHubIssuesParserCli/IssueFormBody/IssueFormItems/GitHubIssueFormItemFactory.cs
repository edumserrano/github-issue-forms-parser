using GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkbox;
using GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text;
using GitHubIssuesParserCli.IssueFormTemplates;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems
{
    internal static class GitHubIssueFormItemFactory
    {
        public static GitHubIssueFormItem CreateFormItem(
            string id,
            GitHubIssueFormTemplateItemTypes type,
            string value)
        {
            return type switch
            {
                GitHubIssueFormTemplateItemTypes.Dropdown
                    or GitHubIssueFormTemplateItemTypes.Input
                    or GitHubIssueFormTemplateItemTypes.Textarea => GitHubIssueFormText.Create(id, value),
                GitHubIssueFormTemplateItemTypes.Checkboxes => GitHubIssueFormCheckboxes.Create(id, value),
                GitHubIssueFormTemplateItemTypes.Markdown => throw new NotImplementedException($"Cannot {typeof(GitHubIssueFormItem)}. {GitHubIssueFormTemplateItemTypes.Markdown} template items are not part of the issue form body"),
                _ => throw new NotImplementedException($"Cannot {typeof(GitHubIssueFormItem)}. Unexpected {typeof(GitHubIssueFormTemplateItemTypes)}: {type}")
            };
        }
    }
}
