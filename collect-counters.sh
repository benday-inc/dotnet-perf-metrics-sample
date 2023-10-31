#!/bin/bash

dotnet-counters monitor --name Benday.PerfMetr --counters Benday.PerfMetrics.WebUi.Controllers.HomeController,Benday.PerfMetrics.WebUi.Controllers.StuffController

