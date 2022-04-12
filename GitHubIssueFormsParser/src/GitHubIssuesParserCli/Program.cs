namespace GitHubIssuesParserCli;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var app = new IssuesParserCli();
        return await app.RunAsync(args);
    }
}
