#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /github-issue-forms-parser
COPY ["GitHubIssueFormsParser/NuGet.Config", "GitHubIssueFormsParser/"]
COPY ["GitHubIssueFormsParser/src/GitHubIssuesParserCli/GitHubIssuesParserCli.csproj", "GitHubIssueFormsParser/src/GitHubIssuesParserCli/"]
RUN dotnet restore "GitHubIssuesParserCli/GitHubIssuesParserCli.csproj"
COPY . .
WORKDIR "/github-issue-forms-parser/GitHubIssueFormsParser/src/GitHubIssuesParserCli"
RUN dotnet build "GitHubIssuesParserCli.csproj" -c Release -o /app/build

FROM build AS publish
# use ''--no-build' and ''p:OutDir' to pickup the output from dotnet build and avoid building again
RUN dotnet publish "GitHubIssuesParserCli.csproj" -c Release -p:OutDir=/app/build -o /app/publish --no-build 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitHubIssuesParserCli.dll"]
