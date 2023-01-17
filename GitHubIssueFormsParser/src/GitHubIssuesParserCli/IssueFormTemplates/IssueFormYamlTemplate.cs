namespace GitHubIssuesParserCli.IssueFormTemplates;

internal sealed class IssueFormYamlTemplate
{
    public IssueFormYamlTemplate(List<IssueFormYmlTemplateItem> items)
    {
        Items = items.NotNull();
    }

    public List<IssueFormYmlTemplateItem> Items { get; }
}

internal sealed class IssueFormYmlTemplateItem
{
    public IssueFormYmlTemplateItem(string id, IssueFormYamlTemplateItemTypes type, string label)
    {
        Id = id.NotNull();
        Type = type;
        Label = new IssueFormYmlTemplateItemLabel(label);
    }

    public string Id { get; }

    public IssueFormYamlTemplateItemTypes Type { get; }

    public IssueFormYmlTemplateItemLabel Label { get; }
}

internal sealed class IssueFormYmlTemplateItemLabel
{
    public IssueFormYmlTemplateItemLabel(string label)
    {
        Value = label.NotNullOrWhiteSpace();
        H3HeaderValue = $"### {label}";
    }

    public string Value { get; }

    public string H3HeaderValue { get; }
}

internal enum IssueFormYamlTemplateItemTypes
{
    Dropdown,
    Input,
    Textarea,
    Checkboxes,
}
