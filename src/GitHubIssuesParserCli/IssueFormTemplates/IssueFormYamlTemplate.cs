namespace GitHubIssuesParserCli.IssueFormTemplates;

internal record IssueFormYamlTemplate
{
    public IssueFormYamlTemplate(List<IssueFormYmlTemplateItem> items)
    {
        Items = items.NotNull();
    }

    public List<IssueFormYmlTemplateItem> Items { get; }
}

internal record IssueFormYmlTemplateItem
{
    public IssueFormYmlTemplateItem(string id, IssueFormYamlTemplateItemTypes type, string label)
    {
        Id = id.NotNullOrWhiteSpace();
        Type = type;
        Label = new IssueFormYmlTemplateItemLabel(label);
    }

    public string Id { get; }

    public IssueFormYamlTemplateItemTypes Type { get; }

    public IssueFormYmlTemplateItemLabel Label { get; }
}

internal record IssueFormYmlTemplateItemLabel
{
    public IssueFormYmlTemplateItemLabel(string label)
    {
        Value = label.NotNullOrWhiteSpace();
        H3HeaderValue = $"### {label}";
    }

    public string Value { get; }

    public string H3HeaderValue { get; }
}
