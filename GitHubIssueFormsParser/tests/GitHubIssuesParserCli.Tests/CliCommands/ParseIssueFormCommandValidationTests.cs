using GitHubIssuesParserCli.IssueFormBodies.Parsing;

namespace GitHubIssuesParserCli.Tests.CliCommands;

/// <summary>
/// These tests check the validation on the options for the <see cref="ParseIssueFormCommand"/>.
/// These tests are not for the Validators applied to the command options. They are for logic constrains enforced
/// before the command logic can be executed.
/// </summary>
[Trait("Category", XUnitCategories.Validation)]
public class ParseIssueFormCommandValidationTests
{
    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.IssueFormBody"/> command option.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateIssueFormBodyOption(string issueFormBody)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody,
            TemplateFilepath = "./some/file/path.txt",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- IssueFormBody cannot be null or whitespace. (Parameter 'IssueFormBody')";

        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("IssueFormBody cannot be null or whitespace. (Parameter 'IssueFormBody')");
    }

    /// <summary>
    /// Validation test for the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command option.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateTemplateFilepathOption(string templateFilepath)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "some issue form body",
            TemplateFilepath = templateFilepath,
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- TemplateFilepath cannot be null or whitespace. (Parameter 'TemplateFilepath')";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<ArgumentException>();
        exception.InnerException.Message.ShouldBe("TemplateFilepath cannot be null or whitespace. (Parameter 'TemplateFilepath')");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command option points
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
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- Could not find a part of the path";
        exception.Message.ShouldStartWith(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<DirectoryNotFoundException>();
        exception.InnerException.Message.ShouldStartWith("Could not find a part of the path");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.TemplateFilepath"/> command option is not a valid
    /// YAML templte.
    /// </summary>
    [Theory]
    [InlineData("./TestFiles/InvalidTemplate.yml")]
    [InlineData("./TestFiles/InvalidTemplate2.yml")]
    public async Task InvalidTemplate(string templateFilepath)
    {
        using var console = new FakeInMemoryConsole();
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = "some issue form body",
            TemplateFilepath = templateFilepath,
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- Failed to deserialize the issue form template. The template must contain at least an YAML member named 'body' at the first indentation level.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormYamlTemplateParserException>();
        exception.InnerException.Message.ShouldStartWith("Failed to deserialize the issue form template. The template must contain at least an YAML member named 'body' at the first indentation level.");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command option is
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
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- H3 header value '### What NuGet package do you want to release?' not found in issue form body.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("H3 header value '### What NuGet package do you want to release?' not found in issue form body.");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command option is
    /// valid but the H3 headers order doesn't match the template.
    /// </summary>
    [Fact]
    public async Task InvalidIssueFormBody2()
    {
        using var console = new FakeInMemoryConsole();
        var issueFormBody = await File.ReadAllTextAsync("./TestFiles/IssueBodyWithOutOfOrderH3Headers.md");
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody.NormalizeLineEndings(),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- Failed to obtain the value for H3 header: '### What NuGet package do you want to release?'. Couldn't find any text between that H3 header and the next H3 header: '### What is the new version for the NuGet package?'.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Failed to obtain the value for H3 header: '### What NuGet package do you want to release?'. Couldn't find any text between that H3 header and the next H3 header: '### What is the new version for the NuGet package?'.");
    }

    /// <summary>
    /// Test for when the <see cref="ParseIssueFormCommand.IssueFormBody"/> command option is
    /// valid but the H3 headers order doesn't match the template.
    /// </summary>
    [Fact]
    public async Task InvalidIssueFormBody3()
    {
        using var console = new FakeInMemoryConsole();
        var issueFormBody = await File.ReadAllTextAsync("./TestFiles/IssueBodyWithMangledCheckbox.md");
        var command = new ParseIssueFormCommand
        {
            IssueFormBody = issueFormBody.NormalizeLineEndings(),
            TemplateFilepath = "./TestFiles/Template.yml",
        };
        var exception = await Should.ThrowAsync<CommandException>(() => command.ExecuteAsync(console).AsTask());
        const string expectedErrorMessage = @"An error occurred trying to execute the command to parse a GitHub issue form.
Error:
- Invalid checkbox option text: '- macOS'.";
        exception.Message.ShouldBe(expectedErrorMessage);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException.ShouldBeAssignableTo<IssueFormBodyParserException>();
        exception.InnerException.Message.ShouldStartWith("Invalid checkbox option text: '- macOS'.");
    }
}
