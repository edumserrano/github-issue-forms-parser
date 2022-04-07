namespace GitHubIssuesParserCli.IssueFormBody;

internal class IssueFormBody
{
    public IssueFormBody(List<IssueFormItem> items)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    public List<IssueFormItem> Items { get; }

    public string ToJson()
    {
        var serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true, // TODO change to false
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
