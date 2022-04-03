using System.Text.Json;
using GitHubIssuesParserCli.IssueFormBody.Parser;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text
{
    internal sealed class GitHubIssueFormText : GitHubIssueFormItem
    {
        private GitHubIssueFormText(string id, string text)
            : base(id, GitHubIssueFormItemTypes.Text)
        {
            Text = text;
        }

        public string Text { get; }

        internal static GitHubIssueFormText Create(string id, string value)
        {
            var sanitezedValue = value.Sanitize();
            return new GitHubIssueFormText(id, sanitezedValue);
        }

        public override void WriteAsJson(Utf8JsonWriter writer)
        {
            writer.WriteString(Id, Text);
        }
    }
}
