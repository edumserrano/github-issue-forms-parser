namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Text;

internal class IssueFormText
{
    private readonly string _value;

    public IssueFormText(string value)
    {
        value.NotNullOrWhiteSpace();
        _value = Sanitize(value);
    }

    public static implicit operator string(IssueFormText issueFormTextValue)
    {
        return issueFormTextValue._value;
    }

    private static string Sanitize(string value)
    {
        var sanitezedValue = value.TrimIssueText();
        return sanitezedValue.IsNoResponse() ? string.Empty : sanitezedValue;
    }
}
