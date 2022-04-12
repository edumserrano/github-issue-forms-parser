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
            .Select(optionText =>
            {
                string label;
                bool isChecked;
                if (optionText.StartsWith("- [X]", StringComparison.Ordinal))
                {
                    label = optionText
                        .TrimStart("- [X]".ToArray())
                        .TrimIssueText();
                    isChecked = true;
                }
                else if (optionText.StartsWith("- [ ]", StringComparison.Ordinal))
                {
                    label = optionText
                        .TrimStart("- [ ]".ToArray())
                        .TrimIssueText();
                    isChecked = false;
                }
                else
                {
                    throw IssueFormBodyParserException.InvalidCheckboxOption(optionText);
                }

                return new IssueFormCheckboxOption(label, isChecked);
            })
            .ToList();
    }
}
