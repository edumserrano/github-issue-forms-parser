namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems;

internal abstract class IssueFormItem
{
    protected IssueFormItem(string id, IssueFormItemTypes type)
    {
        Id = id.NotNullOrWhiteSpace();
        Type = type;
    }

    public string Id { get; }

    public IssueFormItemTypes Type { get; }

    public abstract void WriteAsJson(Utf8JsonWriter writer);
}
