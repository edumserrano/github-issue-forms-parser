// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;
using GitHubIssuesParserCli.IssueFormBody;

// TODO use implict usings setting on directory build props?? what about tests?
// TODO add enable global usings ?
// custom exceptions instead of throw new NotImplementedException

namespace GitHubIssuesParserCli
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var ymlTemplate = await File.ReadAllTextAsync("template.yml");
            var issueBodyAsString = await File.ReadAllTextAsync("issue2.md", Encoding.UTF8);
            var issueFormBody = issueBodyAsString.ToGitHubIssueFormBody(ymlTemplate);
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true, // TODO change to false
                Converters =
                {
                    new GitHubIssueFormBodyJsonConverter(),
                },
            };
            var jsonString = JsonSerializer.Serialize(issueFormBody, serializeOptions);

            Console.WriteLine(jsonString);
            Console.ReadLine();
        }
    }
}
