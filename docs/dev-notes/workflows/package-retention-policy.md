# package-retention-policy workflow

[![Package retention policy](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/package-retention-policy.yml/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/package-retention-policy.yml)

[This workflow](/.github/workflows/package-retention-policy.yml):

- Removes old and unused Docker images used by this action from the GitHub packages registry.

## Secrets

This workflow uses a custom secret `CLEANUP_PACKAGES_GH_TOKEN`. This secret contains a GitHub token with permissions to read and delete GitHub packages and has no expiration date. [Read here](https://github.com/snok/container-retention-policy#token) for further details on why this token is required.
