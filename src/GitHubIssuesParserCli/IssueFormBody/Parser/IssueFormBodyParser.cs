namespace GitHubIssuesParserCli.IssueFormBody.Parser;

internal static class IssueFormBodyParser
{
    public static IssueFormBody Parse(IssueFormBodyText issueFormBodyText, IssueFormYmlTemplate issueFormTemplate)
    {
        // markdown template item types do NOT show in the issue form body, they are only used
        // to show some markdown text when creating the issue.
        var templateItems = issueFormTemplate.Body
            .Where(x => x.Type is not IssueFormYmlTemplateItemTypes.Markdown)
            .ToList();
        var issueFormItems = new List<IssueFormItem>();
        for (var i = 0; i < templateItems.Count; i++)
        {
            var currentTemplateItem = templateItems[i];
            var nextTemplateItem = templateItems.GetNextTemplateElement(i);
            var (startIdx, valueLength) = GetLevel3HeaderValueIndexes(currentTemplateItem, nextTemplateItem, issueFormBodyText);
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
        IssueFormYmlTemplateItem currentTemplateElement,
        IssueFormYmlTemplateItem? nextTemplateElement,
        IssueFormBodyText issueFormBodyText)
    {
        var bodyAsString = (string)issueFormBodyText;
        var currentLevel3Header = $"### {currentTemplateElement.Attributes.Label}";
        var startIdx = bodyAsString.IndexOf(currentLevel3Header, StringComparison.Ordinal) + currentLevel3Header.Length;

        int endIdx;
        if (nextTemplateElement is null)
        {
            endIdx = bodyAsString.Length - 1;
        }
        else
        {
            var nextLevel3Header = $"### {nextTemplateElement.Attributes.Label}";
            endIdx = bodyAsString.IndexOf(nextLevel3Header, StringComparison.Ordinal) - 1;
        }

        var valueLength = endIdx - startIdx + 1;
        return (startIdx, valueLength);
    }
}
