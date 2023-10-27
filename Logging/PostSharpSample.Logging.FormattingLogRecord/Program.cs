using PostSharp.Patterns.Diagnostics;
using PostSharpSample.Logging.BusinessLogic;
using System;

[assembly: Log]

namespace PostSharpSample.Logging.FormattingLogRecord
{
    [Log(AttributeExclude = true)]
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Register the custom logging backend.
            var backend = new CustomLoggingBackend();
            LoggingServices.DefaultBackend = backend;

            // Register the custom parameter formatter.
            LoggingServices.Formatters.Register(new FancyIntFormatter());

            Console.WriteLine("111");
            Test123(new Sensitive<int>(100));

            // Simulate some business logic.
            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");

            Console.ReadKey();
        }

        public static string Test123(Sensitive<int> param)
        {
            return "123";
        }
    }
}