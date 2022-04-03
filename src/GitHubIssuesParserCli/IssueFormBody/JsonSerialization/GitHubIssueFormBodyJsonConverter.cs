using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubIssuesParserCli.IssueFormBody.JsonSerialization
{
    internal class GitHubIssueFormBodyJsonConverter : JsonConverter<GitHubIssueFormBody>
    {
        public override GitHubIssueFormBody Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotSupportedException($"{typeof(GitHubIssueFormBodyJsonConverter)} does not support reading a JSON string into a {typeof(GitHubIssueFormBody)}");
        }

        public override void Write(
            Utf8JsonWriter writer,
            GitHubIssueFormBody value,
            JsonSerializerOptions options)
        {
            value.WriteAsJson(writer);
        }
    }
}
