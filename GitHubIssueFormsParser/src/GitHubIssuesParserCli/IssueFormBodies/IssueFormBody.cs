namespace GitHubIssuesParserCli.IssueFormBodies;

internal sealed class IssueFormBody
{
    private static readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
    {
        WriteIndented = false,
        Converters = { new IssueFormBodyJsonConverter() },
    };

    public IssueFormBody(List<IssueFormItem> items)
    {
        Items = items.NotNull();
    }

    public List<IssueFormItem> Items { get; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, _serializeOptions);
    }

    public void WriteAsJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        foreach (var item in Items)
        {
            item.WriteAsJson(writer);
        }

        writer.WriteEndObject();
    }
}
