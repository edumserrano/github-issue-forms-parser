namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkboxes;

internal sealed class IssueFormCheckboxOption
{
    public IssueFormCheckboxOption(string label, bool isChecked)
    {
        Label = label.NotNullOrWhiteSpace();
        IsChecked = isChecked;
    }

    public string Label { get; }

    public bool IsChecked { get; }
}
