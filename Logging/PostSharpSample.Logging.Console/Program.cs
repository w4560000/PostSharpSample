using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using PostSharpSample.Logging.BusinessLogic;

// Add logging to all methods of the current project.
[assembly: Log]

namespace PostSharpSample.Logging.Console
{
    [Log(AttributeExclude = false)]   // Removes logging from the Program class itself.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure PostSharp Logging to output logs to the console.
            LoggingServices.DefaultBackend = new ConsoleLoggingBackend();

            Test("999");
            QQTest.Test("888");

            // Simulate some business logic.
            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }

        public static string Test(string param)
        {
            return param + "456";
        }
    }
}
