namespace GitHubIssuesParserCli.IssueFormBodies.IssueFormItems.Text;

internal sealed class IssueFormTextItem : IssueFormItem
{
    public IssueFormTextItem(string id, IssueFormText text)
        : base(id, IssueFormItemTypes.Text)
    {
        Text = text.NotNull();
    }

    public string Text { get; }

    public override void WriteAsJson(Utf8JsonWriter writer)
    {
        writer.WriteString(Id, Text);
    }
}
