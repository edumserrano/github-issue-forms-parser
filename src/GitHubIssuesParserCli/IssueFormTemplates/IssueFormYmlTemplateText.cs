namespace GitHubIssuesParserCli.IssueFormTemplates;

internal record IssueFormYmlTemplateText
{
    private readonly string _value;

    public IssueFormYmlTemplateText(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        _value = value;
    }

    public static implicit operator string(IssueFormYmlTemplateText issueFormTemplateText)
    {
        return issueFormTemplateText._value;
    }
}
