namespace GitHubIssuesParserCli
{
    internal static class GitHubIssueFormItemFactory
    {
        public static GitHubIssueFormItem CreateFormItem(
            string id,
            GitHubIssueFormTemplateElementTypes type,
            string value)
        {
            return type switch
            {
                GitHubIssueFormTemplateElementTypes.Dropdown
                    or GitHubIssueFormTemplateElementTypes.Input
                    or GitHubIssueFormTemplateElementTypes.Textarea => CreateIssueFormText(id, value),
                GitHubIssueFormTemplateElementTypes.Checkboxes => CreateIssueFormCheckboxes(id, value),
                _ => throw new NotImplementedException($"Cannot {typeof(GitHubIssueFormItem)}. Unexpected {typeof(GitHubIssueFormTemplateElementTypes)}: {type}")
            };
        }

        private static GitHubIssueFormText CreateIssueFormText(string id, string value)
        {
            var sanitezedValue = value.Sanitize();
            return new GitHubIssueFormText(id, sanitezedValue);
        }

        private static GitHubIssueFormCheckboxes CreateIssueFormCheckboxes(string id, string value)
        {
            var options = CreateCheckboxOptions(value);
            return new GitHubIssueFormCheckboxes(id, options);

            static List<GitHubIssueFormCheckboxOption> CreateCheckboxOptions(string options)
            {
                return options
                    .Split(new string[] { NewLines.UnixNewline, NewLines.WindowsNewline }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(check =>
                    {
                        string name;
                        bool @checked;
                        if (check.StartsWith("- [X]", StringComparison.Ordinal))
                        {
                            // TODO use saniteze method on string instead of just trim? Or create another sanite method?
                            name = check
                                .TrimStart("- [X]".ToArray())
                                .Trim();
                            @checked = true;
                        }
                        else
                        {
                            name = check
                                .TrimStart("- [ ]".ToArray())
                                .Trim();
                            @checked = false;
                        }

                        return new GitHubIssueFormCheckboxOption(name, @checked);
                    })
                    .ToList();
            }
        }
    }
}
