namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal abstract class IssueFormItem
{
    protected IssueFormItem(string id, IssueFormItemTypes type)
    {
        Id = id;
        Type = type;
    }

    public string Id { get; }

    public IssueFormItemTypes Type { get; }

    public abstract void WriteAsJson(Utf8JsonWriter writer);
}
