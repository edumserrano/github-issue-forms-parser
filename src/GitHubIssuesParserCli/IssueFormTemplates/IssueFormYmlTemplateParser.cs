namespace GitHubIssuesParserCli.IssueFormTemplates;

internal static class IssueFormYmlTemplateParser
{
    public static IssueFormYmlTemplate Parse(IssueFormYmlTemplateText ymlTemplate)
    {
        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();
        return deserializer.Deserialize<IssueFormYmlTemplate>(ymlTemplate);
    }
}
