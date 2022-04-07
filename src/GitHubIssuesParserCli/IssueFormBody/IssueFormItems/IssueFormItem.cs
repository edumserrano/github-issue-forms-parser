namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

internal abstract class IssueFormItem
{
    protected IssueFormItem(string id, IssueFormItemTypes type)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        Id = id;
        Type = type;
    }

    public string Id { get; }

    public IssueFormItemTypes Type { get; }

    public abstract void WriteAsJson(Utf8JsonWriter writer);
}
