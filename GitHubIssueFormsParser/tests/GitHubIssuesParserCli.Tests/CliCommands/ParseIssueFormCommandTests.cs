namespace GitHubIssuesParserCli.Tests.CliCommands;

[Trait("Category", XUnitCategories.Commands)]
public class ParseIssueFormCommandTests
{
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
        issueFormJson.CustomReleaseNotes.ShouldBe("## Custom release notes \r\n\r\nTest 123\r\n\r\nAnother line:\r\n- point 1\r\n- point 2\r\n- point 3");
        issueFormJson.OperatingSystems?.MacOS.ShouldNotBeNull();
        issueFormJson.OperatingSystems?.MacOS?.ShouldBeTrue();
        issueFormJson.OperatingSystems?.Windows.ShouldNotBeNull();
        issueFormJson.OperatingSystems?.Windows?.ShouldBeTrue();
        issueFormJson.OperatingSystems?.Linux.ShouldNotBeNull();
        issueFormJson.OperatingSystems?.Linux?.ShouldBeFalse();
        issueFormJson.OperatingSystems?.Unknown.ShouldNotBeNull();
        issueFormJson.OperatingSystems?.Unknown?.ShouldBeFalse();
    }
}
