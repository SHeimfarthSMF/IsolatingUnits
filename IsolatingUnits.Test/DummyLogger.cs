using Microsoft.Extensions.Logging;

namespace IsolatingUnits.Test;

public class DummyLogger<T> : ILogger<T>
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        //do nothing
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new Scope();
    }

    private class Scope : IDisposable
    {
        public void Dispose()
        {
            //do nothing
        }
    }
}