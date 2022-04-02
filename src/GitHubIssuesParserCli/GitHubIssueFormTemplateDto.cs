using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization

    internal class GitHubIssueFormTemplateDto
    {
        [YamlMember(Alias = "body")]
        public List<GitHubIssueFormItemDto> Body { get; init; } = default!;
    }

    internal class GitHubIssueFormItemDto
    {
        [YamlMember(Alias = "id")]
        public string Id { get; init; } = default!;

        [YamlMember(Alias = "type")]
        public GitHubIssueFormItemDtoTypes Type { get; init; }

        [YamlMember(Alias = "attributes")]
        public GitHubIssueFormItemAttributesDto Attributes { get; init; } = default!;
    }

    internal class GitHubIssueFormItemAttributesDto
    {
        [YamlMember(Alias = "label")]
        public string Label { get; init; } = default!;
    }

    internal enum GitHubIssueFormItemDtoTypes
    {
        Dropdown,
        Markdown,
        Input,
        Textarea,
        Checkboxes,
    }

#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization

}
