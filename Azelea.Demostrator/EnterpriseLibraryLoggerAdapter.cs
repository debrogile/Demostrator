using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Azelea.Demostrator
{
    public class EnterpriseLibraryLoggerAdapter : ILoggerFacade
    {
        public EnterpriseLibraryLoggerAdapter()
        {
            Logger.SetLogWriter(new LogWriter(CreateLoggingConfig()));
            ExceptionPolicy.SetExceptionManager(CreateExceptionManager());
        }

        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int)priority);
        }

        private static LoggingConfiguration CreateLoggingConfig()
        {
            var config = new LoggingConfiguration();

            var formatter = new TextFormatter(@"Timestamp: {timestamp(local)}{newline}" +
                "Category: {category}{newline}" +
                "Priority: {priority}{newline}" +
                "Severity: {severity}{newline}" +
                "Message: {message}");
            var listener = new FlatFileTraceListener(@".\log\debug.log",
                "=====================================================",
                "=====================================================",
                formatter);
            config.AddLogSource(Category.Exception.ToString(), SourceLevels.All, true).AddTraceListener(listener);

            var callbackListener = CallbackTraceListener.Instance;
            callbackListener.Formatter = new TextFormatter(@"[{timestamp(local)}] {message}{newline}");
            config.AddLogSource(Category.Debug.ToString(), SourceLevels.All, true)
                .AddTraceListener(callbackListener);

            // Special Sources Configuration
            // config.SpecialSources.LoggingErrorsAndWarnings.AddTraceListener(listener);

            config.IsTracingEnabled = true;

            return config;
        }

        private static ExceptionManager CreateExceptionManager()
        {
            var entries = new List<ExceptionPolicyEntry>
            {
                new ExceptionPolicyEntry(typeof(Exception), PostHandlingAction.None, new List<IExceptionHandler>
                {
                    new LoggingExceptionHandler(Category.Exception.ToString(),
                        100,
                        TraceEventType.Error,
                        "Enterprise Library Exception Handling",
                        (int)Priority.High,
                        typeof(TextExceptionFormatter),
                        Logger.Writer)
                })
            };

            var policies = new List<ExceptionPolicyDefinition>
            {
                new ExceptionPolicyDefinition("Default Policy", entries)
            };

            var manager = new ExceptionManager(policies);

            return manager;
        }
    }
}
