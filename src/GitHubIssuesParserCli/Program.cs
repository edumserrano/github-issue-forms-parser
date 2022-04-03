using CliFx;

// TODO use implict usings setting on directory build props?? what about tests?
// TODO add enable global usings ?
// custom exceptions instead of throw new NotImplementedException

namespace GitHubIssuesParserCli;

internal static class Program
{
    public static async Task<int> Main()
    {
        return await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
    }
}
