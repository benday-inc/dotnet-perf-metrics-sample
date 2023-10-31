using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Benday.PerfMetrics.WebUi.Models;

namespace Benday.PerfMetrics.WebUi.Controllers;

public class StuffController : Controller
{
    private readonly ILogger<StuffController> _logger;

    public StuffController(ILogger<StuffController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var model = new RandomDelayViewModel();

            model.DelayDuration = WaitForRandomAmountOfTime();

            stopwatch.Stop();

            ControllerPerformanceMetrics<StuffController>.Log.RecordRequest(
                nameof(Index), stopwatch.ElapsedMilliseconds);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in StuffController.Index().");
            ControllerPerformanceMetrics<StuffController>.Log.RecordError(
                nameof(Index));

            return View(new RandomDelayViewModel() { Message = ex.ToString() });
        }
    }

    private int WaitForRandomAmountOfTime()
    {
        var rnd = new Random();

        var millisecondsToWait = rnd.Next(100, 1000);

        Thread.Sleep(millisecondsToWait);

        return millisecondsToWait;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
