namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems.Checkboxes;

internal sealed class IssueFormCheckBoxesText
{
    public IssueFormCheckBoxesText(string value)
    {
        value.NotNullOrWhiteSpace();
        Options = CreateCheckboxOptions(value);
    }

    public List<IssueFormCheckboxOption> Options { get; }

    private static List<IssueFormCheckboxOption> CreateCheckboxOptions(string options)
    {
        var checkboxOptions = options
            .Split([NewLines.CR, NewLines.LF], StringSplitOptions.RemoveEmptyEntries)
            .Select(optionText =>
            {
                string label;
                bool isChecked;
                if (optionText.StartsWith("- [X]", StringComparison.Ordinal))
                {
                    label = optionText
                        .TrimStart([.. "- [X]"])
                        .TrimIssueText();
                    isChecked = true;
                }
                else if (optionText.StartsWith("- [x]", StringComparison.Ordinal))
                {
                    label = optionText
                        .TrimStart([.. "- [x]"])
                        .TrimIssueText();
                    isChecked = true;
                }
                else if (optionText.StartsWith("- [ ]", StringComparison.Ordinal))
                {
                    label = optionText
                        .TrimStart([.. "- [ ]"])
                        .TrimIssueText();
                    isChecked = false;
                }
                else
                {
                    throw IssueFormBodyParserException.InvalidCheckboxOption(optionText);
                }

                return new IssueFormCheckboxOption(label, isChecked);
            });
        return [.. checkboxOptions];
    }
}
