param ($templateFilepath, $issueFormBody)
Write-Host 'Starting dotnet cli tool'
Write-Host "templateFilepath $templateFilepath"
Write-Host "issueFormBody $issueFormBody"
dotnet '/app/GitHubIssuesParserCli.dll' parse-issue-form -t $templateFilepath -i $issueFormBody