class CliArgs
{
  # setting these functions in a class because I want to make sure the return type is what is specified and not an array of objects
  # see:
  # https://riptutorial.com/powershell/example/27037/how-to-work-with-functions-returns
  # https://stackoverflow.com/questions/10286164/function-return-value-in-powershell

  [string]SanitizeInputArgs([string[]] $inputArgs) 
  {
    $sanitizedArgs = [string[]]::new($inputArgs.Count)
    Write-Output $inputArgs.Count
    for ($i = 0; $i -lt $inputArgs.Count; $i++)
    {
      $arg = $inputArgs[$i]
      $arg = $this.EscapeSingleQuotes($arg)
      if (!$arg.StartsWith("'"))
      {
        $arg = $this.AddSingleQuote($arg)
      }
  
      $sanitizedArgs[$i] = $arg
    }
  
    return $($sanitizedArgs -join " ")
  }

  [string]EscapeSingleQuotes([string] $value) 
  { 
    return $value -replace '''', ''''''
  }

  [string]AddSingleQuote([string] $value) 
  { 
    return "'$value'"
  }
}

# Keeping this in a function instead of inside a class because of https://stackoverflow.com/questions/52757670/why-does-write-output-not-work-inside-a-powershell-class-method
# And for now prefer to keep using Write-Output instead of Write-Host because of https://www.jsnover.com/blog/2013/12/07/write-host-considered-harmful/
function Main()
{
  [OutputType([Void])]
  param ([string[]] $inputArgs)

  Write-Output "::group::Input arguments"
  Write-Output $inputArgs
  Write-Output "::endgroup::"

  $cliArgs = [CliArgs]::new()
  
  Write-Output "::group::Sanitized input arguments"
  $gitHubIssuesParserArgs =  $cliArgs.SanitizeInputArgs($inputArgs)
  Write-Output $gitHubIssuesParserArgs
  Write-Output "::endgroup::"

  Write-Output "::group::Final command to execute"
  $command = "dotnet '/app/GitHubIssuesParserCli.dll' $gitHubIssuesParserArgs"
  Write-Output $command
  Write-Output "::endgroup::"

  Write-Output "::group::Run dotnet GitHub issue form parser"
  $output = Invoke-Expression $command
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
