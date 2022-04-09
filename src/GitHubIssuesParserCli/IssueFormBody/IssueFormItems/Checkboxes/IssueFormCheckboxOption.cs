namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkboxes;

internal class IssueFormCheckboxOption
{
    public IssueFormCheckboxOption(string name, bool isChecked)
    {
        Name = name.NotNullOrWhiteSpace();
        IsChecked = isChecked;
    }

    public string Name { get; }

    public bool IsChecked { get; }
}
