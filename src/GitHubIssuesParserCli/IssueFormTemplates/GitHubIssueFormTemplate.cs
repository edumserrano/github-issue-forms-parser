using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli.IssueFormTemplates
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization
    internal class GitHubIssueFormTemplate
    {
        [YamlMember(Alias = "body")]
        public List<GitHubIssueFormTemplateItem> Body { get; init; } = default!;
    }

    internal class GitHubIssueFormTemplateItem
    {
        [YamlMember(Alias = "id")]
        public string Id { get; init; } = default!;

        [YamlMember(Alias = "type")]
        public GitHubIssueFormTemplateItemTypes Type { get; init; }

        [YamlMember(Alias = "attributes")]
        public GitHubIssueFormTemplateItemAttributes Attributes { get; init; } = default!;
    }

    internal class GitHubIssueFormTemplateItemAttributes
    {
        [YamlMember(Alias = "label")]
        public string Label { get; init; } = default!;
    }

    internal enum GitHubIssueFormTemplateItemTypes
    {
        Dropdown,
        Markdown,
        Input,
        Textarea,
        Checkboxes,
    }
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization

}
