namespace GitHubIssuesParserCli.IssueFormBody.JsonSerialization;

internal class IssueFormBodyJsonConverter : JsonConverter<IssueFormBody>
{
    public override IssueFormBody Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotSupportedException($"{typeof(IssueFormBodyJsonConverter)} does not support reading a JSON string into a {typeof(IssueFormBody)}");
    }

    public override void Write(
        Utf8JsonWriter writer,
        IssueFormBody value,
        JsonSerializerOptions options)
    {
        value.WriteAsJson(writer);
    }
}
