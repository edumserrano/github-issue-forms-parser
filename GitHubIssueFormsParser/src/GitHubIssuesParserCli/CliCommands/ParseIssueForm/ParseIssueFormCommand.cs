namespace GitHubIssuesParserCli.CliCommands.ParseIssueForm;

[Command("parse-issue-form")]
public class ParseIssueFormCommand : ICommand
{
    [CommandOption(
        "issue-body",
        'i',
        IsRequired = true,
        Validators = new Type[] { typeof(IssueFormBodyOptionValidator) },
        Description = "The body of the GitHub issue form.")]
    public string IssueFormBody { get; init; } = default!;

    [CommandOption(
        "template-filepath",
        't',
        IsRequired = true,
        Validators = new Type[] { typeof(TemplateFilepathOptionValidator) },
        Description = "The filepath for the GitHub issue form YAML template.")]
    public string TemplateFilepath { get; init; } = default!;

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
            var message = @$"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- {e.Message}";
            throw new CommandException(message, innerException: e);
        }
    }
}
