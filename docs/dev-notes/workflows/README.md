# GitHub worflows

There are two workflows setup on this repo:

| Worflow                                                                     | Status and link                                                                                                                                                                                                                                           |                            Description                             |
| --------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------: |
| [build-and-test](/.github/workflows/build-test.yml)                         | [![Build and test](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/build-test.yml/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/build-test.yml)                                       |                 Builds the solution and runs tests                 |
| [test-action](/.github/workflows/test-action.yml)                           | [![Test GitHub action](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/test-action.yml/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/test-action.yml)                                 |                 Builds and tests the GitHub action                 |
| [publish-docker-image](/.github/workflows/publish-docker-image.yml)         | [![Publish Docker image](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/publish-docker-image.yml/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/publish-docker-image.yml)             |   Publishes Docker image used by this action to GitHub packages    |
| [package-retention-policy](/.github/workflows/package-retention-policy.yml) | [![Package retention policy](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/package-retention-policy.yml/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/package-retention-policy.yml) | Removes old Docker images used by this action from GitHub packages |

## Workflows' documentation

- [build-and-test](/docs/dev-notes/workflows/build-and-test-workflow.md)
- [test-action](/docs/dev-notes/workflows/test-action-workflow.md)
- [publish-docker-image](/docs/dev-notes/workflows/publish-docker-image.md)
- [package-retention-policy](/docs/dev-notes/workflows/package-retention-policy.md)
