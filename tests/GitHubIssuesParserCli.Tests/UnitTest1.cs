namespace GitHubIssuesParserCli.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1Async()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseGitHubIssueFormCommand
        {
            IssueFormBody = string.Empty,
            TemplateFilepath = string.Empty,
        };

        // Act
        await command.ExecuteAsync(console);
        var stdOut = console.ReadOutputString();

        // Assert
        // Assert.That(stdOut, Is.EqualTo("foo bar"));
    }
}
