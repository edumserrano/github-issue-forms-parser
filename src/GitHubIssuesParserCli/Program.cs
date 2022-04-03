// tests (how do global/implicit usings work there? see https://blog.jetbrains.com/dotnet/2021/11/18/global-usings-in-csharp-10/)

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
