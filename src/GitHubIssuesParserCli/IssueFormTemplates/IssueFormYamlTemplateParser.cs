namespace GitHubIssuesParserCli.IssueFormTemplates;

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
        // to show some markdown text when creating the issue.
        var items = templateDto.Body
            .Where(x => x.Type is not IssueFormYamlTemplateItemTypes.Markdown)
            .Select(x =>
            {
                var id = x.Id ?? string.Empty;
                var label = x.Attributes?.Label ?? string.Empty;
                return new IssueFormYmlTemplateItem(id, x.Type, label);
            })
            .ToList();
        return new IssueFormYamlTemplate(items);
    }
}
