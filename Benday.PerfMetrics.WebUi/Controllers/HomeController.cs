using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Benday.PerfMetrics.WebUi.Models;

namespace Benday.PerfMetrics.WebUi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(bool throwException = false)
    {
        try
        {
            if (throwException == true)
            {
                throw new Exception("This is a test exception.");
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var model = new RandomDelayViewModel();

            model.DelayDuration = WaitForRandomAmountOfTime();

            stopwatch.Stop();

            ControllerPerformanceMetrics<HomeController>.Log.RecordRequest(
                nameof(Index), stopwatch.ElapsedMilliseconds);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in HomeController.Index().");
            ControllerPerformanceMetrics<HomeController>.Log.RecordError(
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

    public IActionResult Privacy(bool throwException = false)
    {
        try
        {
            if (throwException == true)
            {
                throw new Exception("This is a test exception.");
            }
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var model = new RandomDelayViewModel();

            model.DelayDuration = WaitForRandomAmountOfTime();

            stopwatch.Stop();

            ControllerPerformanceMetrics<HomeController>.Log.RecordRequest(
                nameof(Privacy), stopwatch.ElapsedMilliseconds);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in HomeController.Index().");
            ControllerPerformanceMetrics<HomeController>.Log.RecordError(
                nameof(Privacy));

            return View(new RandomDelayViewModel() { Message = ex.ToString() });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
