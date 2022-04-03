using System.Text.Json;
using GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

namespace GitHubIssuesParserCli.IssueFormBody
{
    internal class GitHubIssueFormBody
    {
        public GitHubIssueFormBody(List<GitHubIssueFormItem> items)
        {
            Items = items;
        }

        public List<GitHubIssueFormItem> Items { get; }

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
}
