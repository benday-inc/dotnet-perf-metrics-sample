namespace Benday.PerfMetrics;

public interface IControllerPerformanceMetrics<T>
{
    void RecordRequest(string methodName, double requestDurationInMilliseconds);
    void RecordError(string methodName);
}


