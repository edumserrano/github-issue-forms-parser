namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems;

public sealed class CreateIssueFormItemException : Exception
{
    private CreateIssueFormItemException(string message)
        : base(message)
    {
    }

    internal static CreateIssueFormItemException Markdown()
    {
        var message = $"Cannot create {typeof(IssueFormItem)}. {IssueFormYamlTemplateItemTypes.Markdown} template items are not part of the issue form body and as such the {typeof(IssueFormItemFactory)} doesn't support creating them.";
        return new CreateIssueFormItemException(message);
    }

    internal static CreateIssueFormItemException UnexpectedType(IssueFormYamlTemplateItemTypes type)
    {
        var message = $"Cannot create {typeof(IssueFormItem)}. Unexpected {typeof(IssueFormYamlTemplateItemTypes)}: {type}";
        return new CreateIssueFormItemException(message);
    }
}
