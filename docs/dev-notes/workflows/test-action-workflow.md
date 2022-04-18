# test-action workflow

[![Test GitHub action](https://github.com/edumserrano/github-issue-forms-parser/workflows/Test%20GitHub%20action/badge.svg)](https://github.com/edumserrano/github-issue-forms-parser/actions/workflows/test-action.yml)

[This workflow](/.github/workflows/test-action.yml):

- Contains a step that executes the GitHub action provided by this repo.
- Runs the action against a valid test template and a valid test issue form body.
- Checks that the output produced by the action is as expected.
- Runs the action against a valid test template and an invalid test issue form body.
- Checks that action should have failed the workflow.

Since this workflow executes the [Docker container action](https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action) it will build and execute the docker container so if there are any issues with the action's [Dockerfile](/Dockerfile) this workflow will detect it.
