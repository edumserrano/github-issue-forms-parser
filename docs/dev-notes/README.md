# Dev notes

## Building the GitHubIssueFormsParser solution

### Building with Visual Studio

1) Clone the repo and open the **GitHubIssueFormsParser.sln** solution file at `/GitHubIssueFormsParser`.
2) Press build on Visual Studio.

### Building with dotnet CLI

1) Clone the repo and browse to the solution's directory at `/GitHubIssueFormsParser` using your favorite shell.
2) Run **`dotnet build GitHubIssueFormsParser.sln`** to build the source of the CLI app.

## Running GitHubIssueFormsParser solution tests

### Run tests with Visual Studio

1) Clone the repo and open the **GitHubIssueFormsParser.sln** solution file at `/GitHubIssueFormsParser`.
2) Go to the test explorer in Visual Studio and run tests.

**Note:** [Remote testing](https://docs.microsoft.com/en-us/visualstudio/test/remote-testing?view=vs-2022) with WSL is configured on the solution which enables you to run the tests locally on Linux or on Windows. You can view the configuration file at [testenvironments.json](/GitHubIssueFormsParser/testenvironments.json). To run the tests on Linux you need to have at least `Visual Studio 2022` and the Linux distro `Ubuntu-20.04` installed on [WSL](https://docs.microsoft.com/en-us/windows/wsl/install).

### Run tests with dotnet CLI

1) Clone the repo and browse to the solution's directory at `/GitHubIssueFormsParser` using your favorite shell.
2) Run **`dotnet test GitHubIssueFormsParser.sln`** to run tests.

## Building and running the Docker container action using Powershell against a set of test data

The steps below show how to run the Docker container action against a set of test data provided by the repo. However you can follow the same steps and provide any data you wish to test.

1) Clone the repo and browse to the repo's directory.
2) Run `docker build -t github-issue-parser .`
3) Read the test issue form body into the variable `$issueBody` by doing: `$issueBody = Get-Content GitHubIssueFormsParser/tests/GitHubIssuesParserCli.Tests/TestFiles/IssueBody.md -Raw`
4) Run the docker container by executing `docker run --rm -v ${pwd}:/workspace --workdir /workspace github-issue-parser GitHubIssueFormsParser/tests/GitHubIssuesParserCli.Tests/TestFiles/Template.yml $issueBody`.

**Notes:**

- The docker container entrypoint expects two arguments:
    1) First, the filepath to the issue form template.
    2) Second, the issue form body.
- When running the docker container the current directory is mounted to the docker container so that we can read the issue form template.
- When reading the issue form body into the `$issueBody` variable we have to use the `-Raw` parameter to avoid problems with line endings.

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

## Note about the Docker container action

This repo provides a [Docker container action](https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action). See here for more information about the [syntax for a Docker container action](https://docs.github.com/en/actions/creating-actions/metadata-syntax-for-github-actions#runs-for-docker-container-actions). To understand better how the action builds and executes the Docker container look at the log for the steps that build and run the action.

### As of writing this, the log for building the docker action looks as follows

```
/usr/bin/docker build 
-t 2bcf09:d996dfb6f4ec40c1a59c1e244bdd3374 
-f "/home/runner/work/_actions/edumserrano/github-issue-forms-parser/v1/Dockerfile" 
"/home/runner/work/_actions/edumserrano/github-issue-forms-parser/v1"
```

Note that the `docker build` command points to the Dockerfile on `/home/runner/work/_actions/edumserrano/github-issue-forms-parser/v1/Dockerfile`. What is happening here is that GitHub clones the action's repository into the GitHub runner's working directory of the repo making use of this action. The clone of action's repo will be under the `_actions` folder.

This way it can successfully build the Dockerfile for this action which would otherwise fail since the Dockerfile references files in the action's repository which would not be present in the repository making use of this action.

**Example:**

- Repository `hello-world` creates a workflow that uses the `GitHub issue forms parser` action.
- When the workflow is executing, it contains a setup step that runs befor any of the workflow defined steps. This step will clone the `GitHub issue forms parser` action repo into the runner's workding directory under the `_actions` folder and build the Docker container.
- This allows the Dockerfile to reference files in the `GitHub issue forms parser` repo even though the workflow has not explicitly checked it out.

### As of writing this, the log for running the docker action looks as follows

```
/usr/bin/docker run 
--name bcf09d996dfb6f4ec40c1a59c1e244bdd3374_381201 
--label 2bcf09 
--workdir /github/workspace 
--rm 
-e INPUT_TEMPLATE-FILEPATH -e INPUT_ISSUE-FORM-BODY -e HOME 
-e GITHUB_JOB -e GITHUB_REF -e GITHUB_SHA 
-e GITHUB_REPOSITORY -e GITHUB_REPOSITORY_OWNER -e GITHUB_RUN_ID 
-e GITHUB_RUN_NUMBER -e GITHUB_RETENTION_DAYS -e GITHUB_RUN_ATTEMPT 
-e GITHUB_ACTOR -e GITHUB_WORKFLOW -e GITHUB_HEAD_REF 
-e GITHUB_BASE_REF -e GITHUB_EVENT_NAME -e GITHUB_SERVER_URL 
-e GITHUB_API_URL -e GITHUB_GRAPHQL_URL -e GITHUB_REF_NAME 
-e GITHUB_REF_PROTECTED -e GITHUB_REF_TYPE -e GITHUB_WORKSPACE 
-e GITHUB_ACTION -e GITHUB_EVENT_PATH -e GITHUB_ACTION_REPOSITORY 
-e GITHUB_ACTION_REF -e GITHUB_PATH -e GITHUB_ENV 
-e GITHUB_STEP_SUMMARY -e RUNNER_OS -e RUNNER_ARCH 
-e RUNNER_NAME -e RUNNER_TOOL_CACHE -e RUNNER_TEMP 
-e RUNNER_WORKSPACE -e ACTIONS_RUNTIME_URL -e ACTIONS_RUNTIME_TOKEN 
-e ACTIONS_CACHE_URL -e GITHUB_ACTIONS=true -e CI=true 
-v "/var/run/docker.sock":"/var/run/docker.sock" 
-v "/home/runner/work/_temp/_github_home":"/github/home" 
-v "/home/runner/work/_temp/_github_workflow":"/github/workflow" 
-v "/home/runner/work/_temp/_runner_file_commands":"/github/file_commands" 
-v "/home/runner/work/github-issue-forms-parser/github-issue-forms-parser":"/github/workspace" 
2bcf09:d996dfb6f4ec40c1a59c1e244bdd3374  <template-file-path> <issue-form-body>
```

When running the docker container there are lots of docker parameters set. Besides all the environment variables note that there are several volume mounts. More importantly, note that the contents of the checked out repo where the action is executing is mounted into the container at `/github/workspace` and that the `workdir` is also set to `/github/workspace`.

This allows the GitHub action to access the files checked out by the workflow and is what allows users to pass in a relative path to their repository for the `template-filepath` action's input parameter.

**Example:**

- Repository `hello-world` has an issue form template file at `.github\ISSUE_TEMPLATE\my-template.yml`.
- We create a workflow in the `hello-world` repository that checks out the `hello-world` repo and makes use of the `GitHub issue forms parser` action.
- We set the `template-filepath` input parameter of the `GitHub issue forms parser` action to `.github\ISSUE_TEMPLATE\my-template.yml`.
- When the workflow is executing the Docker container is able to get to `.github\ISSUE_TEMPLATE\my-template.yml` because the contents of the checked out `hello-world` repo are mounted into the Docker container at `/github/workspace`. Furthermore the `template-filepath` input parameter doesn't need to start with `/github/workspace` because the `workdir` parameter is set to `/github/workspace` when executing the Docker container.

## Other notes

When creatng the [Test GitHub action workflow](/.github/workflows/test-action.yml) I sometimes had difficulty figuring out how to properly read the issue form body from a file and pass it into the GitHub action as an input parameter.

What was happening initially was that the newlines were not being preserved and the action would fail to parse the issue form body. To help me debug this issue and see exactly what text, including newline characters, were being passed into the action I added the following debug step:

```yml
- name: Debug reading issue form body file using -Raw
  run: |
    $issueBody = Get-Content ./GitHubIssueFormsParser/tests/GitHubIssuesParserCli.Tests/TestFiles/IssueBody.md -Raw
    $issue = @{
    body = $issueBody
    }
    $issue | ConvertTo-Json
```

The above will output a JSON string in which the value of the property body will also contain the newline characters if available. With this I was able to identify that without the `-Raw` parameter I was losing the newline characters. See here for more info on [`-Raw`](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/get-content).
