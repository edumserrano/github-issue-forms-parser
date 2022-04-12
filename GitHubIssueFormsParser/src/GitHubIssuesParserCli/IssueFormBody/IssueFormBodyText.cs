namespace GitHubIssuesParserCli.IssueFormBody;

internal record IssueFormBodyText
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
}