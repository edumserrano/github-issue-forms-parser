using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubIssuesParserCli
{
    internal static class GitHubIssueFormBodyJsonSerializationExtensions
    {
        public static void WriteGitHubIssueFormBody(this Utf8JsonWriter writer, GitHubIssueFormBody issueFormBody)
        {
            writer.WriteStartObject();
            foreach (var item in issueFormBody.Items)
            {
                writer.WriteGitHubIssueFormItem(item);
            }

            writer.WriteEndObject();
        }

        public static void WriteGitHubIssueFormItem(this Utf8JsonWriter writer, GitHubIssueFormItem issueFormItem)
        {
            switch (issueFormItem.Type)
            {
                case GitHubIssueFormItemTypes.Text:
                    writer.WriteGitHubIssueFormText((GitHubIssueFormText)issueFormItem);
                    break;
                case GitHubIssueFormItemTypes.Checkboxes:
                    writer.WriteGitHubIssueFormCheckboxes((GitHubIssueFormCheckboxes)issueFormItem);
                    break;
                default:
                    throw new NotSupportedException($"{typeof(GitHubIssueFormBodyJsonConverter)} failed to write {typeof(GitHubIssueFormItem)}. Unexpected item type: {issueFormItem.Type}");
            }
        }

        public static void WriteGitHubIssueFormText(this Utf8JsonWriter writer, GitHubIssueFormText issueFormText)
        {
            writer.WriteString(issueFormText.Id, issueFormText.Text);
        }

        public static void WriteGitHubIssueFormCheckboxes(this Utf8JsonWriter writer, GitHubIssueFormCheckboxes issueFormCheckboxes)
        {
            writer.WriteStartObject(issueFormCheckboxes.Id);
            foreach (var option in issueFormCheckboxes.Options)
            {
                writer.WriteBoolean(option.Name, option.Checked);
            }

            writer.WriteEndObject();
        }
    }

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
            writer.WriteGitHubIssueFormBody(value);
        }
    }
}
