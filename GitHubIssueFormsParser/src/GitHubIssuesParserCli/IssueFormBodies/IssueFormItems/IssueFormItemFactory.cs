namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems;

internal static class IssueFormItemFactory
{
    public static IssueFormItem CreateFormItem(
        string id,
        IssueFormYamlTemplateItemTypes type,
        string value)
    {
        return type switch
        {
            IssueFormYamlTemplateItemTypes.Dropdown
                or IssueFormYamlTemplateItemTypes.Input
                or IssueFormYamlTemplateItemTypes.Textarea => CreateIssueFormText(id, value),
            IssueFormYamlTemplateItemTypes.Checkboxes => CreateIssueFormCheckboxesItem(id, value),
            _ => throw CreateIssueFormItemException.UnexpectedType(type),
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
