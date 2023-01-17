namespace GitHubIssuesParserCli.CliCommands;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(TemplateFilepathOptionValidator) usage
internal sealed class TemplateFilepathOptionValidator : BindingValidator<string>
{
    public override BindingValidationError? Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new BindingValidationError("Cannot be null or whitespace.");
        }

        return !File.Exists(value)
            ? new BindingValidationError("File does not exist.")
            : null;
    }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Referenced via typeof(TemplateFilepathOptionValidator) usage
