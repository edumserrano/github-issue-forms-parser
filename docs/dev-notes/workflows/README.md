# GitHub worflows

There are two workflows setup on this repo:

| Worflow                                             |                                                                                                     Status and link                                                                                                      |            Description             |
| --------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | :--------------------------------: |
| [build-and-test](/.github/workflows/build-test.yml) |     [![Build and test](https://github.com/edumserrano/github-issue-forms-parser/workflows/Build%20and%20test/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/build-test.yml)      | Builds the solution and runs tests |
| [test-actions](/.github/workflows/test-action.yml)  | [![Test GitHub action](https://github.com/edumserrano/github-issue-forms-parser/workflows/Test%20GitHub%20action/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/test-action.yml) | Builds and tests the GitHub action |

## Workflows' documentation

- [build-and-test](/docs/dev-notes/workflows/build-and-test-workflow.md)
- [test-actions](/docs/dev-notes/workflows/test-action-workflow.md)

## Note about status badges

I couldn't create a status badge by executing the steps documented in [Adding a workflow status badge](https://docs.github.com/en/actions/monitoring-and-troubleshooting-workflows/adding-a-workflow-status-badge). Following thedocumentation was giving me a status badge which would always say `no status` isntead of the pass/fail status.

There are some articles online explaining that this happens when the workflow has a `name` defined. Although these articles are old and the current GitHub documentation does not mention this workaround, formatting the `svg` link for the status badge as follows successfully produced the status badge:

- https://github.com/{repo}/workflows/{workflow-name-URI-encoded}/badge.svg

For instance, for this repo and for the [build-and-test](/.github/workflows/build-test.yml) workflow the `svg` link is:

- https://github.com/edumserrano/github-issue-forms-parser/workflows/Build%20and%20test/badge.svg
