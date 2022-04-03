using YamlDotNet.Serialization;

namespace GitHubIssuesParserCli.IssueFormTemplates
{
    internal static class GitHubIssueFormYmlTemplateParser
    {
        public static GitHubIssueFormTemplate Parse(string ymlTemplate)
        {
            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();
            return deserializer.Deserialize<GitHubIssueFormTemplate>(ymlTemplate);
        }
    }
}
