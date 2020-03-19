using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Mniak.Automation
{
    internal class MemoryLogger : ILogger
    {
        private readonly IList<dynamic> messages = new List<dynamic>();
        public IDisposable BeginScope<TState>(TState state) => new NullDisposable();
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter.Invoke(state, exception);
            messages.Add(new {
                exception,
                message
            });
        }

        public IEnumerable<dynamic> Messages => messages;
        class NullDisposable : IDisposable
        {
            public void Dispose() {}
        }
    }
}