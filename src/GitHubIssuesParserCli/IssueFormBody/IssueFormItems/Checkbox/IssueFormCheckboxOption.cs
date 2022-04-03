namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkbox
{
    internal class IssueFormCheckboxOption
    {
        public IssueFormCheckboxOption(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }

        public string Name { get; }

        public bool IsChecked { get; }
    }
}
