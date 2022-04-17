namespace GitHubIssuesParserCli.CliCommands;

public class ParseGitHubIssueFormCommandException : Exception
{
    private const string _errorMessage = "An error occurred trying to execute the command to parse a GitHub issue forms. See inner exception for more details.";

    public ParseGitHubIssueFormCommandException(Exception innerException)
       : base(_errorMessage, innerException)
    {
    }
}
