namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static class LineEndings
{
    /// <summary>
    /// Updates a string so that the line endings match the OS expected line endings.
    /// </summary>
    /// <param name="original">String to update.</param>
    /// <returns>A string containing line endings matching the OS expected line endings.</returns>
    public static string NormalizeLineEndings(this string original)
    {
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
