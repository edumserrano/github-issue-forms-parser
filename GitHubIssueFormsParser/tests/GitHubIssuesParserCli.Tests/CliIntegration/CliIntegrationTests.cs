namespace GitHubIssuesParserCli.Tests.CliIntegration;

/// <summary>
/// These tests make sure that the CLI interface is as expected.
/// IE: if the command name changes or the options change then these tests would pick that up.
/// </summary>
[Trait("Category", XUnitCategories.Integration)]
public class CliIntegrationTests
{
    /// <summary>
    /// Tests that if no arguments are passed the CLI returns the help text.
    /// </summary>
    [Fact]
    public async Task IntegrationTest1()
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);
        await app.RunAsync();
        var output = console.ReadOutputString();
        var expectedOutput = File.ReadAllText("./TestFiles/CliOutputNoArgs.txt");
        output.ShouldEndWith(expectedOutput);
    }


    /// <summary>
    /// Tests the correct value for the options that can be used with the 'parse-issue-form' command.
    /// </summary>
    [Theory]
    [InlineData("-i", "-t")]
    [InlineData("--issue-body", "--template-filepath")]
    public async Task IntegrationTest2(string issueFormParamName, string templateFilepathParamName)
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var issueFormBody = File.ReadAllText("./TestFiles/IssueBody.md");
        const string templateFilepath = "./TestFiles/Template.yml";
        var args = new[] { "parse-issue-form", issueFormParamName, issueFormBody, templateFilepathParamName, templateFilepath };
        await app.RunAsync(args);
        var output = console.ReadOutputString();

        var issueFormJson = JsonSerializer.Deserialize<IssueFormTestModel>(output);
        issueFormJson.ShouldNotBeNull();
    }
}
