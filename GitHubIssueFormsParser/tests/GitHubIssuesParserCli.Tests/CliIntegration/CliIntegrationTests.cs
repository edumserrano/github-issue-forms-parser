namespace GitHubIssuesParserCli.Tests.CliIntegration;

[Trait("Category", XUnitCategories.Integration)]
public class CliIntegrationTests
{
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
        issueFormJson.NuGetId.ShouldBe("dotnet-sdk-extensions");
        issueFormJson.NuGetVersion.ShouldBe("1.0.13-alpha");
        issueFormJson.AutoGenerateReleaseNotes.ShouldBe("Yes");
        issueFormJson.AutoGenerateReleaseNotesOptional.ShouldBeEmpty();
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes {CRLF}{CRLF}Test 123{CRLF}{CRLF}Another line:{CRLF}- point 1{CRLF}- point 2{CRLF}- point 3");
        issueFormJson.OperatingSystems.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldBe(true);
        issueFormJson.OperatingSystems.Windows.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Windows.ShouldBe(true);
        issueFormJson.OperatingSystems.Linux.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Linux.ShouldBe(false);
        issueFormJson.OperatingSystems.Unknown.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Unknown.ShouldBe(false);
    }
}
