using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using EventFlow.Logs;

namespace PizzaMaster.PowerShell
{
    public class BufferedCmdletLog : Log
    {
        private readonly Queue<LogEntry> queue = new Queue<LogEntry>();
        private readonly Stack<Cmdlet> stack = new Stack<Cmdlet>();

        protected override bool IsDebugEnabled => true;

        protected override bool IsInformationEnabled => true;

        protected override bool IsVerboseEnabled => true;

        public void Flush()
        {
            this.Flush(this.stack.Peek());
        }

        private void Flush(Cmdlet target)
        {
            while (this.queue.Count > 0)
            {
                var item = this.queue.Dequeue();
                switch (item.LogLevel)
                {
                    case LogLevel.Verbose:
                        target.WriteVerbose(item.Text);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Information:
                        target.WriteDebug(item.Text);
                        break;
                    case LogLevel.Warning:
                        target.WriteWarning(item.Text);
                        break;
                    case LogLevel.Error:
                    case LogLevel.Fatal:
                        var exception = item.Exception ?? new Exception(item.Text);
                        target.WriteError(new ErrorRecord(exception, "PizzaMasterError", ErrorCategory.NotSpecified,
                                                          null));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(item.LogLevel));
                }
            }
        }

        public void StartLogging(PizzaMasterCmdlet cmdlet)
        {
            //this.stack.Push(cmdlet);
        }

        public void StopLogging(PizzaMasterCmdlet cmdlet)
        {
            //var target = this.stack.Pop();
            //if (target != cmdlet)
            //    throw new InvalidOperationException();

            //this.Flush(cmdlet);
        }

        protected override void Write(LogLevel logLevel, string format, params object[] args)
        {
            if (this.stack.Count > 0)
            {
                var str = args.Length != 0 ? string.Format(format, args) : format;
                this.queue.Enqueue(new LogEntry(logLevel, str));
            }
        }

        protected override void Write(LogLevel logLevel, Exception exception, string format, params object[] args)
        {
            if (this.stack.Count > 0)
            {
                var str = args.Length != 0 ? string.Format(format, args) : format;
                this.queue.Enqueue(new LogEntry(logLevel, str, exception));
            }
        }

        private class LogEntry
        {
            public LogEntry(LogLevel logLevel, string text, Exception exception = null)
            {
                this.LogLevel = logLevel;
                this.Text = text;
                this.Exception = exception;
            }

            public Exception Exception { get; }

            public LogLevel LogLevel { get; }

            public string Text { get; }
        }
    }
}