namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkboxes;

internal record IssueFormCheckBoxesText
{
    public IssueFormCheckBoxesText(string value)
    {
        Options = CreateCheckboxOptions(value);
    }

    public List<IssueFormCheckboxOption> Options { get; }

    private static List<IssueFormCheckboxOption> CreateCheckboxOptions(string options)
    {
        return options
            .Split(new string[] { NewLines.UnixNewline, NewLines.WindowsNewline }, StringSplitOptions.RemoveEmptyEntries)
            .Select(check =>
            {
                string name;
                bool isChecked;
                if (check.StartsWith("- [X]", StringComparison.Ordinal))
                {
                    // TODO use sanitize method on string instead of just trim? Or create another sanite method?
                    name = check
                        .TrimStart("- [X]".ToArray())
                        .Trim();
                    isChecked = true;
                }
                else
                {
                    name = check
                        .TrimStart("- [ ]".ToArray())
                        .Trim();
                    isChecked = false;
                }

                return new IssueFormCheckboxOption(name, isChecked);
            })
            .ToList();
    }
}
