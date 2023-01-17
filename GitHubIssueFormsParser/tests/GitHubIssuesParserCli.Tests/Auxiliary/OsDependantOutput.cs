namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static class OsDependantOutput
{
    public static string ReadAllText(string filepath)
    {
        return Environment.OSVersion.Platform == PlatformID.Unix
            ? File.ReadAllText($"{filepath}-unix.txt").NormalizeLineEndings()
            : File.ReadAllText($"{filepath}-windows.txt").NormalizeLineEndings();
    }
}
