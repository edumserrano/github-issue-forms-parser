namespace GitHubIssuesParserCli.Tests.CliCommands;

[Trait("Category", XUnitCategories.Validation)]
public class ParseIssueFormCommandValidationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task ValidateIssueFormBodyParam(string? issueFormBody)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody,
            TemplateFilepath = "./some/file/path.txt",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("IssueFormBody cannot be null or whitespace.");
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task ValidateTemplateFilepathParam(string? templateFilepath)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "some issue form body",
            TemplateFilepath = templateFilepath,
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("TemplateFilepath cannot be null or whitespace.");
    }

    [Fact]
    public async Task TemplateFilepathNotFound()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "some issue form body",
            TemplateFilepath = "./this/path/does/not/exist.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<DirectoryNotFoundException>();
        exception.InnerException.Message.ShouldStartWith("Could not find a part of the path");
    }

    [Fact]
    public async Task InvalidIssueFormBody()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "invalid json body",
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("H3 header value '### What NuGet package do you want to release?' not found in issue form body.");
    }

    [Fact]
    public async Task InvalidIssueFormBody2()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = File.ReadAllText("./TestFiles/IssueBodyWithOutOfOrderH3Headers.md"),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Failed to obtain the value for H3 header: '### What NuGet package do you want to release?'. Couldn't find any text between that H3 header and the next H3 header: '### What is the new version for the NuGet package?'.");
    }

    [Fact]
    public async Task InvalidIssueFormBody3()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = File.ReadAllText("./TestFiles/IssueBodyWithMangledCheckbox.md"),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Invalid checkbox option text: '- macOS'.");
    }
}
