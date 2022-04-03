using System.Text.Json;
using GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

namespace GitHubIssuesParserCli.IssueFormBody
{
    internal class IssueFormBody
    {
        public IssueFormBody(List<IssueFormItem> items)
        {
            Items = items;
        }

        public List<IssueFormItem> Items { get; }

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
