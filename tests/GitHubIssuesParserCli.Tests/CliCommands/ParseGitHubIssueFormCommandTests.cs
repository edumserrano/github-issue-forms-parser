namespace GitHubIssuesParserCli.Tests.CliCommands;

public class ParseGitHubIssueFormCommandTests
{
    [Fact]
    public async Task ParseGitHubIssueFormCommandTest1()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseGitHubIssueFormCommand
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
        issueFormJson.CustomReleaseNotes.ShouldBe("## Custom release notes \r\n\r\nTest 123\r\n\r\nAnother line:\r\n- point 1\r\n- point 2\r\n- point 3");
        issueFormJson.OperatingSystems?.MacOS.ShouldBeTrue();
        issueFormJson.OperatingSystems?.Windows.ShouldBeTrue();
        issueFormJson.OperatingSystems?.Linux.ShouldBeFalse();
    }

    // test passing nulls, validation etc
}
