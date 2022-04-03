using GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkbox;
using GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text;
using GitHubIssuesParserCli.IssueFormTemplates;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems
{
    internal static class IssueFormItemFactory
    {
        public static IssueFormItem CreateFormItem(
            string id,
            IssueFormYmlTemplateItemTypes type,
            string value)
        {
            return type switch
            {
                IssueFormYmlTemplateItemTypes.Dropdown
                    or IssueFormYmlTemplateItemTypes.Input
                    or IssueFormYmlTemplateItemTypes.Textarea => CreateIssueFormText(id, value),
                IssueFormYmlTemplateItemTypes.Checkboxes => CreateIssueFormCheckboxesItem(id, value),
                IssueFormYmlTemplateItemTypes.Markdown => throw new NotImplementedException($"Cannot {typeof(IssueFormItem)}. {IssueFormYmlTemplateItemTypes.Markdown} template items are not part of the issue form body"),
                _ => throw new NotImplementedException($"Cannot {typeof(IssueFormItem)}. Unexpected {typeof(IssueFormYmlTemplateItemTypes)}: {type}")
            };
        }

        private static IssueFormTextItem CreateIssueFormText(string id, string value)
        {
            var text = new IssueFormText(value);
            return new IssueFormTextItem(id, text);
        }

        private static IssueFormCheckboxesItem CreateIssueFormCheckboxesItem(string id, string value)
        {
            var text = new IssueFormCheckBoxesText(value);
            return new IssueFormCheckboxesItem(id, text);
        }
    }
}
