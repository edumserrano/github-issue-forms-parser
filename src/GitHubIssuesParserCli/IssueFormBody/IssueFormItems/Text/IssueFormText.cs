using GitHubIssuesParserCli.IssueFormBody.Parser;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text
{
    internal record IssueFormText
    {
        private readonly string _value;

        public IssueFormText(string value)
        {
            _value = value.Sanitize();
        }

        public static implicit operator string(IssueFormText issueFormTextValue)
        {
            return issueFormTextValue._value;
        }
    }
}
