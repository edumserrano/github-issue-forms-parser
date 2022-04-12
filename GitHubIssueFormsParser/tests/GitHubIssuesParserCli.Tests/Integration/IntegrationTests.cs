namespace GitHubIssuesParserCli.Tests.Integration;

[Trait("Category", XUnitCategories.Integration)]
public class IntegrationTests
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
}
