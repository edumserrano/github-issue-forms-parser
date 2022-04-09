namespace GitHubIssuesParserCli.Tests.Auxiliary;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Used via generics on JSON deserialization
internal class IssueFormTestModel
{
    [JsonPropertyName("nuget-id")]
    public string? NuGetId { get; set; }

    [JsonPropertyName("nuget-version")]
    public string? NuGetVersion { get; set; }

    [JsonPropertyName("auto-generate-release-notes")]
    public string? AutoGenerateReleaseNotes { get; set; }

    [JsonPropertyName("auto-generate-release-notes-optional")]
    public string? AutoGenerateReleaseNotesOptional { get; set; }

    [JsonPropertyName("custom-release-notes")]
    public string? CustomReleaseNotes { get; set; }

    [JsonPropertyName("operating-systems")]
    public OperatingSystems? OperatingSystems { get; set; }
}

internal class OperatingSystems
{
    [JsonPropertyName("macos")]
    public bool? MacOS { get; set; }

    [JsonPropertyName("windows")]
    public bool? Windows { get; set; }

    [JsonPropertyName("linux")]
    public bool? Linux { get; set; }

    [JsonPropertyName("i-dont-know")]
    public bool? Unknown { get; set; }
}
#pragma warning restore CA1812 // Avoid uninstantiated internal classes. Used via generics on JSON deserialization
