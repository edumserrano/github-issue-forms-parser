namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkboxes;

internal record IssueFormCheckBoxesText
{
    public IssueFormCheckBoxesText(string value)
    {
        value.NotNullOrWhiteSpace();
        Options = CreateCheckboxOptions(value);
    }

    public List<IssueFormCheckboxOption> Options { get; }

    private static List<IssueFormCheckboxOption> CreateCheckboxOptions(string options)
    {
        return options
            .Split(new string[] { NewLines.UnixNewline, NewLines.WindowsNewline }, StringSplitOptions.RemoveEmptyEntries)
            .Select(check =>
            {
                string label;
                bool isChecked;
                if (check.StartsWith("- [X]", StringComparison.Ordinal))
                {
                    // TODO use sanitize method on string instead of just trim? Or create another sanite method?
                    label = check
                        .TrimStart("- [X]".ToArray())
                        .Trim();
                    isChecked = true;
                }
                else
                {
                    label = check
                        .TrimStart("- [ ]".ToArray())
                        .Trim();
                    isChecked = false;
                }

                return new IssueFormCheckboxOption(label, isChecked);
            })
            .ToList();
    }
}
