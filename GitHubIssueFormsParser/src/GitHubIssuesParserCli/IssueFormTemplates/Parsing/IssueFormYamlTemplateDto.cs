namespace GitHubIssuesParserCli.IssueFormTemplates.Parsing;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization
internal class IssueFormYamlTemplateDto
{
    [YamlMember(Alias = "body")]
    public List<IssueFormYamlTemplateItemDto>? Body { get; init; }
}

internal class IssueFormYamlTemplateItemDto
{
    [YamlMember(Alias = "id")]
    public string? Id { get; init; }

    [YamlMember(Alias = "type")]
    public IssueFormYamlTemplateItemDtoTypes Type { get; init; }

    [YamlMember(Alias = "attributes")]
    public IssueFormYamlTemplateItemAttributesDto? Attributes { get; init; }
}

internal class IssueFormYamlTemplateItemAttributesDto
{
    [YamlMember(Alias = "label")]
    public string? Label { get; init; }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Used via generics on YML deserialization

