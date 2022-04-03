using System.Text.Json;

namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkbox
{
    internal sealed class GitHubIssueFormCheckboxes : GitHubIssueFormItem
    {
        public GitHubIssueFormCheckboxes(string id, List<GitHubIssueFormCheckboxOption> options)
            : base(id, GitHubIssueFormItemTypes.Checkboxes)
        {
            Options = options;
        }

        public List<GitHubIssueFormCheckboxOption> Options { get; }

        internal static GitHubIssueFormCheckboxes Create(string id, string value)
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
                            // TODO use sanitize method on string instead of just trim? Or create another sanite method?
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

        public override void WriteAsJson(Utf8JsonWriter writer)
        {
            writer.WriteStartObject(Id);
            foreach (var option in Options)
            {
                writer.WriteBoolean(option.Name, option.IsChecked);
            }

            writer.WriteEndObject();
        }
    }
}
