// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;
using GitHubIssuesParserCli.IssueFormBody.JsonSerialization;
using GitHubIssuesParserCli.IssueFormBody.Parser;
using GitHubIssuesParserCli.IssueFormTemplates;

// TODO use implict usings setting on directory build props?? what about tests?
// TODO add enable global usings ?
// custom exceptions instead of throw new NotImplementedException
// todo add cli library to handle args
//  - should accept file path to template file and string with github issue body
// todo create some classes to represent important types (?):
//  - one for issue form body
//  - one for template file
//  - one for issue form value (this could handle the sanitization issues)
//    - one for checkbox option
//    - one for the rest

namespace GitHubIssuesParserCli
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var ymlTemplateAsString = await File.ReadAllTextAsync("template.yml");
            var issueBodyAsString = await File.ReadAllTextAsync("issue2.md", Encoding.UTF8);

            var issueFormBodyText = new IssueFormBodyText(issueBodyAsString);
            var issueFormTemplateText = new IssueFormYmlTemplateText(ymlTemplateAsString);
            
            var issueFormTemplate = IssueFormYmlTemplateParser.Parse(issueFormTemplateText);
            var issueFormBody = IssueFormBodyParser.Parse(issueFormBodyText, issueFormTemplate);


            //var issueFormBody = issueBodyAsString.ToGitHubIssueFormBody(ymlTemplateAsString);
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true, // TODO change to false
                Converters =
                {
                    new IssueFormBodyJsonConverter(),
                },
            };
            var jsonString = JsonSerializer.Serialize(issueFormBody, serializeOptions);

            Console.WriteLine(jsonString);
            Console.ReadLine();
        }
    }
}
