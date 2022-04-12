namespace GitHubIssuesParserCli;

public class IssuesParserCli
{
    public IssuesParserCli()
    {
        CliApplicationBuilder = new CliApplicationBuilder().AddCommandsFromThisAssembly();
    }

    public CliApplicationBuilder CliApplicationBuilder { get; }

    public ValueTask<int> RunAsync(IReadOnlyList<string> args = default!)
    {
        args ??= Array.Empty<string>();
        return CliApplicationBuilder
            .Build()
            .RunAsync(args);
    }
}
