param ($templateFilepath, $issueFormBody)

Write-Output "::group::GitHub issue forms template"
Write-Output "Template filepath: '$templateFilepath'"
$template = Get-Content $templateFilepath
Write-Output $template
Write-Output "::endgroup::"

Write-Output "::group::GitHub issue forms body"
Write-Output $issueFormBody
Write-Output "::endgroup::"

Write-Output "::group::Run dotnet GitHub issue forms parser"
$output = dotnet '/app/GitHubIssuesParserCli.dll' parse-issue-form -t $templateFilepath -i $issueFormBody
Write-Output "::set-output name=parsed-issue::$output"
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue forms parser output"
Write-Output $output
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue forms parser output indented"
$outputAsJson = ConvertFrom-Json $output
COnvertTo-Json $outputAsJson
Write-Output "::endgroup::"