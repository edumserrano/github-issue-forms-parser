namespace GitHubIssuesParserCli.CliCommands;

[Command("parse-issue-form")]
public class ParseIssueFormCommand : ICommand
{
    [CommandOption("issue-body", 'i', Description = "The body of the GitHub issue form.")]
    public string? IssueFormBody { get; init; }

    [CommandOption("template-filepath", 't', Description = "The filepath for the GitHub issue form YAML template.")]
    public string? TemplateFilepath { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            console.NotNull();
            TemplateFilepath.NotNullOrWhiteSpace();
            IssueFormBody.NotNullOrWhiteSpace();

            var yamlTemplateAsString = await File.ReadAllTextAsync(TemplateFilepath);
            var issueFormBodyText = new IssueFormBodyText(IssueFormBody);
            var issueFormTemplateText = new IssueFormYamlTemplateText(yamlTemplateAsString);
            var issueFormTemplate = IssueFormYamlTemplateParser.Parse(issueFormTemplateText);
            var issueFormBody = IssueFormBodyParser.Parse(issueFormBodyText, issueFormTemplate);
            var issueFormBodyAsJson = issueFormBody.ToJson();
            await console.Output.WriteLineAsync(issueFormBodyAsJson);
        }
        catch (Exception e)
        {
            throw new ParseGitHubIssueFormCommandException(e);
        }
    }
}
