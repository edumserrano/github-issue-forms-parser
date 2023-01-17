namespace GitHubIssuesParserCli.IssueFormTemplates;

internal sealed class IssueFormYamlTemplateText
{
    private readonly string _value;

    public IssueFormYamlTemplateText(string value)
    {
        _value = value.NotNullOrWhiteSpace();
    }

    public static implicit operator string(IssueFormYamlTemplateText issueFormTemplateText)
    {
        return issueFormTemplateText._value;
    }

    public override string ToString() => (string)this;
}
