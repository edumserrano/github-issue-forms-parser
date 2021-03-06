function Main()
{
  [OutputType([Void])]
  param ([string[]] $inputArgs)

  Write-Output "::group::Input arguments"
  Write-Output $inputArgs
  Write-Output "::endgroup::"
 
  Write-Output "::group::Run dotnet GitHub issue form parser"
  $output = dotnet '/app/GitHubIssuesParserCli.dll' $inputArgs
  if ($LASTEXITCODE -ne 0 )
  {
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
}

# invoke entrypoint function
Main $args