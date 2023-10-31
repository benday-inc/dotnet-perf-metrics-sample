# dotnet tool install --global dotnet-counters

# check that dotnet-counters command exists
if (Get-Command "dotnet-counters") {
    Write-Host "dotnet-counters exists."
}
else {
    Write-Host "dotnet-counters is not installed."
    Write-Host "Run the following command to install it:"
    Write-Host "dotnet tool install --global dotnet-counters"
    exit;
}

Push-Location .\Benday.PerfMetrics.WebUi

$process = Start-Process -passthru -FilePath dotnet -argumentlist run

Write-Host "Process info..."
Write-Host $process

# wait for n seconds
$secondsToWaitBeforeStartingDotnetCounters = 8
Write-Host "Waiting for $secondsToWaitBeforeStartingDotnetCounters seconds..."
Start-Sleep -Seconds $secondsToWaitBeforeStartingDotnetCounters

# view counters in real time
dotnet-counters monitor --name Benday.PerfMetrics.WebUi --counters Benday.PerfMetrics.WebUi.Controllers.HomeController,Benday.PerfMetrics.WebUi.Controllers.StuffController

Pop-Location

