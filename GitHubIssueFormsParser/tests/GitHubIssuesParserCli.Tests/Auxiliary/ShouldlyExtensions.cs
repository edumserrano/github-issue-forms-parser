namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static class ShouldlyExtensions
{
    public static void ShouldBeWithNormalizedNewlines(this string actual, string expected)
    {
        var normalizedActual = actual.NormalizeLineEndings();
        var normalizedExpected = expected.NormalizeLineEndings();
        normalizedActual.ShouldBe(normalizedExpected);
    }
}
