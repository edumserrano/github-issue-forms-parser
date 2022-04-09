namespace GitHubIssuesParserCli.IssueFormTemplates.Parsing;

internal static class IssueFormYamlTemplateParser
{
    public static IssueFormYamlTemplate Parse(IssueFormYamlTemplateText yamlTemplate)
    {
        yamlTemplate.NotNull();
        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();
        var templateDto = deserializer.Deserialize<IssueFormYamlTemplateDto>(yamlTemplate);
        if (templateDto?.Body is null)
        {
            throw IssueFormYamlTemplateParserException.InvalidYmlTemplate();
        }

        // markdown template item types do NOT show in the issue form body, they are only used
        // to show some markdown text when creating the issue. Excluding them here so that we
        // only get the issue form template items that will be rendered on the issue form
        var items = templateDto.Body
            .Where(x => x.Type is not IssueFormYamlTemplateItemDtoTypes.Markdown)
            .Select(x =>
            {
                var id = x.Id ?? string.Empty;
                var label = x.Attributes?.Label ?? string.Empty;
                var type = x.Type switch
                {
                    IssueFormYamlTemplateItemDtoTypes.Dropdown => IssueFormYamlTemplateItemTypes.Dropdown,
                    IssueFormYamlTemplateItemDtoTypes.Input => IssueFormYamlTemplateItemTypes.Input,
                    IssueFormYamlTemplateItemDtoTypes.Textarea => IssueFormYamlTemplateItemTypes.Textarea,
                    IssueFormYamlTemplateItemDtoTypes.Checkboxes => IssueFormYamlTemplateItemTypes.Checkboxes,
                    IssueFormYamlTemplateItemDtoTypes.Markdown => throw IssueFormYamlTemplateParserException.UnexpectedTemplateItem(x.Type),
                    _ => throw IssueFormYamlTemplateParserException.UnexpectedTemplateItem(x.Type),
                };
                return new IssueFormYmlTemplateItem(id, type, label);
            })
            .ToList();
        return new IssueFormYamlTemplate(items);
    }
}
