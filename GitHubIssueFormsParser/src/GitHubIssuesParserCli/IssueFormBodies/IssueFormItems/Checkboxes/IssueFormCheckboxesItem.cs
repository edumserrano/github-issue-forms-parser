namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems.Checkboxes;

internal sealed class IssueFormCheckboxesItem : IssueFormItem
{
    public IssueFormCheckboxesItem(string id, IssueFormCheckBoxesText text)
        : base(id, IssueFormItemTypes.Checkboxes)
    {
        id.NotNullOrWhiteSpace();
        text.NotNull();

        Options = text.Options;
    }

    public List<IssueFormCheckboxOption> Options { get; }

    public override void WriteAsJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject(Id);
        foreach (var option in Options)
        {
            var slugifiedLabel = option.Label.GenerateSlug();
            writer.WriteBoolean(slugifiedLabel, option.IsChecked);
        }

        writer.WriteEndObject();
    }
}
