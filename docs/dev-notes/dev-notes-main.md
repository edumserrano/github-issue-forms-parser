# Dev notes

## Building

### Building with Visual Studio

1) Clone the repo and open the **GitHubIssueFormsParser.sln** solution file at `/GitHubIssueFormsParser`.
2) Press build on Visual Studio.

### Building with dotnet CLI

1) Clone the repo and browse to the solution's directory at `/GitHubIssueFormsParser` using your favorite shell.
2) Run **`dotnet build GitHubIssueFormsParser.sln`** to build the source of the CLI app.

## Running tests

### Run tests with Visual Studio

1) Clone the repo and open the **GitHubIssueFormsParser.sln** solution file at `/GitHubIssueFormsParser`.
2) Go to the test explorer in Visual Studio and run tests.

**Note:** [Remote testing](https://docs.microsoft.com/en-us/visualstudio/test/remote-testing?view=vs-2022) with WSL is configured on the solution which enables you to run the tests locally on Linux or on Windows. You can view the configuration file at [testenvironments.json](/GitHubIssueFormsParser/testenvironments.json). To run the tests on Linux you need to have at least `Visual Studio 2022` and the Linux distro `Ubuntu-20.04` installed on [WSL](https://docs.microsoft.com/en-us/windows/wsl/install).

### Run tests with dotnet CLI

1) Clone the repo and browse to the solution's directory at `/GitHubIssueFormsParser` using your favorite shell.
2) Run **`dotnet test GitHubIssueFormsParser.sln`** to run tests.

## Projects wide configuration

The [Directory.Build.props](/GitHubIssueFormsParser/Directory.Build.props) enables several settings as well as adds some common NuGet packages for all projects.

There is a set of NuGet packages that are only applied in test projects by using the condition `"'$(IsTestProject)' == 'true'"`. To make this work the `csproj` for the test projects must have the `<IsTestProject>true</IsTestProject>` property defined. Adding this property manually shouldn't be needed because it should be added by the `Microsoft.NET.Test.Sdk` package however there seems to be an issue with this when running tests outside of Visual Studio. See [this GitHub issue](https://github.com/dotnet/sdk/issues/3790#issuecomment-1100773198) for more info.

## Deterministic Build configuration

Following the guide from [Deterministic Builds](https://github.com/clairernovotny/DeterministicBuilds) the `ContinuousIntegrationBuild` setting on the [Directory.Build.props](/GitHubIssueFormsParser/Directory.Build.props) is set to true, if the build is being executed in GitHub actions.

## Repository configuration

From all the GitHub repository settings the configurations worth mentioning are:

- **Automatically delete head branches** is enabled: after pull requests are merged, head branches are deleted automatically.
- **Branch protection rules**. There is a branch protection rule for the the `main` branch that will enforce the following:
  - Require status checks to pass before merging.
  - Require branches to be up to date before merging.
  - Require linear history.

## GitHub Workflows

For more information about the GitHub workflows configured for this repo go [here](/docs/dev-notes/workflows/README.md).
