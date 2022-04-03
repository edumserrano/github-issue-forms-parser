using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli.IssueFormTemplates;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization
internal class IssueFormYmlTemplate
{
    [YamlMember(Alias = "body")]
    public List<IssueFormYmlTemplateItem> Body { get; init; } = default!;
}

internal class IssueFormYmlTemplateItem
{
    [YamlMember(Alias = "id")]
    public string Id { get; init; } = default!;

    [YamlMember(Alias = "type")]
    public IssueFormYmlTemplateItemTypes Type { get; init; }

    [YamlMember(Alias = "attributes")]
    public IssueFormYmlTemplateItemAttributes Attributes { get; init; } = default!;
}

internal class IssueFormYmlTemplateItemAttributes
{
    [YamlMember(Alias = "label")]
    public string Label { get; init; } = default!;
}

internal enum IssueFormYmlTemplateItemTypes
{
    Dropdown,
    Markdown,
    Input,
    Textarea,
    Checkboxes,
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization

