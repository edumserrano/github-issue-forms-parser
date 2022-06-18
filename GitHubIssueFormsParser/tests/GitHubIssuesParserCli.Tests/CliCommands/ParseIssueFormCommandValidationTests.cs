namespace GitHubIssuesParserCli.Tests.CliCommands;

[Trait("Category", XUnitCategories.Validation)]
public class ParseIssueFormCommandValidationTests
{
    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.IssueFormBody"/> command parameter.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateIssueFormBodyParam(string issueFormBody)
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

    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command parameter.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateTemplateFilepathParam(string templateFilepath)
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

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command parameter points
    /// to a file that doesn't exist.
    /// </summary>
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

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command parameter is not a valid
    /// YAML templte.
    /// </summary>
    [Fact]
    public async Task InvalidTemplate()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "some issue form body",
            TemplateFilepath = "./TestFiles/InvalidTemplate.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormYamlTemplateParserException>();
        exception.InnerException.Message.ShouldStartWith("Failed to deserialize the issue form template. The template must contain at least an YAML member named 'body' at the first indentation level.");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command parameter is
    /// not a JSON string.
    /// </summary>
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

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command parameter is
    /// valid but the H3 headers order doesn't match the template.
    /// </summary>
    [Fact]
    public async Task InvalidIssueFormBody2()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/IssueBodyWithOutOfOrderH3Headers.md"),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Failed to obtain the value for H3 header: '### What NuGet package do you want to release?'. Couldn't find any text between that H3 header and the next H3 header: '### What is the new version for the NuGet package?'.");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command parameter is
    /// valid but the H3 headers order doesn't match the template.
    /// </summary>
    [Fact]
    public async Task InvalidIssueFormBody3()
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = NormalizedLineEndingsFileReader.ReadAllText("./TestFiles/IssueBodyWithMangledCheckbox.md"),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<ParseGitHubIssueFormCommandException>(() => command.ExecuteAsync(console).AsTask());
        exception.Message.ShouldBe("An error occurred trying to execute the command to parse a GitHub issue form. See inner exception for more details.");
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Invalid checkbox option text: '- macOS'.");
    }
}
