# param (
#   $commandName,
#   $templateFilepathOption,
#   $templateFilepath,
#   $issueFormBodyOption,
#   $issueFormBody)

Write-Output "::group::Run dotnet GitHub issue form parser"


Write-Output "==========================="
Write-Output $commandName
Write-Output $templateFilepathOption
Write-Output $templateFilepath
Write-Output $issueFormBodyOption
Write-Output $issueFormBody
Write-Output "==========================="
Write-Output $args.Count
Write-Output $args
Write-Output "==========================="

$templateFilepath = $args[2]
$templateFilepath = "'$templateFilepath'"
$issueFormBody =  $args[4]
# $issueFormBody =  $issueFormBody -replace '''', '`'''
$issueFormBody =  $issueFormBody -replace '''', ''''''
$issueFormBody = "'$issueFormBody'"
$inputArgs =-join($args[0], " ", $args[1], " ", $templateFilepath, " ", $args[3], " ", $issueFormBody)
$command = "dotnet '/app/GitHubIssuesParserCli.dll' $inputArgs"
Write-Output $command
$output = Invoke-Expression $command
if($LASTEXITCODE -ne 0 ) {
    Write-Output "::error::GitHub issue form parser didn't complete successfully. See the step's log for more details."
    exit $LASTEXITCODE
}
Write-Output "::set-output name=parsed-issue::$output"
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue form parser output"
Write-Output $output
Write-Output "::endgroup::"

Write-Output "::group::dotnet GitHub issue form parser output indented"
$outputAsJson = ConvertFrom-Json $output
COnvertTo-Json $outputAsJson
Write-Output "::endgroup::"