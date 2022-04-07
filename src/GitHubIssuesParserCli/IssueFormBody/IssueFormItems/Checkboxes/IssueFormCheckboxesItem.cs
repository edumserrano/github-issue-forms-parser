namespace GitHubIssuesParserCli.IssueFormBody.IssueFormItems.Checkboxes;

internal sealed class IssueFormCheckboxesItem : IssueFormItem
{
    public IssueFormCheckboxesItem(string id, IssueFormCheckBoxesText text)
        : base(id, IssueFormItemTypes.Checkboxes)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        Options = text.Options;
    }

    public List<IssueFormCheckboxOption> Options { get; }

    public override void WriteAsJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject(Id);
        foreach (var option in Options)
        {
            writer.WriteBoolean(option.Name, option.IsChecked);
        }

        writer.WriteEndObject();
    }
}
