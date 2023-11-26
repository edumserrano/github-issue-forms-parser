namespace GitHubIssuesParserCli.Tests.Auxiliary;

internal static partial class LineEndings
{
    // matchTimeoutMilliseconds used to prevent denial of service attacks. See MA0009 https://github.com/meziantou/Meziantou.Analyzer/blob/main/docs/Rules/MA0009.md
    // Adding 5 secs which for this scenario means: a random high enough value. It will probably work with much lower values but
    // for this code this doesn't matter much as long as it's compliant with MA0009.
    [GeneratedRegex(@"\r\n", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 5000)]
    private static partial Regex WindowsNewLinesRegex();

    // This pattern uses a negative lookbehind to ensure that the LF character is not preceded by a CR character.
    [GeneratedRegex(@"(?<!\r)\n", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 5000)]
    private static partial Regex LinuxNewLinesRegex();

    /// <summary>
    /// Updates a string so that the line endings match the OS expected line endings.
    /// </summary>
    /// <param name="original">String to update.</param>
    /// <returns>A string containing line endings matching the OS expected line endings.</returns>
    public static string NormalizeLineEndings(this string original)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT && LinuxNewLinesRegex().IsMatch(original))
        {
            // if it's a Windows OS and doesn't contain Windows line endings then replace
            // new lines with Windows line endings
            return LinuxNewLinesRegex().Replace(original, CR + LF);
        }

        if (Environment.OSVersion.Platform == PlatformID.Unix && WindowsNewLinesRegex().IsMatch(original))
        {
            // if it's a Linux OS and contains Windows line endings then replace
            // new lines with Linux line endings
            return WindowsNewLinesRegex().Replace(original, LF);
        }

        return original;
    }
}
