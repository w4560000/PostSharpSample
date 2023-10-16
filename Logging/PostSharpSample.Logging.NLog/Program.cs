using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharpSample.Logging.BusinessLogic;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using LogLevel = NLog.LogLevel;
using System.Text;
using System;
using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;

namespace PostSharpSample.Logging.NLog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure NLog.
            var nlogConfig = new LoggingConfiguration();

            var fileTarget = new FileTarget("file")
            {
                FileName = "D:/logs/Lab/PostSharpSample.Logging.NLog/Web.log",
                Encoding = Encoding.UTF8,
                Layout = @"${longdate} ${uppercase:${level}} ${logger} ${message:withException=true}",
                ArchiveFileName = "D:/logs/Lab/PostSharpSample.Logging.NLog/Web.{#}.log",
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveEvery = FileArchivePeriod.Hour,
                ArchiveDateFormat = "yyyyMMdd-HHmm",
                MaxArchiveFiles = 120,
                ConcurrentWrites = false,
            };

            nlogConfig.AddTarget(fileTarget);
            nlogConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));

            var consoleTarget = new ConsoleTarget("console");
            consoleTarget.Layout = @"${longdate} ${uppercase:${level}} ${logger} ${message:withException=true}";
            nlogConfig.AddTarget(consoleTarget);
            nlogConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));



            // Configure PostSharp Logging to use NLog.
            LoggingServices.DefaultBackend = new NLogLoggingBackend(new LogFactory(nlogConfig));

            LogManager.EnableLogging();

            // Simulate some business logic.
            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }
    }
}
