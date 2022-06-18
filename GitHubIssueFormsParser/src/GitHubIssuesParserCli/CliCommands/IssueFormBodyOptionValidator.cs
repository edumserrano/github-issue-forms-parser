namespace GitHubIssuesParserCli.CliCommands;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(IssueFormBodyOptionValidator) usage
internal class IssueFormBodyOptionValidator : BindingValidator<string>
{
    public override BindingValidationError? Validate(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? new BindingValidationError("Cannot be null or whitespace.")
            : null;
    }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(TemplateFilepathOptionValidator) usage
