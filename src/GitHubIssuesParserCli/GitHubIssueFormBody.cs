namespace GitHubIssuesParserCli
{
    internal record GitHubIssueFormBody(List<GitHubIssueFormItem> Items);

    internal abstract record GitHubIssueFormItem(string Id, GitHubIssueFormItemTypes Type);

    internal record GitHubIssueFormText(string Id, string Text)
        : GitHubIssueFormItem(Id, GitHubIssueFormItemTypes.Text);

    internal record GitHubIssueFormCheckboxes(string Id, List<GitHubIssueFormCheckboxOption> Options)
        : GitHubIssueFormItem(Id, GitHubIssueFormItemTypes.Checkboxes);

    internal record GitHubIssueFormCheckboxOption(string Name, bool Checked);

    internal enum GitHubIssueFormItemTypes
    {
        Text,
        Checkboxes,
    }
}
