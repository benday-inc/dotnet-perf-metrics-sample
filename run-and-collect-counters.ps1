# dotnet tool install --global dotnet-counters
dotnet tool restore

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

