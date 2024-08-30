namespace GitHubIssuesParserCli.Tests.CliIntegration;

/// <summary>
/// These tests make sure that the CLI interface is as expected.
/// IE: if the command name changes or the options change then these tests would pick that up.
/// These tests also test the <see cref="IBindingValidator"/> validators of the command options.
/// </summary>
[Trait("Category", XUnitCategories.Integration)]
public class CliIntegrationTests
{
    /// <summary>
    /// Tests that if no arguments are passed the CLI returns the help text.
    /// </summary>
    [Fact]
    public async Task NoArguments()
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);
        await app.RunAsync();
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/CliOutputNoArgs");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --issue-body option is required for the 'parse-issue-form' command.
    /// </summary>
    [Fact]
    public async Task IssueBodyOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[] { "parse-issue-form", "--template-filepath", "some filepath" };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/CliOutputUsage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests that the --template-filepath option is required for the 'parse-issue-form' command.
    /// </summary>
    [Fact]
    public async Task TemplateFilepathOptionIsRequired()
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[] { "parse-issue-form", "--issue-body", "some issue body" };
        await app.RunAsync(args);
        var output = console.ReadOutputString();
        var expectedOutput = OsDependantOutput.ReadAllText("./TestFiles/CliOutputUsage");
        output.ShouldEndWith(expectedOutput);
    }

    /// <summary>
    /// Tests the validation of the --issue-body option for the 'parse-issue-form' command.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task IssueBodyOptionValidation(string issueBody)
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[] { "parse-issue-form", "--issue-body", issueBody, "--template-filepath", "some filepath" };
        await app.RunAsync(args);
        var error = console.ReadErrorString();

        var expectedError = await File.ReadAllTextAsync("./TestFiles/CliErrorIssueBodyValidation.txt");
        error.ShouldBeWithNormalizedNewlines(expectedError);
    }

    /// <summary>
    /// Tests the validation of the --template-filepath option for the 'parse-issue-form' command.
    /// Tests for empty string or whitespace.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task TemplateFilepathOptionValidation(string templateFilepath)
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[] { "parse-issue-form", "--issue-body", "some body", "--template-filepath", templateFilepath };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = await File.ReadAllTextAsync("./TestFiles/CliErrorTemplateFilepathValidation.txt");
        error.ShouldBeWithNormalizedNewlines(expectedError);
    }

    /// <summary>
    /// Tests the validation of the --template-filepath option for the 'parse-issue-form' command.
    /// Tests for file existance.
    /// </summary>
    [Fact]
    public async Task TemplateFilepathOptionValidation2()
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var args = new[] { "parse-issue-form", "--issue-body", "some body", "--template-filepath", "non-existent-file.txt" };
        await app.RunAsync(args);
        var error = console.ReadErrorString();
        var expectedError = await File.ReadAllTextAsync("./TestFiles/CliErrorTemplateFilepathValidation2.txt");
        error.ShouldBeWithNormalizedNewlines(expectedError);
    }

    /// <summary>
    /// Tests the correct value for the options that can be used with the 'parse-issue-form' command.
    /// This test uses an issue body where the checkboxes use both an Uppercase and Lowercase 'X' or aren't
    /// checked.
    /// </summary>
    [Theory]
    [InlineData("-i", "-t")]
    [InlineData("--issue-body", "--template-filepath")]
    public async Task ExpectedUsage(string issueFormOptionName, string templateFilepathOptionName)
    {
        using var console = new FakeInMemoryConsole();
        var app = new IssuesParserCli();
        app.CliApplicationBuilder.UseConsole(console);

        var issueFormBody = await File.ReadAllTextAsync("./TestFiles/IssueBody.md");
        issueFormBody = issueFormBody.NormalizeLineEndings();
        const string templateFilepath = "./TestFiles/Template.yml";
        var args = new[] { "parse-issue-form", issueFormOptionName, issueFormBody, templateFilepathOptionName, templateFilepath };
        await app.RunAsync(args);
        var output = console.ReadOutputString();

        var issueFormJson = JsonSerializer.Deserialize<IssueFormTestModel>(output);
        issueFormJson.ShouldNotBeNull();
    }
}
