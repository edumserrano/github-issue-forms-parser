namespace GitHubIssuesParserCli.IssueFormTemplates.Parsing;

public sealed class IssueFormYamlTemplateParserException : Exception
{
    private IssueFormYamlTemplateParserException(string message)
       : base(message)
    {
    }

    internal static IssueFormYamlTemplateParserException InvalidYmlTemplate()
    {
        const string message = "Failed to deserialize the issue form template. The template must contain at least an YAML member named 'body' at the first indentation level.";
        return new IssueFormYamlTemplateParserException(message);
    }

    internal static IssueFormYamlTemplateParserException UnexpectedTemplateItem(IssueFormYamlTemplateItemDtoTypes type)
    {
        var message = $"Cannot create {typeof(IssueFormYmlTemplateItem)}. Unexpected {typeof(IssueFormYamlTemplateItemDtoTypes)}: {type}";
        return new IssueFormYamlTemplateParserException(message);
    }
}
