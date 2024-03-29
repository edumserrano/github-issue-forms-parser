namespace GitHubIssuesParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseIssueFormCommand"/> outputs the expected JSON value to the console.
/// </summary>
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
        var issueFormBody = await File.ReadAllTextAsync("./TestFiles/IssueBody.md");
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody.NormalizeLineEndings(),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();

        var issueFormJson = JsonSerializer.Deserialize<IssueFormTestModel>(output);
        issueFormJson.ShouldNotBeNull();
        issueFormJson.NuGetId.ShouldBe("dotnet-sdk-extensions");
        issueFormJson.NuGetVersion.ShouldBe("1.0.13-alpha");
        issueFormJson.AutoGenerateReleaseNotes.ShouldBe("Yes");
        issueFormJson.PushNuget.ShouldBeEmpty();
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes{Environment.NewLine}{Environment.NewLine}Test 123{Environment.NewLine}\"line with double quotes\"{Environment.NewLine}{Environment.NewLine}Another line:{Environment.NewLine}- point 1{Environment.NewLine}- point 2{Environment.NewLine}- point 3");
        issueFormJson.OperatingSystems.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldBe(expected: true);
        issueFormJson.OperatingSystems.Windows.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Windows.ShouldBe(expected: true);
        issueFormJson.OperatingSystems.Linux.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Linux.ShouldBe(expected: false);
        issueFormJson.OperatingSystems.Unknown.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Unknown.ShouldBe(expected: false);
    }

    /// <summary>
    /// Tests that the <see cref="ParseIssueFormCommand"/> produces the expected JSON output
    /// regardless of the line endings on the issue form body.
    /// </summary>
    [Theory]
    [InlineData(LF)]
    [InlineData(CR + LF)]
    public async Task ParseIssueFormCommandTest2(string newLine)
    {
        var issueFormBody = $"### What NuGet package do you want to release?{newLine}{newLine}dotnet-sdk-extensions{newLine}{newLine}### What is the new version for the NuGet package?{newLine}{newLine}1.0.13-alpha{newLine}{newLine}### Auto-generate release notes?{newLine}{newLine}Yes{newLine}{newLine}### Push to NuGet.org?{newLine}{newLine}_No response_{newLine}{newLine}### Custom release notes?{newLine}{newLine}## Custom release notes {newLine}{newLine}Test 123{newLine}{newLine}Another line:{newLine}- point 1{newLine}- point 2{newLine}- point 3{newLine}{newLine}### Which operating systems have you used?{newLine}{newLine}- [X] macOS{newLine}- [X] Windows{newLine}- [ ] Linux{newLine}- [ ] I don't know{newLine}";
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
        issueFormJson.PushNuget.ShouldBeEmpty();
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes {newLine}{newLine}Test 123{newLine}{newLine}Another line:{newLine}- point 1{newLine}- point 2{newLine}- point 3");
        issueFormJson.OperatingSystems.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldNotBeNull();
        issueFormJson.OperatingSystems.MacOS.ShouldBe(expected: true);
        issueFormJson.OperatingSystems.Windows.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Windows.ShouldBe(expected: true);
        issueFormJson.OperatingSystems.Linux.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Linux.ShouldBe(expected: false);
        issueFormJson.OperatingSystems.Unknown.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Unknown.ShouldBe(expected: false);
    }

    /// <summary>
    /// Tests that the <see cref="ParseIssueFormCommand"/> produces the expected JSON output.
    /// This tests an edge case scenario for matching on the H3 header values. The matching is done
    /// with an string.IndexOf method and if two H3 headers start with the same value then the matching
    /// would always return the first occurence.
    ///
    /// This has been fixed by changing the matching so that it only matches if the H3 header value
    /// matches for an entire line, not just the first string occurence.
    /// </summary>
    [Fact]
    public async Task ParseIssueFormCommandTest3()
    {
        using var console = new FakeInMemoryConsole();
        var issueFormBody = await File.ReadAllTextAsync("./TestFiles/IssueBody2.md");
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody.NormalizeLineEndings(),
            TemplateFilepath = "./TestFiles/Template2.yml",
        };
        await command.ExecuteAsync(console);
        var output = console.ReadOutputString();
        output.ShouldNotBeNull();
    }
}
