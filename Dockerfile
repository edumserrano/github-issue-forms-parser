#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
# install powershell as per https://docs.microsoft.com/en-us/powershell/scripting/install/install-alpine
ARG PWSH_VERSION=7.3.6
RUN apk add --no-cache \
    ca-certificates \
    less \
    ncurses-terminfo-base \
    krb5-libs \
    libgcc \
    libintl \
    libssl1.1 \
    libstdc++ \
    tzdata \
    userspace-rcu \
    zlib \
    icu-libs \
    curl
RUN apk -X https://dl-cdn.alpinelinux.org/alpine/edge/main add --no-cache lttng-ust
RUN curl -L https://github.com/PowerShell/PowerShell/releases/download/v${PWSH_VERSION}/powershell-${PWSH_VERSION}-linux-alpine-x64.tar.gz -o /tmp/powershell.tar.gz
RUN mkdir -p /opt/microsoft/powershell/7
RUN tar zxf /tmp/powershell.tar.gz -C /opt/microsoft/powershell/7
RUN chmod +x /opt/microsoft/powershell/7/pwsh
RUN ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh
# end of install powershell

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /github-issue-forms-parser
COPY ["GitHubIssueFormsParser/NuGet.Config", "GitHubIssueFormsParser/"]
COPY ["GitHubIssueFormsParser/src/GitHubIssuesParserCli/GitHubIssuesParserCli.csproj", "GitHubIssueFormsParser/src/GitHubIssuesParserCli/"]
RUN dotnet restore "GitHubIssueFormsParser/src/GitHubIssuesParserCli/GitHubIssuesParserCli.csproj"
COPY . .
WORKDIR "/github-issue-forms-parser/GitHubIssueFormsParser/src/GitHubIssuesParserCli"
RUN dotnet build "GitHubIssuesParserCli.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
# use ''--no-build' and ''p:OutDir' to pickup the output from dotnet build and avoid building again
RUN dotnet publish "GitHubIssuesParserCli.csproj" -c Release -p:OutDir=/app/build -o /app/publish --no-build

FROM base AS final
COPY --from=publish /app/publish /app
COPY ["entrypoint.ps1", "/"]
ENTRYPOINT ["pwsh", "/entrypoint.ps1"]
