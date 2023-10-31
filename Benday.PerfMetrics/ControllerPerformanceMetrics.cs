using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace Benday.PerfMetrics;

public class ControllerPerformanceMetrics<T> : IControllerPerformanceMetrics<T>
{
    public static readonly ControllerPerformanceMetrics<T> Log = new();

    private readonly ConcurrentDictionary<string, MethodMetrics> _methodMetrics = new();

    private readonly Meter _meter;

    public ControllerPerformanceMetrics()
    {
        var baseName = GetBaseName();

        _meter = new Meter(baseName);
    }

    protected virtual string GetBaseName()
    {
        var genericType = typeof(T);

        if (genericType == null)
        {
            throw new InvalidOperationException(
                $"Initialization error. Problem getting type for T.");
        }
        else
        {
            var typeName = genericType.FullName ??
                throw new InvalidOperationException(
                    "Initialization error. Type T does not have a fullname property value");

            return typeName;
        }
    }

    public void RecordRequest(string methodName, double requestDurationInMilliseconds)
    {
        var methodMetrics = GetMethodMetrics(methodName);
        methodMetrics.RecordRequest(requestDurationInMilliseconds);
    }

    public void RecordError(string methodName)
    {
        var methodMetrics = GetMethodMetrics(methodName);
        methodMetrics.RecordError();
    }

    public MethodMetrics GetMethodMetrics(string methodName)
    {
        return _methodMetrics.GetOrAdd(methodName, 
            (key) => new MethodMetrics(_meter, methodName));
    }
}


