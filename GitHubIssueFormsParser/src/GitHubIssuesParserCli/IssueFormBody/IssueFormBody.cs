namespace GitHubIssuesParserCli.IssueFormBody;

internal sealed class IssueFormBody
{
    public IssueFormBody(List<IssueFormItem> items)
    {
        Items = items.NotNull();
    }

    public List<IssueFormItem> Items { get; }

    public string ToJson()
    {
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            Converters =
            {
                new IssueFormBodyJsonConverter(),
            },
        };
        return JsonSerializer.Serialize(this, serializeOptions);
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
