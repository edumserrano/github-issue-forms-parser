namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static class OsDependantOutput
{
    public static string ReadAllText(string filepath)
    {
        return Environment.OSVersion.Platform == PlatformID.Unix
            ? NormalizedLineEndingsFileReader.ReadAllText($"{filepath}-unix.txt")
            : NormalizedLineEndingsFileReader.ReadAllText($"{filepath}-windows.txt");
    }
}
