#!/bin/bash

dotnet-counters monitor --name Benday.PerfMetr --counters Benday.PerfMetrics.WebUi.Controllers.HomeController,Benday.PerfMetrics.WebUi.Controllers.StuffController

# note: if the monitor command isn't finding your process, try this:
# dotnet-counters ps
#
# that will show you the list of processes that dotnet-counters can see
# look for the one that matches the app and then use that name.
# it can be a little confusing because it seems like the tool goes after an abreviated name
# sometimes.  