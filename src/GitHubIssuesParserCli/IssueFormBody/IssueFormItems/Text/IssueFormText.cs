namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text;

internal record IssueFormText
{
    private readonly string _value;

    public IssueFormText(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
        }

        _value = Sanitize(value);
    }

    public static implicit operator string(IssueFormText issueFormTextValue)
    {
        return issueFormTextValue._value;
    }

    private static string Sanitize(string value)
    {
        var sanitezedValue = value
            .Trim()
            .Trim(NewLines.UnixNewlineChars)
            .Trim(NewLines.WindowsNewlineChars);
        return sanitezedValue.IsNoResponse() ? string.Empty : sanitezedValue;
    }
}
