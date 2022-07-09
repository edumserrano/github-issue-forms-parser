Main # invoke entrypoint function

function Main()
{
  Write-Output "::group::Run dotnet GitHub issue form parser"
  $command = Sanitize-InputArgs $args 
  Write-Output $command
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

function Sanitize-InputArgs
{
  [OutputType([string])]
  param ([string[]] $inputArgs)

  $sanitizedArgs = [string[]]::new($inputArgs.Count)
  for ($i = 0; $i -lt $inputArgs.Count; $i++)
  {
    $arg = $inputArgs[$i]
    $arg = Escape-SingleQuotes $arg
    if (!$arg.StartsWith("'"))
    {
      $arg  = Add-SingleQuote $arg
    }

    $sanitizedArgs[$i] = $arg
  }

  return $($sanitizedArgs -join " ")
}


function Escape-SingleQuotes
{
  [OutputType([string])]
  param ([string] $value)

  return $value -replace '''', ''''''
}

function Add-SingleQuote
{
  [OutputType([string])]
  param ([string] $value)

  return "'$value'"
}
