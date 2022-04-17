# build-and-test workflow

[![Build and test](https://github.com/edumserrano/github-issue-forms-parser/workflows/Build%20and%20test/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/build-test.yml)

[This workflow](/.github/workflows/build-test.yml):

- Builds the code and runs tests.
- Generates code coverage and uploads as a workflow artifact.
- Uploads code coverage to Codecov.
- Uploades test results as a workflow artifact.

## Codecov

Codecov integration does not require any secret, it was done via the [Codecov GitHub app](https://github.com/apps/codecov).

Besides the information available on the [Codecov web app](https://app.codecov.io/gh/edumserrano/dot-net-sdk-extensions), this integration enables Codecov to:

- [add status checks on pull requests](https://docs.codecov.com/docs/commit-status)
- [display coverage on pull requests via comments](https://docs.codecov.com/docs/pull-request-comments)
- [add line-by-line coverage on pull requests via file annotations](https://docs.codecov.com/docs/github-checks)

The [Codecov configuration file](/.github/codecov.yml) contains additional configuration for Codecov.

## Build warnings will make the workflow fail

The `dotnet build` command includes the `-warnaserror` flag which will cause the build to fail if there are any errors.

This is used to help keep the code healthy whilst balancing local dev. Meaning, when developing locally there is no need to force all warnings to be fixed to be able to build the code.

## Testing loggers

When running tests we use 3 loggers:

- `trx`: normal logger, produces test result files which can be downloaded and viewed on Visual Studio.
- `GitHubActions`: used to produce annotations on the workflow to give more visibility when tests fail. For more info see [GitHub Actions Test Logger](https://github.com/Tyrrrz/GitHubActionsTestLogger). It also adds annotations on PRs.
- `liquid.custom`: Uses a [template](/GitHubIssueFormsParser/tests/liquid-test-logger-template.md) to create a markdown reports for the test results. These markdown reports are uploaded as workflow artifacts and in case of Pull Requests they are added as comments. For more info see [Liquid Test Reports](https://github.com/kurtmkurtm/LiquidTestReports).
