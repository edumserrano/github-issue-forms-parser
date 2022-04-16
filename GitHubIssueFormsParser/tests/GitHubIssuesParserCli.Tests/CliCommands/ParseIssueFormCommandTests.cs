namespace GitHubIssuesParserCli.Tests.CliCommands;

[Trait("Category", XUnitCategories.Commands)]
public class ParseIssueFormCommandTests
{
    /// <summary>
    /// Tests that the <see cref="ParseIssueFormCommand"/> produces the expected JSON output.
    /// </summary>
    [Fact]
    public async Task ParseIssueFormCommandTest1()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = File.ReadAllText("./TestFiles/IssueBody.md"),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        await command.ExecuteAsync(console);
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

    /// <summary>
    /// Tests that the <see cref="ParseIssueFormCommand"/> produces the expected JSON output
    /// when the line endings are only LF.
    /// </summary>
    [Fact]
    public async Task ParseIssueFormCommandTest2()
    {
        var issueFormBody = $"### What NuGet package do you want to release?{LF}{LF}dotnet-sdk-extensions{LF}{LF}### What is the new version for the NuGet package?{LF}{LF}1.0.13-alpha{LF}{LF}### Auto-generate release notes?{LF}{LF}Yes{LF}{LF}### Auto-generate release notes optional?{LF}{LF}_No response_{LF}{LF}### Custom release notes?{LF}{LF}## Custom release notes {LF}{LF}Test 123{LF}{LF}Another line:{LF}- point 1{LF}- point 2{LF}- point 3{LF}{LF}### Which operating systems have you used?{LF}{LF}- [X] macOS{LF}- [X] Windows{LF}- [ ] Linux{LF}- [ ] I don't know{LF}";
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody,
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();

        var issueFormJson = JsonSerializer.Deserialize<IssueFormTestModel>(output);
        issueFormJson.ShouldNotBeNull();
        issueFormJson.NuGetId.ShouldBe("dotnet-sdk-extensions");
        issueFormJson.NuGetVersion.ShouldBe("1.0.13-alpha");
        issueFormJson.AutoGenerateReleaseNotes.ShouldBe("Yes");
        issueFormJson.AutoGenerateReleaseNotesOptional.ShouldBeEmpty();
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes {LF}{LF}Test 123{LF}{LF}Another line:{LF}- point 1{LF}- point 2{LF}- point 3");
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
