using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli
{
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
        public GitHubIssueFormItemDtoTypes Type { get; init; } = GitHubIssueFormItemDtoTypes.Unknown;

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
        Unknown,
        Dropdown,
        Markdown,
        Input,
        Textarea,
        Checkboxes,
    }
}
