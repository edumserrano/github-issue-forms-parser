namespace GitHubIssuesParserCli.CliCommands;

[Command]
public class ParseGitHubIssueFormCommand : ICommand
{
    [CommandOption("issue-body", 'i', Description = "The body of the GitHub issue form.")]
    public string IssueFormBody { get; init; } = default!;

    [CommandOption("template-filepath", 't', Description = "The filepath for the GitHub issue form yml template.")]
    public string TemplateFilepath { get; init; } = default!;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        if (console is null)
        {
            throw new ArgumentNullException(nameof(console));
        }

        var ymlTemplateAsString = await File.ReadAllTextAsync(TemplateFilepath);
        var issueFormBodyText = new IssueFormBodyText(IssueFormBody);
        var issueFormTemplateText = new IssueFormYmlTemplateText(ymlTemplateAsString);
        var issueFormTemplate = IssueFormYmlTemplateParser.Parse(issueFormTemplateText);
        var issueFormBody = IssueFormBodyParser.Parse(issueFormBodyText, issueFormTemplate);
        var jsonString = issueFormBody.ToJson();
        await console.Output.WriteLineAsync(jsonString);
    }
}
