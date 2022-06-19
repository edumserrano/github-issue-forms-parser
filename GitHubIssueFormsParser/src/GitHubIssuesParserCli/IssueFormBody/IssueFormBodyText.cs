namespace GitHubIssuesParserCli.IssueFormBody;

internal class IssueFormBodyText
{
    private readonly string _value;

    public IssueFormBodyText(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(IssueFormBodyText issueFormBodyText)
    {
        return issueFormBodyText._value;
    }

    public override string ToString() => (string)this;
}
