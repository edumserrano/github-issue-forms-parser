namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

public sealed class CreateIssueFormItemException : Exception
{
    private CreateIssueFormItemException(string message)
        : base(message)
    {
    }

    internal static CreateIssueFormItemException UnexpectedType(IssueFormYamlTemplateItemTypes type)
    {
        var message = $"Cannot create {typeof(IssueFormItem)}. Unexpected {typeof(IssueFormYamlTemplateItemTypes)}: {type}";
        return new CreateIssueFormItemException(message);
    }
}
