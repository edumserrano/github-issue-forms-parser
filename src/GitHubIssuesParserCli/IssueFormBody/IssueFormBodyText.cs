namespace GitHubIssuesParserCli.IssueFormBody;

internal record IssueFormBodyText
{
    private readonly string _value;

    public IssueFormBodyText(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
    }

    public static implicit operator string(IssueFormBodyText issueFormBodyText)
    {
        return issueFormBodyText._value;
    }
}
