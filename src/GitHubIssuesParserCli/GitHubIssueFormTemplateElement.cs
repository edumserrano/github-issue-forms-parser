namespace GitHubIssuesParserCli
{
    internal record GitHubIssueFormTemplate(List<GitHubIssueFormTemplateElement> FormTemplateElements);

    internal record GitHubIssueFormTemplateElement(
        string Id,
        GitHubIssueFormTemplateElementTypes Type,
        string Label);

    internal enum GitHubIssueFormTemplateElementTypes
    {
        Dropdown,
        Markdown,
        Input,
        Textarea,
        Checkboxes,
    }
}
