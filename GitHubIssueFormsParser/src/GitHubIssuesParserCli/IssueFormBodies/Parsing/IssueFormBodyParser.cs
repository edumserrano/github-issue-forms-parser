namespace GitHubIssuesParserCli.IssueFormBodies.Parsing;

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
            var (startIdx, valueLength) = GetLevel3HeaderValueIndexes(
                currentTemplateItem.Label,
                nextTemplateItem?.Label,
                issueFormBodyText);
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
        var startIdx = issueFormBodyText.GetStartIndex(currentH3Header.H3HeaderValue) + currentH3Header.H3HeaderValue.Length;
        var endIdx = nextH3Header is null
            ? bodyAsString.Length - 1
            : issueFormBodyText.GetStartIndex(nextH3Header.H3HeaderValue) - 1;
        if (endIdx <= startIdx)
        {
            throw IssueFormBodyParserException.InvalidH3HeaderValue(currentH3Header, nextH3Header);
        }

        var valueLength = endIdx - startIdx + 1;
        return (startIdx, valueLength);
    }

    private static int GetStartIndex(this IssueFormBodyText issueFormBodyText, string h3HeaderValue)
    {
        var bodyAsString = (string)issueFormBodyText;
        var h3HeaderValueWindowsLineEnding = h3HeaderValue + NewLines.CR + NewLines.LF;
        var startIdx = bodyAsString.IndexOf(h3HeaderValueWindowsLineEnding, StringComparison.Ordinal);
        if (startIdx is not -1)
        {
            return startIdx;
        }

        var h3HeaderValueUnixLineEnding = h3HeaderValue + NewLines.LF;
        startIdx = bodyAsString.IndexOf(h3HeaderValueUnixLineEnding, StringComparison.Ordinal);
        if (startIdx is not -1)
        {
            return startIdx;
        }

        throw IssueFormBodyParserException.H3HeaderNotFound(h3HeaderValue);
    }

    internal static class NewLines
    {
        public const string CR = "\r";
        public const string LF = "\n";
    }
}
