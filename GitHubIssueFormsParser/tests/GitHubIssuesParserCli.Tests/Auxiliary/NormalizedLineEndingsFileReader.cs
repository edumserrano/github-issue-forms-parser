namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static class NormalizedLineEndingsFileReader
{
    /// <summary>
    /// Opens a text file, reads all text and then closes the file.
    /// </summary>
    /// <param name="path">The file to open for reading.</param>
    /// <returns>A string containing all the text of the file with line endings matching the OS.</returns>
    public static string ReadAllText(string path)
    {
        var original = File.ReadAllText(path);
        if (Environment.OSVersion.Platform == PlatformID.Win32NT && original.Contains(CR + LF, StringComparison.Ordinal))
        {
            // if it's a Windows OS and contains Windows line endings then do nothing
            return original;
        }
        if (Environment.OSVersion.Platform == PlatformID.Win32NT && original.Contains(LF, StringComparison.Ordinal))
        {
            // if it's a Windows OS and doesn't contain Windows line endings then replace
            // new lines with Windows line endings
            return original.Replace(LF, Environment.NewLine, StringComparison.Ordinal);
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix && original.Contains(CR + LF, StringComparison.Ordinal))
        {
            // if it's a Linux OS and contains Windows line endings then replace
            // new lines with Linux line endings
            return original.Replace(CR + LF, Environment.NewLine, StringComparison.Ordinal);
        }

        return original;
    }
}
