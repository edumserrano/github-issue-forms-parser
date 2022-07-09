class CliArgs
{
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

function Main()
{
  [OutputType([Void])]
  param ([string[]] $inputArgs)

  Write-Output $inputArgs.Count
  
  Write-Output "::group::Input arguments"
  Write-Output $inputArgs
  Write-Output "::endgroup::"

  $cliArgs = [CliArgs]::new()
  
  Write-Output "::group::Sanitized input arguments"
  $command = $cliArgs.SanitizeInputArgs($inputArgs)
  Write-Output $command  
  $command.GetType()
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
