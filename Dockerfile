#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine3.15 AS base
# install the requirements
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
# Download the powershell '.tar.gz' archive
RUN curl -L https://github.com/PowerShell/PowerShell/releases/download/v7.2.2/powershell-7.2.2-linux-alpine-x64.tar.gz -o /tmp/powershell.tar.gz
# Create the target folder where powershell will be placed
RUN mkdir -p /opt/microsoft/powershell/7
# Expand powershell to the target folder
RUN tar zxf /tmp/powershell.tar.gz -C /opt/microsoft/powershell/7
# Set execute permissions
RUN chmod +x /opt/microsoft/powershell/7/pwsh
# Create the symbolic link that points to pwsh
RUN ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /github-issue-forms-parser
COPY ["GitHubIssueFormsParser/NuGet.Config", "GitHubIssueFormsParser/"]
COPY ["GitHubIssueFormsParser/src/GitHubIssuesParserCli/GitHubIssuesParserCli.csproj", "GitHubIssueFormsParser/src/GitHubIssuesParserCli/"]
RUN dotnet restore "GitHubIssueFormsParser/src/GitHubIssuesParserCli/GitHubIssuesParserCli.csproj"
COPY . .
WORKDIR "/github-issue-forms-parser/GitHubIssueFormsParser/src/GitHubIssuesParserCli"
RUN dotnet build "GitHubIssuesParserCli.csproj" -c Release -o /app/build

FROM build AS publish
# use ''--no-build' and ''p:OutDir' to pickup the output from dotnet build and avoid building again
RUN dotnet publish "GitHubIssuesParserCli.csproj" -c Release -p:OutDir=/app/build -o /app/publish --no-build 

FROM base AS final
# WORKDIR /app
COPY --from=publish /app/publish /app
COPY ["entrypoint.ps1", "/"]
# ENTRYPOINT ["dotnet", "/app/GitHubIssuesParserCli.dll"]
ENTRYPOINT ["pwsh", "/entrypoint.ps1"]
