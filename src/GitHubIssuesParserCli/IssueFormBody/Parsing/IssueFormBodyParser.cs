namespace GitHubIssuesParserCli.IssueFormBody.Parsing;

internal static class IssueFormBodyParser
{
    public static IssueFormBody Parse(IssueFormBodyText issueFormBodyText, IssueFormYamlTemplate issueFormTemplate)
    {
        issueFormBodyText.NotNull();
        issueFormTemplate.NotNull();

        var templateItems = issueFormTemplate.Items;
        var issueFormItems = new List<IssueFormItem>();
        for (var i = 0; i < templateItems.Count; i++)
        {
            var currentTemplateItem = templateItems[i];
            var nextTemplateItem = templateItems.GetNextTemplateElement(i);
            var (startIdx, valueLength) = GetLevel3HeaderValueIndexes(currentTemplateItem.Label, nextTemplateItem?.Label, issueFormBodyText);
            var bodyAsString = (string)issueFormBodyText;
            var value = bodyAsString.Substring(startIdx, valueLength);
            var issueFormItem = IssueFormItemFactory.CreateFormItem(currentTemplateItem.Id, currentTemplateItem.Type, value);
            issueFormItems.Add(issueFormItem);
        }

        return new IssueFormBody(issueFormItems);
    }

    private static IssueFormYmlTemplateItem? GetNextTemplateElement(this List<IssueFormYmlTemplateItem> templateItems, int templateItemIdx)
    {
        return templateItemIdx == templateItems.Count - 1 ? null : templateItems[templateItemIdx + 1];
    }

    private static (int startIdx, int valueLength) GetLevel3HeaderValueIndexes(
        IssueFormYmlTemplateItemLabel currentH3Header,
        IssueFormYmlTemplateItemLabel? nextH3Header,
        IssueFormBodyText issueFormBodyText)
    {
        var bodyAsString = (string)issueFormBodyText;
        var startIdx = bodyAsString.IndexOf(currentH3Header.H3HeaderValue, StringComparison.Ordinal) + currentH3Header.H3HeaderValue.Length;
        var endIdx = nextH3Header is null
            ? bodyAsString.Length - 1
            : bodyAsString.IndexOf(nextH3Header.H3HeaderValue, StringComparison.Ordinal) - 1;
        var valueLength = endIdx - startIdx + 1;
        return (startIdx, valueLength);
    }
}
