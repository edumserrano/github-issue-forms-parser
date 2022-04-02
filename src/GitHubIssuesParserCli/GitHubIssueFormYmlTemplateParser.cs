using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli
{
    internal static class GitHubIssueFormYmlTemplateParser
    {
        public static GitHubIssueFormTemplate Parse(string ymlTemplate)
        {
            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();
            var issueTemplate = deserializer.Deserialize<GitHubIssueFormTemplateDto>(ymlTemplate);
            var formTemplateElements = issueTemplate.Body
                .Where(issueFormItem => issueFormItem.Type is not GitHubIssueFormItemDtoTypes.Markdown)
                .Select(issueFormItem =>
                {
                    var type = issueFormItem.Type switch
                    {
                        GitHubIssueFormItemDtoTypes.Dropdown => GitHubIssueFormTemplateElementTypes.Dropdown,
                        GitHubIssueFormItemDtoTypes.Markdown => GitHubIssueFormTemplateElementTypes.Markdown,
                        GitHubIssueFormItemDtoTypes.Input => GitHubIssueFormTemplateElementTypes.Input,
                        GitHubIssueFormItemDtoTypes.Textarea => GitHubIssueFormTemplateElementTypes.Textarea,
                        GitHubIssueFormItemDtoTypes.Checkboxes => GitHubIssueFormTemplateElementTypes.Checkboxes,
                        _ => throw new NotImplementedException()
                    };
                    return new GitHubIssueFormTemplateElement(issueFormItem.Id, type, issueFormItem.Attributes.Label);
                })
                .ToList();
            return new GitHubIssueFormTemplate(formTemplateElements);
        }
    }

    internal static class GitHubIssueFormYmlTemplateParserStringExtensions
    {
        public static GitHubIssueFormTemplate ToGitHubIssueFormTemplate(this string ymlTemplate)
        {
            return GitHubIssueFormYmlTemplateParser.Parse(ymlTemplate);
        }
    }
}
