namespace GitHubIssuesParserCli.Tests.CliCommands;

[Trait("Category", XUnitCategories.Validation)]
public class ParseIssueFormCommandOptionDefaultsTests
{
    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.IssueFormBody"/> command option default value.
    /// </summary>
    [Fact]
    public void IssueFormBodyDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand();
        command.IssueFormBody.ShouldBeNull();
    }

    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command option default value.
    /// </summary>
    [Fact]
    public void TemplateFilepathDefaultsToNull()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand();
        command.TemplateFilepath.ShouldBeNull();
    }
}
