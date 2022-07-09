namespace GitHubIssuesParserCli.Tests.CliCommands;

/// <summary>
/// These tests make sure that the <see cref="ParseIssueFormCommand"/> outputs the expected JSON value to the console.
/// </summary>
[Trait("Category", XUnitCategories.Commands)]
public class ParseIssueFormCommandTests
{
    [Fact]
    public async Task AAA()
    {
        var issueFormBody = @"
'### What NuGet package do you want to release?

  dotnet-sdk-extensions

  ### What is the new version for the NuGet package?

  1.0.13-alpha

  ### Auto-generate release notes?

  Yes

  ### Push to NuGet.org?

  _No response_

  ### Custom release notes?

  ## Custom release notes

  Test 123

  Another line:
  - point 1
  - point 2
  - point 3

  ### Which operating systems have you used?

  - [X] macOS
  - [X] Windows
  - [ ] Linux
  - [ ] I don''t know'
";
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
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes{Environment.NewLine}{Environment.NewLine}Test 123{Environment.NewLine}{Environment.NewLine}Another line:{Environment.NewLine}- point 1{Environment.NewLine}- point 2{Environment.NewLine}- point 3");
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
    /// Tests that the <see cref="ParseIssueFormCommand"/> produces the expected JSON output.
    /// </summary>
    [Fact]
    public async Task ParseIssueFormCommandTest1()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/IssueBody.md"),
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
        issueFormJson.CustomReleaseNotes.ShouldBe($"## Custom release notes{Environment.NewLine}{Environment.NewLine}Test 123{Environment.NewLine}{Environment.NewLine}Another line:{Environment.NewLine}- point 1{Environment.NewLine}- point 2{Environment.NewLine}- point 3");
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
    /// regardless of the line endings on the issue form body.
    /// </summary>
    [Theory]
    [InlineData(CR)]
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
        issueFormJson.OperatingSystems.MacOS.ShouldBe(true);
        issueFormJson.OperatingSystems.Windows.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Windows.ShouldBe(true);
        issueFormJson.OperatingSystems.Linux.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Linux.ShouldBe(false);
        issueFormJson.OperatingSystems.Unknown.ShouldNotBeNull();
        issueFormJson.OperatingSystems.Unknown.ShouldBe(false);
    }
}
