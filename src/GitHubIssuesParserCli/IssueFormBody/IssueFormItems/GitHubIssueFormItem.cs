using System.Text.Json;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems
{
    internal abstract class GitHubIssueFormItem
    {
        protected GitHubIssueFormItem(string id, GitHubIssueFormItemTypes type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get; }

        public GitHubIssueFormItemTypes Type { get; }

        public abstract void WriteAsJson(Utf8JsonWriter writer);
    }
}
