param ($templateFilepath, $issueFormBody)

Write-Output "::group::GitHub issue form template"
Write-Output "Template filepath: '$templateFilepath'"
$template = Get-Content $templateFilepath
Write-Output $template
Write-Output "::endgroup::"

Write-Output "::group::GitHub issue form body"
Write-Output $issueFormBody
Write-Output "::endgroup::"

Write-Output "::group::Run dotnet GitHub issue form parser"
$output = dotnet '/app/GitHubIssuesParserCli.dll' parse-issue-form -t $templateFilepath -i $issueFormBody
Write-Output "::set-output name=parsed-issue::$output"
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue form parser output"
Write-Output $output
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue form parser output indented"
$outputAsJson = ConvertFrom-Json $output
COnvertTo-Json $outputAsJson
Write-Output "::endgroup::"