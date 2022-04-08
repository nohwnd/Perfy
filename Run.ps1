param (
    [ValidateSet("Debug", "Release")]
    [string] $Configuration = "Debug"
)

dotnet build --configuration $Configuration

$path = "$PSScriptRoot\Perfy.TestAdapter\bin\$Configuration\net7.0\adapter\Perfy.TestAdapter.dll"

$results = [System.Collections.Generic.List[Object]]@()

$steps = @("Discovery", "Run")
$counts = @(1, 100, 1000, 10000)
foreach ($step in $steps) {
    foreach ($count in $counts) {
        $env:TEST_COUNT = $count
        $sw = [Diagnostics.StopWatch]::StartNew()

        if ($step -eq "Discovery") {
            # --list-tests does discovery only, the output is long and adds writing to console to overhead
            # we dont care about that, so we capture it and ignore it.
            $null = dotnet test $path --list-tests
            "Discovering $count tests took $($sw.ElapsedMilliseconds) ms"
        }
        else {
            # run output is short, it is nice to see it
            dotnet test $path
        }

        $results.Add([pscustomobject]@{
            Step = $Step
            Count = $count
            Time = "$($sw.ElapsedMilliseconds) ms"
            ExitCode = $LASTEXITCODE
        })
    } 
}

$results

if ($results.ExitCode -gt 0) { 
    throw "At least one step failed."
}